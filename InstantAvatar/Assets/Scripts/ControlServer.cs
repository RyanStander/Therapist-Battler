using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

// ReSharper disable All

public class ControlServer : MonoBehaviour
{
    public int port = 4243;

    private TcpListener server;
    private bool serverStarted;
    private TcpClient client;
    public ControlMessageHandler handler;

    private void Start()
    {
        Application.runInBackground = true;
        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            StartListening();
            serverStarted = true;
            // GameObject.Find("msg").GetComponent<InputField>().text = "Server " + port;
            Debug.Log("ControlServer started on port " + port);
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e.Message);
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
                // Debug.Log("Control Data available");
                StreamReader reader = new StreamReader(stream, true);
                string data = reader.ReadLine();

                if (data != null)
                {
                    handler.processMessage(data, client);
                }
            }
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