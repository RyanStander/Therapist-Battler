using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace UnityConnector
{
    /**
     * Connector for the Unity InstantAvatar
     */
    public class DataSender
    {
        private string _ipAddr = "localhost";
        private int _segmentDataPort = 4242;
        private int _shadowSegmentDataPort = 4244;
        private int _commandDataPort = 4243;
        private Socket _segmentDataSocket;
        private Socket _shadowSocket;
        private Socket _commandDataSocket;
        private byte[] _data = new byte[1024];
        private byte[] _segment_bytes = new byte[1024];

        private string lastSegmentsMessage = null;

        /// <summary>
        /// Create DataSender and connect to avator using default ports and ip
        /// </summary>
        public DataSender()
        {
            initConnections(_ipAddr, _segmentDataPort, _shadowSegmentDataPort, _commandDataPort);
        }

        /// <summary>
        /// Create DataSender and connect to avator using give ports and ip
        /// </summary>
        public DataSender(string ipAddr, int segmentPort, int shadowPort, int commandPort)
        {
            initConnections(ipAddr, segmentPort, shadowPort, commandPort);
        }
        
        /// <summary>
        /// init connection with instant avatar (default: localhost, 4242, 4244, 4243)<br/>
        /// 
        /// </summary>
        /// <param name="ipAddr">server ip of avatar; default: localhost</param>
        /// <param name="segmentPort">segment server port of avatar; default: 4242</param>
        /// <param name="shadowPort">segment server port of shadow avatar; default: 4244</param>
        /// <param name="commandPort">command server port of avatar; default: 4243</param>
        /// <returns></returns>
        public int initConnections(string ipAddr, int segmentPort, int shadowPort, int commandPort)
        {
            _ipAddr = ipAddr;
            _segmentDataPort = segmentPort;
            _shadowSegmentDataPort = shadowPort;
            _commandDataPort = commandPort;

            reconnect();

            if (_segmentDataSocket == null || _commandDataSocket == null || _shadowSocket == null)
                return -1;
            return 1;
        }

        /// <summary>
        /// Request reconnection to the Unity InstantAvatar using last known (default) ip address and ports
        /// </summary>
        public void reconnect()
        {
            _segmentDataSocket = getSocket(_segmentDataPort);
            _shadowSocket = getSocket(_shadowSegmentDataPort);
            _commandDataSocket = getSocket(_commandDataPort);
        }

        private Socket getSocket(int port)
        {
            try
            {
                IPHostEntry host = Dns.GetHostEntry(_ipAddr);
                IPAddress ipAddress = host.AddressList[1];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                Socket socket = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(remoteEP);
                return socket;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// send command to avatar. Message options:<br/>
        ///  <code>camera,position</code> where position one of {front, left, right, topLeft, topRight}<br/>
        /// <code>segments,segment1,segment2,...</code> where segments are elements of {Pelvis, LeftUpperLeg, LeftLowerLeg, LeftFoot,
        /// RightUpperLeg, RightLowerLeg, RightFoot}<br/>
        /// <code>switchScene</code> toggle between male and female avatar<br/>
        /// <code>shadow,on</code> and <code>shadow,off</code> show or hide shadow avatar
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public int sendCommand(string message)
        {
            // Console.WriteLine("sending: " + message);
            try
            {
                checkCommandConnection();
                if (message.StartsWith("segments"))
                {
                    lastSegmentsMessage = message;
                }

                if (_commandDataSocket == null || !_commandDataSocket.Connected) return -1;
                int retVal = sendCmd(message);
                if (message.StartsWith("switchScene"))
                {
                    Console.WriteLine("switching scenes");
                    Thread.Sleep(100);
                    _commandDataSocket = null;
                    reconnect();

                    // op de een of andere manier krijg je niet direct een connection reset door. Versturen van 
                    // twee keer onderstaand bericht na een sleep forceert het herstel van de connectie
                    Thread.Sleep(100);
                    sendCommandWithReply("getCamPosition");
                    sendCommandWithReply("getCamPosition");
                }

                return retVal;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : {0}", e.Message);
                return -1;
            }
        }

        private void checkCommandConnection()
        {
            if (_commandDataSocket == null || !_commandDataSocket.Connected)
            {
                Console.WriteLine("reconnecting (command socket null) ");
                reconnect();
                if (_segmentDataSocket != null && lastSegmentsMessage != null)
                {
                    // if viewer has been restarted, segments need to be resent
                    sendCmd(lastSegmentsMessage);
                }
            }
        }

        private void checkSegmentConnection()
        {
            if (_segmentDataSocket == null || _shadowSocket == null || !_segmentDataSocket.Connected)
            {
                Console.WriteLine("reconnecting (segment socket null)");
                reconnect();
                if (_segmentDataSocket != null && _shadowSocket != null && lastSegmentsMessage != null)
                {
                    // if viewer has been restarted, segments need to be resent
                    sendCmd(lastSegmentsMessage);
                }
            }
        }

        private int sendCmd(string message)
        {
            int n_bytes = _commandDataSocket.Send(Encoding.ASCII.GetBytes(message + "\n"));
            Thread.Sleep(30);
            return n_bytes;
        }

        /// <summary>
        /// request-reply command. Message options: <br/>
        /// <code>getCamPosition</code> -> response: current camera position
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string sendCommandWithReply(string message)
        {
            try
            {
                checkCommandConnection();
                if (_commandDataSocket == null) return "";

                sendCmd(message);

                int n_bytes = _commandDataSocket.Receive(_data, 0, _data.Length, SocketFlags.None);
                Thread.Sleep(30);

                return System.Text.Encoding.ASCII.GetString(_data, 0, n_bytes);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : {0}", e.Message);
                return $"Exception : {e.Message}";
            }
        }

        /// <summary>
        /// send to the avatar new rotations (quaternions) to the initialized segments as list of floats;
        /// length of the array must be the
        /// 4 * the number of initialized segments; expected quaternion order: (w, x, y, z) 
        /// </summary>
        /// <param name="segment_floats"></param>
        /// <returns></returns>
        public int sendAvatarSegmentRotations(float[] segment_floats)
        {
            return sendSegmentRotations(segment_floats, _segmentDataSocket);
        }

        /// <summary>
        /// Get the rotations (quaternions) of the avatar's segments <br/>
        /// returns quaternions as a list of floats; quaternion order: (w, x, y, z)
        /// </summary>
        /// <returns>quaternions as a list of floats; quaternion order: (w, x, y, z)</returns>
        public float[] getAvatarSegmentRotations()
        {
            return getSegmentRotations("getAvatarRotations");
        }
        
        /// <summary>
        /// Get the inverted rotations (quaternions) of the avatar's segments: <br/>
        /// inv = rotation * inv(initialRotation); result = (w, -x, -z, -y)
        /// </summary>
        /// <returns>quaternions as a list of floats; quaternion order: (w, x, y, z)</returns>
        public float[] getInvertedAvatarSegmentRotations()
        {
            return getSegmentRotations("getInvertedAvatarRotations");
        }

        private float[] getSegmentRotations(string command)
        {
            checkCommandConnection();
            if (_commandDataSocket == null) return null;
            sendCmd(command);
            int n_bytes = _commandDataSocket.Receive(_data, 0, _data.Length, SocketFlags.None);
            float[] result = new float[n_bytes / 4];
            Buffer.BlockCopy(_data, 0, result, 0, n_bytes);
            return result;
        }
        
        /// <summary>
        /// send to the shadow avatar new rotations (quaternions) to the initialized segments as list of floats;
        /// length of the array must be the
        /// 4 * the number of initialized segments; expected quaternion order: (w, x, y, z) 
        /// </summary>
        /// <param name="segment_floats"></param>
        /// <returns></returns>
        public int sendShadowAvatarSegmentRotations(float[] segment_floats)
        {
            return sendSegmentRotations(segment_floats, _shadowSocket);
        }
        
        
         /// <summary>
         /// Get the rotations (quaternions) of the shadow avatar's segments<br/>
         /// returns quaternions as a list of floats; quaternion order: (w, x, y, z)
         /// </summary>
         /// <returns>quaternions as a list of floats; quaternion order: (w, x, y, z)</returns>
         public float[] getShadowAvatarSegmentRotations()
         {
             return getSegmentRotations("getShadowAvatarRotations");
         }

         /// <summary>
         /// Get the inverted rotations (quaternions) of the shadow avatar's segments: <br/>
         /// inv = rotation * inv(initialRotation); result = (w, -x, -z, -y)
         /// </summary>
         /// <returns>quaternions as a list of floats; quaternion order: (w, x, y, z)</returns>
         public float[] getInvertedShadowAvatarSegmentRotations()
         {
             return getSegmentRotations("getInvertedShadowAvatarRotations");
         }
        
        private int sendSegmentRotations(float[] segment_floats, Socket avatarSocket)
        {
            try
            {
                checkSegmentConnection();
                if (avatarSocket == null) return -1;
                int length = 4 * segment_floats.Length;
                Buffer.BlockCopy(segment_floats, 0, _segment_bytes, 0, length);
                int result = avatarSocket.Send(_segment_bytes, length, SocketFlags.None);
                // Console.WriteLine(result + " bytes sent");
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : {0}", e.Message);
                return -1;
            }
        }
    }
}