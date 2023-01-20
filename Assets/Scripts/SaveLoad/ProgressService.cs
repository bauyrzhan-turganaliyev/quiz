using Data;

namespace SaveLoad
{
    public class ProgressService
    {
        public PlayerProgress PlayerProgress { get; private set; }
        public SaveLoadService SaveLoadService { get; private set; }

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