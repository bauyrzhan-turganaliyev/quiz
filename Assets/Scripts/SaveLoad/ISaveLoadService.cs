using Data;

namespace SaveLoad
{
    public interface ISaveLoadService
    {
        void SaveProgress(PlayerProgress playerProgress);
        PlayerProgress LoadProgress();
    }
}