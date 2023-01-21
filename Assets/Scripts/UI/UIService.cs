using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UI.Windows;
using UnityEngine;

namespace UI
{
    public class UIService : MonoBehaviour
    {
        [SerializeField] private List<Window> _windowsList;
        
        public Action OnStartGame;

        private MainMenuWindow _mainMenuWindow;
        private GameWindow _gameWindow;
        private ResultsWindow _resultsWindow;
        
        private PlayerProgress _playerProgress;

        public void Init(PlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
            
            SetupWindows();
            RegisterWindows();
            SwitchWindows(WindowType.MainMenu);
        }

        private void StartGame()
        {
            SwitchWindows(WindowType.Game);
            OnStartGame?.Invoke();
        }
        public void UpdateGameUI(GameData gameData) => 
            _gameWindow.UpdateGameUI(gameData);
        public void EndGame(bool flag, GameData gameData, int bestScore)
        {
            SwitchWindows(WindowType.Results);
            _resultsWindow.ShowResults(flag, gameData, bestScore);
        }
        public void UpdateBestScore(int bestScore) => 
            _mainMenuWindow.UpdateBestScore(bestScore);
        private void SetupWindows()
        {
            _mainMenuWindow = GetWindow(WindowType.MainMenu) as MainMenuWindow;
            _gameWindow = GetWindow(WindowType.Game) as GameWindow;
            _resultsWindow = GetWindow(WindowType.Results) as ResultsWindow;
        }
        private void RegisterWindows()
        {
            foreach (var window in _windowsList)
                if (window is IRegister register)
                    register.Register();

            _mainMenuWindow.UpdateBestScore(_playerProgress.BestScore);
            _mainMenuWindow.OnGameSwitch += SwitchGame;

            _resultsWindow.OnBackMenu += () => SwitchWindows(WindowType.MainMenu); 
        }
        private void SwitchGame(bool isStart)
        {
            if (isStart) StartGame();
            else Application.Quit();
        }
        private void SwitchWindows(WindowType windowType)
        {
            foreach (var window in _windowsList) 
                window.Switch(window.WindowType == windowType);
        }
        private Window GetWindow(WindowType windowType) =>
            _windowsList.FirstOrDefault(window => window.WindowType == windowType);
    }
}