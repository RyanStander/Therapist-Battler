using System;
using System.Threading;
using System.Threading.Tasks;
using UnityConnector;

namespace UConnTester
{
    internal class Program
    {
        static DataSender sender = new DataSender();

        static string[] bodyparts =
        {
            "LeftFoot", "LeftLowerLeg", "LeftUpperLeg", "Pelvis/Sacrum", "RightFoot", "RightLowerLeg", "RightUpperLeg"
        };


        public static void Main(string[] args)
        {
            if (sender.initConnections("localhost", 4242, 4244, 4243) < 0)
            {
                Console.WriteLine("Connection failed; exit...");
                return;
            }

            sendSegments();

            string instructions = "wasd-qe for camera control\n" +
                                  "C for current camera position\n" +
                                  "Y read and send movement data\n" +
                                  "U read and send longer movement data\n" +
                                  "N show shadow avatar\n" +
                                  "M hide shadow avatar\n" +
                                  "I to switch scene\n" +
                                  "R to print avatar rotations (T: shadow)\n" + 
                                  "H to print avatar inverted rotations (J: shadow)\n" + 
                                  "X to eXit";
            Console.WriteLine(instructions);
            
            Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);

            Console.Write("Command: ");
            ConsoleKey input = Console.ReadKey(true).Key;
            while (input != ConsoleKey.X)
            {
                int result = processInput(input);
                if (result == 0)
                {
                    Console.WriteLine("Unknown input");
                    Console.WriteLine(instructions);
                }
                else if (result < 0)
                {
                    Console.WriteLine("Could not send message");
                    Console.WriteLine(instructions);
                }

                input = Console.ReadKey(true).Key;
            }
        }

        private static void sendSegments()
        {
            string cmd = "segments";
            foreach (string bodypart in bodyparts)
            {
                cmd += "," + bodypart;
            }

            sender.sendCommand(cmd);
        }

        private static int processInput(ConsoleKey input)
        {
            switch (input)
            {
                case ConsoleKey.W:
                    return sender.sendCommand("camera,front");
                case ConsoleKey.A:
                    return sender.sendCommand("camera,left");
                case ConsoleKey.D:
                    return sender.sendCommand("camera,right");
                case ConsoleKey.S:
                    return sender.sendCommand("camera,front");
                case ConsoleKey.Q:
                    return sender.sendCommand("camera,topLeft");
                case ConsoleKey.E:
                    return sender.sendCommand("camera,topRight");
                case ConsoleKey.C:
                    string reply = sender.sendCommandWithReply("getCamPosition");
                    Console.WriteLine("Camera position: " + reply);
                    return reply.Length;
                case ConsoleKey.Y:
                    sendWalkingData("walking_alt_1.csv");
                    break;
                case ConsoleKey.U:
                    sendWalkingData("walking_alt_.csv");
                    break;
                case ConsoleKey.N:
                    return sender.sendCommand("shadow,on");
                case ConsoleKey.M:
                    return sender.sendCommand("shadow,off");
                case ConsoleKey.R:
                    printRotations(sender.getAvatarSegmentRotations());
                    return 1;
                case ConsoleKey.T:
                    printRotations(sender.getShadowAvatarSegmentRotations());
                    return 1;
                case ConsoleKey.H:
                    printRotations(sender.getInvertedAvatarSegmentRotations());
                    return 1;
                case ConsoleKey.J:
                    printRotations(sender.getInvertedShadowAvatarSegmentRotations());
                    return 1;
            }

            return -1;
        }

        private static void printRotations(float[] rotations)
        {
            for (int i = 0; i < rotations.Length / 4; i++)
            {
                int offset = 4 * i;
                Console.WriteLine("({0:f}, {1:f}, {2:f}, {3:f})", rotations[offset], rotations[offset + 1],
                    rotations[offset + 2], rotations[offset + 3]);
            }
        }

        private static void sendWalkingData(string walk_csv)
        {
            Task.Factory.StartNew(() => { sendWalkingDataThreaded(walk_csv); });
        }

        private static void sendWalkingDataThreaded(string walk_csv)
        {
            string path = "../../fusion_clinician_files/";
            string csv_file = path + walk_csv;
            Console.WriteLine("reading: " + csv_file);
            string segment_file = path + "walking_segments.csv";
            Console.WriteLine(segment_file);
            string[] segment_lines = System.IO.File.ReadAllLines(@segment_file);

            
            sender.sendCommand("segments," + segment_lines[0]);
            Thread.Sleep(100);


            string[] data_lines = System.IO.File.ReadAllLines(@csv_file);
            float[] data = new float[data_lines[0].Split(',').Length];

            float oldtime = 0;
            float timediff = 0;

            Console.WriteLine("Start sending data!");
            Console.WriteLine();
            foreach (string line in data_lines)
            {
                string[] splt = line.Split(',');
                float nwTime = float.Parse(splt[0]);
                if (oldtime == 0)
                {
                    timediff = 0;
                }
                else
                {
                    timediff = nwTime - oldtime;
                }

                oldtime = nwTime;
                for (int i = 1; i < splt.Length; i++)
                {
                    data[i - 1] = float.Parse(splt[i]);
                }

                Thread.Sleep((int) (timediff * 1000));
                // Console.WriteLine(splt[0]);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine($"{nwTime:  ###.###}");
                sender.sendAvatarSegmentRotations(data);
                sender.sendShadowAvatarSegmentRotations(data);
            }

            Console.WriteLine("Finished sending data!");
        }
    }
}