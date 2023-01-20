using System;
using System.Collections.Generic;
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
            SwitchWindows(WindowType.MainMenu);
            
            _mainMenuWindow = GetWindow(WindowType.MainMenu) as MainMenuWindow;
            _gameWindow = GetWindow(WindowType.Game) as GameWindow;
            _resultsWindow = GetWindow(WindowType.Results) as ResultsWindow;
        }
        private void OnGameStarted()
        {
            SwitchWindows(WindowType.Game);
            OnStartGame?.Invoke();
        }        
        public void UpdateGameUI(GameData gameData)
        {
            _gameWindow.UpdateGameUI(gameData);
        }
        public void OnGameEnded(bool flag, GameData gameData, int bestScore)
        {
            SwitchWindows(WindowType.Results);
            _resultsWindow.ShowResults(flag, gameData, bestScore);
        }
        public void UpdateBestScore(int bestScore)
        {
            _mainMenuWindow.UpdateBestScore(bestScore);
        }
        private void SetupWindows()
        {
            foreach (var window in _windowsList)
            {
                switch (window)
                {
                    case MainMenuWindow mainMenu:
                        InitWindow(mainMenu);
                        RegisterWindow(mainMenu);

                        mainMenu.UpdateBestScore(_playerProgress.BestScore);
                        mainMenu.OnGameSwitch += SwitchGame; 
                        break;
                    case GameWindow game:
                        InitWindow(game);
                        RegisterWindow(game);
                        break;
                    case ResultsWindow results:
                        InitWindow(results);
                        RegisterWindow(results);
                        
                        results.OnBackMenu += (() => SwitchWindows(WindowType.MainMenu)); 
                        break;
                }
            }
        }
        private void SwitchGame(bool flag)
        {
            if (flag)
            {
                OnGameStarted();
            }
            else Application.Quit();
        }
        private void InitWindow(IWindow window) => window.Init();
        private void RegisterWindow(IRegister registrant) => registrant.Register();
        private void SwitchWindows(WindowType windowType)
        {
            foreach (var window in _windowsList)
            {
                window.Switch(window.WindowType == windowType);
            }
        }
        private Window GetWindow(WindowType windowType)
        {
            foreach (Window window in _windowsList)
            {
                if (window.WindowType == windowType) return window;
            }

            return null;
        }



    }
}