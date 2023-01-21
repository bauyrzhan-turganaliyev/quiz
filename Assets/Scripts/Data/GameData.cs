using Config;

namespace Data
{
    public class GameData
    {
        public int CurrentQuestion {get; set;}
        public int CorrectAnswerCount {get; set;}
        public int LifeCount {get; set;}
        public int AllSeconds {get; private set;}
        public int Seconds {get; private set;}
        public int Minutes {get; private set;}
        
        private readonly GameConfig _gameConfig;
        
        public GameData(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            Reset();
        }


        public void AddSecond()
        {
            AllSeconds++;
            Seconds++;
            if (Seconds >= 60)
            {
                Minutes++;
                Seconds = 0;
            }
        }

        public void Reset()
        {
            CurrentQuestion = 0;
            CorrectAnswerCount = 0;
            LifeCount = _gameConfig.MaxLives;
            AllSeconds = 0;
            Seconds = 0;
            Minutes = 0;
        }
    }
}