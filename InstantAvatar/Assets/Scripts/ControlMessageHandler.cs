using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

// ReSharper disable All

public class ControlMessageHandler : MonoBehaviour
{
    [SerializeField] private Camera frontCam = null;
    [SerializeField] private Camera leftCam = null;
    [SerializeField] private Camera rightCam = null;
    [SerializeField] private Camera topLeftCam = null;
    [SerializeField] private Camera topRightCam = null;

    [SerializeField] private GameObject shadowAvatar = null;

    private Camera activeCam;

    [SerializeField] private SegmentMessageHandler segmentMessageHandler;
    [SerializeField] private ShadowSegmentMessageHandler shadowMessageHandler;


    private Dictionary<string, Camera> cameras = new Dictionary<string, Camera>();

    private string[]
        sceneNames = new[] {"FemaleLegs", "MaleLegs"}; //{"MaleScene", "MaleLegs", "FemaleScene", "FemaleLegs"};

    private int currentSceneNr = 0;

    private byte[] _data = new byte[1024];

    private void Start()
    {
        // Debug.Log("Starting scene " + SceneManager.GetActiveScene().name);
        fillCameras();
        if (shadowAvatar != null)
        {
            shadowAvatar.SetActive(false);
        }
        currentSceneNr = Array.IndexOf(sceneNames, SceneManager.GetActiveScene().name);

        foreach (Camera cam in cameras.Values)
        {
            if (cam != null)
                cam.gameObject.SetActive(false);
        }

        activeCam = frontCam;
        moveCam("front");
    }

    private void fillCameras()
    {
        cameras["front"] = frontCam;
        cameras["left"] = leftCam;
        cameras["right"] = rightCam;
        cameras["topLeft"] = topLeftCam;
        cameras["topRight"] = topRightCam;
    }

    public void processMessage(string data, TcpClient client)
    {
        // try
        // {
            string[] splt = data.Split(',');
            if (splt.Length > 0)
            {
                Debug.Log(splt[0]);
                switch (splt[0])
                {
                    case "camera":
                        if (splt.Length > 1)
                            moveCam(splt[1]);
                        break;
                    case "segments":
                        segmentMessageHandler.InitActiveSegments(splt.Skip(1).ToArray());
                        shadowMessageHandler.InitActiveSegments(splt.Skip(1).ToArray());
                        break;
                    case "getCamPosition":
                        client.Client.Send(Encoding.ASCII.GetBytes(activeCam.name));
                        break;
                    case "switchScene":
                        currentSceneNr = (currentSceneNr + 1) % sceneNames.Length;
                        SceneManager.LoadScene(sceneNames[currentSceneNr]);
                        break;
                    case "shadow":
                        if (splt.Length > 1)
                            if (splt[1].Equals("on")) shadowAvatar.SetActive(true);
                            else if (splt[1].Equals("off")) shadowAvatar.SetActive(false);
                        break;
                    case "getAvatarRotations":
                        SendRotations(client, segmentMessageHandler.getRotations());
                        break;
                    case "getShadowAvatarRotations":
                        SendRotations(client, shadowMessageHandler.getRotations());
                        break;
                    case "getInvertedAvatarRotations":
                        SendRotations(client, segmentMessageHandler.getInvertedRotations());
                        break;
                    case "getInvertedShadowAvatarRotations":
                        SendRotations(client, shadowMessageHandler.getInvertedRotations());
                        break;
                }
            }
        // }
        // catch (Exception ex)
        // {
        //     Debug.Log(ex.StackTrace);
        // }
    }

    private void SendRotations(TcpClient client, Quaternion[] rotations)
    {
        // Debug.Log("sending rotations");
        float[] floats = new float[rotations.Length * 4];
        for (int i = 0; i < rotations.Length; i++)
        {
            // Debug.Log(rotations[i]);
            floats[i * 4] = rotations[i].w;
            floats[i * 4 + 1] = rotations[i].x;
            floats[i * 4 + 2] = rotations[i].y;
            floats[i * 4 + 3] = rotations[i].z;
        }

        int length = 4 * floats.Length;
        Buffer.BlockCopy(floats, 0, _data, 0, length);
        client.Client.Send(_data, 0, length, SocketFlags.None);
    }

    private void moveCam(string camPosition)
    {
        activeCam.gameObject.SetActive(false);
        if (cameras.ContainsKey(camPosition))
        {
            Camera nwCam = cameras[camPosition];
            if (nwCam != null)
            {
                activeCam.gameObject.SetActive(false);
                activeCam = nwCam;
                activeCam.gameObject.SetActive(true);
            }
        }
    }
}