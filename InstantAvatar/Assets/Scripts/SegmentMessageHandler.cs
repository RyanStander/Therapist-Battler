using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable All

public class SegmentMessageHandler : MonoBehaviour
{
    [SerializeField] private Transform Pelvis = null;
    [SerializeField] private Transform LeftUpperLeg = null;
    [SerializeField] private Transform LeftLowerLeg = null;
    [SerializeField] private Transform LeftFoot = null;
    [SerializeField] private Transform RightUpperLeg = null;
    [SerializeField] private Transform RightLowerLeg = null;
    [SerializeField] private Transform RightFoot = null;

    [Header("For vertical correction")]
    [SerializeField] private Transform BaseModel = null;
    [SerializeField] private Transform LeftHeel = null;
    [SerializeField] private Transform LeftToe = null;
    [SerializeField] private Transform RightHeel = null;
    [SerializeField] private Transform RightToe = null;
    
    private Transform[] lowestTransforms;
    private float[] startHeights = new float[4];
    
    private IDictionary<string, Transform> segments;

    private Segment[] activeSegments = new Segment[0];
    private string[] lastSegmentNames = new string[0];

    public int NrActiveSegments { get; private set; }


    public SegmentMessageHandler()
    {
        CreateSegmentsDict();
        NrActiveSegments = 0;
    }
    
    private void Awake()
    {

    }

    public void InitActiveSegments(string[] segmentNames)
    {
        // debugRotations();

        if (segmentNames.SequenceEqual(lastSegmentNames))
        {
            return;
        }

        CreateSegmentsDict();
        string res = "";
        foreach (string segmentName in segmentNames)
        {
            res += segmentName + " ";
        }

        Debug.Log(res);
        NrActiveSegments = countValidSegmentNames(segmentNames);
        activeSegments = new Segment[NrActiveSegments];

        int i = 0;
        foreach (var segmentName in segmentNames)
        {
            if (segments.ContainsKey(segmentName))
            {
                activeSegments[i] = new Segment(segments[segmentName]);
                i++;
            }
        }

        lastSegmentNames = (string[]) segmentNames.Clone();

        initHeightAdjustment();
    }

    private void initHeightAdjustment()
    {
        lowestTransforms = new[] {LeftHeel, LeftToe, RightHeel, RightToe};
        for (int i = 0; i < 4; i++ )
        {
            startHeights[i] = lowestTransforms[i].position.y;
        }
    }

    private int countValidSegmentNames(string[] segmentNames)
    {
        int count = 0;
        foreach (var segmentName in segmentNames)
        {
            if (segments.ContainsKey(segmentName))
            {
                count++;
            }
        }

        return count;
    }

    private void CreateSegmentsDict()
    {
        segments = new Dictionary<string, Transform>();
        segments["LeftFoot"] = LeftFoot;
        segments["LeftLowerLeg"] = LeftLowerLeg;
        segments["LeftUpperLeg"] = LeftUpperLeg;
        segments["Pelvis/Sacrum"] = Pelvis;
        segments["RightFoot"] = RightFoot;
        segments["RightLowerLeg"] = RightLowerLeg;
        segments["RightUpperLeg"] = RightUpperLeg;
    }

    public void processQuaternions(Quaternion[] quaternions)
    {
        for (int i = 0; i < activeSegments.Length; i++)
        {
            activeSegments[i].transform.rotation = quaternions[i] * activeSegments[i].initialRotation;
        }
        adjustVerticalPosition();
    }

    public void adjustVerticalPosition()
    {
        float lowDiff = 10f;
        for (int i = 0; i < 4; i++ )
        {
            float diff = lowestTransforms[i].position.y - startHeights[i];
            if (diff < lowDiff)
            {
                lowDiff = diff;
            }
        }
        var pos = BaseModel.position;
        BaseModel.position = new Vector3(pos.x, pos.y - lowDiff, pos.z);
    }

    public Quaternion[] getRotations()
    {
        Quaternion[] rotations = new Quaternion[activeSegments.Length];
        for (int i = 0; i < activeSegments.Length; i++)
        {
            rotations[i] = activeSegments[i].transform.rotation;
        }

        return rotations;
    }
    
    public Quaternion[] getInvertedRotations()
    {
        Quaternion[] rotations = new Quaternion[activeSegments.Length];
        for (int i = 0; i < activeSegments.Length; i++)
        {
            Quaternion inverse = activeSegments[i].transform.rotation *
                                 Quaternion.Inverse(activeSegments[i].initialRotation);
            rotations[i] = new Quaternion(-inverse.x, -inverse.z, -inverse.y, inverse.w);
        }

        return rotations;
    }
}

internal class Segment
{
    public Quaternion initialRotation { get; }
    public Transform transform { get; }

    public Segment(Transform transform)
    {
        this.transform = transform;
        this.initialRotation = transform.rotation;
    }
}