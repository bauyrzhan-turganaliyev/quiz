using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class MainMenuWindow : Window,  IWindow, IRegister
    {
        [SerializeField] private TMP_Text _bestScoreText;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;

        public Action<bool> OnGameSwitch;
        public void Init()
        {
            Switch(false);
        }

        public void Register()
        {
            print("Register Main Menu");
            _startButton.onClick.AddListener(() => OnGameSwitch?.Invoke(true));
            _exitButton.onClick.AddListener(() => OnGameSwitch?.Invoke(false));
        }

        public void UpdateBestScore(int bestScore)
        {
            _bestScoreText.text = $"THE BEST SCORE: {bestScore}";
        }
    }
}