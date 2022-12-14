using GameEvents;
using UnityEngine;

namespace LevelScreen
{
    [CreateAssetMenu(menuName = "Scriptable Objects/levelObject")]
    public class LevelData : ScriptableObject
    {
        public string LevelName;
        public int LevelNumber;
        [Tooltip("The level that is created in game")]
        public GameEventDataHolder GameEventDataHolderLevel;
        [Range(-250, 280)] public int HeightValue;
        public Sprite SpriteIcon;
        [Range(0, 3)] public int StarCount;
        public int StarRequirement;
        public bool FinishedLevel;
        public Texture LevelBackgroundImage;
    }
}