using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "Game_Config", menuName = "ScriptableObjects/Game_Config", order = 1)]
    public class GameConfig : ScriptableObject
    {
        public int MaxLives;
        public int MaxSeconds;
    }
}