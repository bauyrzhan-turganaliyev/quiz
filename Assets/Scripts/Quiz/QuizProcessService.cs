using System;
using System.Collections.Generic;
using Config;
using Data;
using TMPro;
using Tools.Algorithm;
using Tools.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Quiz
{
    public class QuizProcessService : MonoBehaviour, IQuizProcessService
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private QuizData _quizData;
        
        [SerializeField] private TMP_Text _questionText;
        [SerializeField] private List<CustomButton> _options;

        public Action<GameData> OnUpdateGameUI;
        public Action<bool, GameData> OnGameEnd;
        
        private QuestionData[] _questionsList;
        private Shuffler<QuestionData> _shuffler;
        private GameData _gameData;

        private GameStatus _gameStatus;
        private int _questionsCount;

        private float _elapsed;

        public void Init()
        {
            _shuffler = new Shuffler<QuestionData>();
            _questionsList = _quizData.QuestionsDataList;
            _questionsCount = _questionsList.Length;
            SetupGameConfig();
            _gameData = new GameData(_gameConfig);
        }
        
        public void Begin()
        {
            _gameStatus = GameStatus.Game;
            
            CleanUp();
            _gameData.Reset();
            if (_quizData.IsShuffled) 
                ShuffleQuestionList();

            OnUpdateGameUI?.Invoke(_gameData);

            InitQuestion(_gameData.CurrentQuestion);
        }
        public void End(bool isWin)
        {
            _gameStatus = GameStatus.NonGame;
            OnGameEnd?.Invoke(isWin, _gameData);
        }
        private void Update()
        {
            if (_gameStatus == GameStatus.NonGame) return;
            _elapsed += Time.deltaTime;
            if (_elapsed >= 1f) {
                _elapsed  %= 1f;
                OutputTime();
            }
        }
        private void OutputTime()
        {
            _gameData.AddSecond();
            OnUpdateGameUI?.Invoke(_gameData);
            
            if (_gameData.AllSeconds > _gameConfig.MaxSeconds) 
                End(false);
        }

        private void InitQuestion(int questionID)
        {
            ClearButtons();

            _questionText.text = _questionsList[questionID].Question;
            var correctAnswerIndex = _questionsList[questionID].CorrectAnswerIndex;
            int i = 0;
            foreach (var answer in _questionsList[questionID].Answers)
            {
                int j = i;
                _options[i].Button.gameObject.SetActive(true);
                _options[i].Button.interactable = true;
                _options[i].Text.text = answer;
                _options[i].Button.onClick.AddListener(() => CalculateAnswer(j==correctAnswerIndex));
                i++;
            }
            
        }

        private void CleanUp() =>
            ClearButtons();
        private void ClearButtons()
        {
            foreach (var button in _options)
            {
                button.Button.onClick.RemoveAllListeners();
                button.Button.gameObject.SetActive(false);
                button.Button.interactable = false;
            }
        }

        private void CalculateAnswer(bool isCorrect)
        {
            if (isCorrect)
                _gameData.CorrectAnswerCount++;
            else
            {
                _gameData.LifeCount--;
                if (_gameData.LifeCount < 0)
                {
                    End(false);
                    return;
                }
            }
            
            _gameData.CurrentQuestion++;
            if (_gameData.CurrentQuestion >= _questionsCount)
            {
                End(true);
                return;
            }

            InitQuestion(_gameData.CurrentQuestion);
            
            OnUpdateGameUI?.Invoke(_gameData);
        }
        private void SetupGameConfig()
        {
            if (_gameConfig.MaxLives < 0) _gameConfig.MaxLives = 0;
            if (_gameConfig.MaxSeconds < 10) _gameConfig.MaxSeconds = int.MaxValue;
        }
        private void ShuffleQuestionList() => 
            _shuffler.Shuffle(_questionsList);
    }

    public enum GameStatus
    {
        NonGame,
        Game,
    }
}