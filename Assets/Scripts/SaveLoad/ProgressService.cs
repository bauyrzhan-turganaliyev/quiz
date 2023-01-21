using Data;

namespace SaveLoad
{
    public class ProgressService
    {
        public PlayerProgress PlayerProgress { get; private set; }
        public readonly SaveLoadService SaveLoadService;

        public ProgressService()
        {
            SaveLoadService = new SaveLoadService();
            LoadProgressOrInitNew();
        }

        private void LoadProgressOrInitNew() =>
            PlayerProgress = 
                SaveLoadService.LoadProgress()
                ?? new PlayerProgress();
    }
}