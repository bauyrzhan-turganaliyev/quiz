using Data;
using TMPro;
using UnityEngine;

namespace UI.Windows
{
    public class GameWindow : Window, IWindow, IRegister
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private TMP_Text _livesText;

        public void Init()
        {
            Switch(false);
        }
        
        public void Register()
        {
            print("Game window registration");
        }

        public void UpdateGameUI(GameData gameData)
        {
            if (gameData == null) return;
            
            _scoreText.text = gameData.CorrectAnswerCount > 1
                ? $"{gameData.CorrectAnswerCount} scores"
                : $"{gameData.CorrectAnswerCount} score";

            _timeText.text = gameData.Minutes < 10 ? $"0{gameData.Minutes}:" : $"{gameData.Minutes}:";
            _timeText.text += gameData.Seconds < 10 ? $"0{gameData.Seconds}" : $"{gameData.Seconds}";

            _livesText.text = $"{gameData.LifeCount} life";
        }
    }
}