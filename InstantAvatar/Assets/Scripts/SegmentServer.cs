using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

// ReSharper disable All

public class SegmentServer : MonoBehaviour
{
    public int port = 4242;

    private static int BYTES_PER_FLOAT = 4;
    private static int FLOATS_PER_QUATERNION = 4;

    private TcpListener server;
    private bool serverStarted;
    private TcpClient client;
    public SegmentMessageHandler handler;
    private byte[] data;
    private int[] floatOffsets;
    private Quaternion[] quaternions;

   private void Start()
    {
        startServer();
    }
   
    protected void startServer()
    {
        Application.runInBackground = true;

        allocateDataMemory();
        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            StartListening();
            serverStarted = true;
            // GameObject.Find("msg").GetComponent<InputField>().text = "Server " + port;
            Debug.Log("Server started on port " + port);
        }
        catch (Exception )
        {
        }
    }

    private void allocateDataMemory()
    {
        data = new byte[BYTES_PER_FLOAT * FLOATS_PER_QUATERNION * 50];
        floatOffsets = new int[FLOATS_PER_QUATERNION];
        for (int i = 0; i < floatOffsets.Length; i++)
        {
            floatOffsets[i] = i * BYTES_PER_FLOAT;
        }

        allocateQuaternions();
    }

    private void allocateQuaternions()
    {
        quaternions = new Quaternion[handler.NrActiveSegments];
        for (int i = 0; i < quaternions.Length; i++)
        {
            quaternions[i] = new Quaternion();
        }
    }

    private void Update()
    {
        if (!serverStarted)
        {
            return;
        }

        if (IsConnected(client))
        {
            NetworkStream stream = client.GetStream();
            if (stream.DataAvailable)
            {
                // Debug.Log("Data available");
                stream.Read(data, 0, data.Length);
                {
                    ProcessData();
                }
            }
        }
    }

    private void ProcessData()
    {
        if (quaternions.Length == 0)
        {
            allocateQuaternions();
        }

        convertData();
        handler.processQuaternions(quaternions);
    }

    private void convertData()
    {
        // incoming quaternions: w, x, y, z => x, y, z, w 
        for (int i_quat = 0; i_quat < handler.NrActiveSegments; i_quat++)
        {
            // Debug.Log(handler.NrActiveSegments + " - " + quaternions.Length);
            int start_index = i_quat * FLOATS_PER_QUATERNION * BYTES_PER_FLOAT;

            float w = BitConverter.ToSingle(data, start_index + floatOffsets[0]);
            float x = BitConverter.ToSingle(data, start_index + floatOffsets[1]);
            float y = BitConverter.ToSingle(data, start_index + floatOffsets[2]);
            float z = BitConverter.ToSingle(data, start_index + floatOffsets[3]);

            // convert left to right:
            // negate x;
            // swap y and z and negate
            quaternions[i_quat] = new Quaternion(-x, -z, -y, w);
        }
    }
    
    private bool IsConnected(TcpClient client)
    {
        try
        {
            if (client != null && client.Client != null && client.Client.Connected)
            {
                if (client.Client.Poll(0, SelectMode.SelectRead))
                {
                    return !(client.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }


    private void StartListening()
    {
        server.BeginAcceptTcpClient(AcceptTCPClient, server);
    }

    private void AcceptTCPClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener) ar.AsyncState;
        client = listener.EndAcceptTcpClient(ar);
        StartListening();
        Debug.Log(client + " has connected");
    }

    private void OnDestroy()
    {
        server.Stop();
    }
}