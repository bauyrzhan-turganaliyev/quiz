using System;
using Data;
using SaveLoad;
using UI;
using UnityEngine;

namespace Quiz
{
    public class QuizService : MonoBehaviour
    {
        [SerializeField] private QuizProcessService _quizProcessService;

        private UIService _uiService;
        private ProgressService _progressService;

        public void Init(UIService uiService, ProgressService progressService)
        {
            _uiService = uiService;
            _progressService = progressService;

            SubscribeServices();
            InitMembers();
        }

        private void SubscribeServices()
        {
            _uiService.OnStartGame += StartGame;
            _quizProcessService.OnUpdateGameUI += UpdateGameUI;
            _quizProcessService.OnGameEnd += EndGame;
        }

        private void UpdateGameUI(GameData gameData) => 
            _uiService.UpdateGameUI(gameData);

        private void StartGame() =>
            _quizProcessService.Begin();
        
        private void EndGame(bool isWin, GameData gameData)
        {
            _uiService.EndGame(isWin, gameData, _progressService.PlayerProgress.BestScore);
            
            if (_progressService.PlayerProgress.BestScore < gameData.CorrectAnswerCount && isWin)
            {
                _progressService.PlayerProgress.BestScore = gameData.CorrectAnswerCount;
                _progressService.SaveLoadService.SaveProgress(_progressService.PlayerProgress);
                _uiService.UpdateBestScore(_progressService.PlayerProgress.BestScore);
            }
        }

        private void InitMembers() =>
            _quizProcessService.Init();
    }
}