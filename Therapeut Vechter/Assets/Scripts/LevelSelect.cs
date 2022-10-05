using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/levelObject")]

public class LevelSelect : ScriptableObject
{
    public string LevelName;
    public int LevelNumber;
    public int HeightValue;
    public Sprite Images;
    public int StarCount;
}