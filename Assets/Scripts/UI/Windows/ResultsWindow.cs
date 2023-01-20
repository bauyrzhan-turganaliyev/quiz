using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class ResultsWindow : Window, IWindow, IRegister
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _timeText;
        
        [SerializeField] private Button _menuButton;

        public Action OnBackMenu;
        public void Init()
        {
            Switch(false);
        }
        
        public void Register()
        {
            _menuButton.onClick.AddListener((() => OnBackMenu?.Invoke()));
        }

        public void ShowResults(bool flag, GameData gameData, int bestScore)
        {
            _titleText.text = flag ? "You are winner!" : "You are loser!";
            _titleText.color = flag ? Color.green : Color.red;
            
            _scoreText.text = bestScore < gameData.CorrectAnswerCount 
                ? $"NEW BEST SCORE!\n{gameData.CorrectAnswerCount} score(s)" 
                : $"{gameData.CorrectAnswerCount} score(s)";
            _timeText.text = $"{gameData.Minutes} minute(s) and {gameData.Seconds} second(s)";
        }
    }
}