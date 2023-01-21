using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class ResultsWindow : Window, IRegister
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _timeText;
        
        [SerializeField] private Button _menuButton;

        public Action OnBackMenu;
        public void Register()
        {
            _menuButton.onClick.AddListener(() => OnBackMenu?.Invoke());
        }

        public void ShowResults(bool isWin, GameData gameData, int bestScore)
        {
            _titleText.text = isWin ? "You are winner!" : "You are loser!";
            _titleText.color = isWin ? Color.green : Color.red;
            
            _scoreText.text = bestScore < gameData.CorrectAnswerCount 
                ? $"NEW BEST SCORE!\n{gameData.CorrectAnswerCount} score(s)" 
                : $"{gameData.CorrectAnswerCount} score(s)";
            _scoreText.text += bestScore < gameData.CorrectAnswerCount && !isWin ? " but you lost" : "";
            _timeText.text = $"{gameData.Minutes} minute(s) and {gameData.Seconds} second(s)";
        }
    }
}