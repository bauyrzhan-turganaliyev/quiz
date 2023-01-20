using Config;

namespace Data
{
    public class GameData
    {
        public int CurrentQuestion;
        public int CorrectAnswerCount;
        public int LifeCount;
        public int AllSeconds;
        public int Seconds;
        public int Minutes;
        

        public GameData(GameConfig gameConfig)
        {
            CurrentQuestion = 0;
            CorrectAnswerCount = 0;
            LifeCount = gameConfig.MaxLives;
            AllSeconds++;
            Seconds = 0;
            Minutes = 0;
            
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
    }
}