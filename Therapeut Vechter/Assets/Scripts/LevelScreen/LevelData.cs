using UnityEngine;

namespace LevelScreen
{
    [CreateAssetMenu(menuName = "ScriptableObjects/levelObject")]
    public class LevelData : ScriptableObject
    {
        public string LevelName;
        public int LevelNumber;
        [Range(-250, 280)] public int HeightValue;
        public Sprite SpriteIcon;
        [Range(0, 3)] public int StarCount;
        public int StarRequirement;
        public bool FinishedLevel;
        public Sprite LevelBackgroundImage;
    }
}