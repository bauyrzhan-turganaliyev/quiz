using System;
using System.Collections.Generic;
using Config;
using Data;
using TMPro;
using Tools.Alghoritm;
using Tools.UI;
using UnityEngine;
using Random = UnityEngine.Random;

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
        
        private List<QuestionData> _questionsList;
        private IShuffler<QuestionData> _shuffler;
        private GameData _gameData;

        private GameStatus _gameStatus;
        private int _questionsCount;
        private float _elapsed;

        public void Init()
        {
            _shuffler = new Shuffler<QuestionData>();
            _questionsList = new List<QuestionData>();
        }
        public void Begin()
        {
            _gameStatus = GameStatus.Game;
            
            CleanUp();
            PrepareQuestionList();
            if (_quizData.IsShuffled) ShuffleQuestionList();

            _gameData = new GameData(_gameConfig);
            _questionsCount = _quizData.QuestionsDataList.Count;
            OnUpdateGameUI?.Invoke(_gameData);

            InitQuestion(_gameData.CurrentQuestion);
        }
        public void End(bool flag)
        {
            _gameStatus = GameStatus.NonGame;
            CleanUp();
            OnGameEnd?.Invoke(flag, _gameData);
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
            
            if (_gameData.AllSeconds > _gameConfig.MaxSeconds && _gameConfig.MaxSeconds != -1) End(false);
        }
        private void InitQuestion(int questionID)
        {
            ClearButtons();
            
            _questionText.text = _questionsList[questionID].Question;
            var optionsCount = _questionsList[questionID].WrongAnswers.Count + 1;
            var correctAnswerIndex = Random.Range(0, optionsCount);
            bool isCorrectInit = false;
            
            for (int i = 0; i < optionsCount; i++)
            {
                var ID = i;
                _options[i].Button.gameObject.SetActive(true);
                _options[i].Button.interactable = true;
                
                if (i == correctAnswerIndex)
                {
                    isCorrectInit = true;
                    _options[i].Text.text = _questionsList[questionID].CorrectAnswer;
                    _options[i].Button.onClick.AddListener(OnCorrectAnswer);
                }
                else
                {
                    if (isCorrectInit) ID -= 1;
                    _options[i].Text.text = _questionsList[questionID].WrongAnswers[ID];
                    _options[i].Button.onClick.AddListener(OnWrongAnswer);
                }
            }
        }
        private void CleanUp()
        {
            ClearButtons();
            _questionText.text = "";
            _questionsList.Clear();
            _questionsCount = 0;
        }
        private void ClearButtons()
        {
            foreach (var button in _options)
            {
                button.Button.onClick.RemoveAllListeners();
                var buttonColors = button.Button.colors;
                buttonColors.normalColor = Color.white;
                button.Button.gameObject.SetActive(false);
                button.Button.interactable = false;
            }
        }
        private void OnWrongAnswer()
        {
            _gameData.LifeCount--;
            if (_gameData.LifeCount < 0)
            {
                OnGameEnd?.Invoke(false, _gameData);
            }
            _gameData.CurrentQuestion++;
            InitQuestion(_gameData.CurrentQuestion);
            
            OnUpdateGameUI?.Invoke(_gameData);
        }
        private void OnCorrectAnswer()
        {
            _gameData.CorrectAnswerCount++;
            _gameData.CurrentQuestion++;
            if (_gameData.CurrentQuestion >= _questionsCount)
            {
                End(true);
            }
            else InitQuestion(_gameData.CurrentQuestion);

            OnUpdateGameUI?.Invoke(_gameData);
        }
        private void PrepareQuestionList()
        {
            foreach (var questionData in _quizData.QuestionsDataList)
            {
                _questionsList.Add(questionData);
            }
        }
        private void ShuffleQuestionList() => _shuffler.Shuffle(_questionsList);
    }

    public enum GameStatus
    {
        NonGame,
        Game,
    }
}