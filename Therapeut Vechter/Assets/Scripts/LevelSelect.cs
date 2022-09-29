using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/levelObject")]

public class LevelSelect : ScriptableObject
{
    public string LevelName;
    public int LevelCount;
    public float HeightValue;
    public Sprite[] Images;
}