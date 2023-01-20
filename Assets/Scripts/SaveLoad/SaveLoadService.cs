using Data;
using Tools.Extensions;
using UnityEngine;

namespace SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        
        public void SaveProgress(PlayerProgress playerProgress) =>
            PlayerPrefs.SetString(ProgressKey, playerProgress.ToJson());
        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<PlayerProgress>();
    }
}