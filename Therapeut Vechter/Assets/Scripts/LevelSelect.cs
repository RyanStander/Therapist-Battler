using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/levelObject")]

public class LevelSelect : ScriptableObject
{
    public string LevelName;
    public int LevelNumber;
    [Range(25,125)]
    public int HeightValue;
    public Sprite SpriteIcon;
    [Range(0, 3)]
    public int StarCount;
    //public GameObject LevelPrefab;
}