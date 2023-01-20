using System;
using Quiz;
using SaveLoad;
using UI;
using UnityEngine;

namespace Infrastructure
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private UIService _uiService;
        [SerializeField] private QuizService _quizService;

        private ProgressService _progressService;
        private void Awake()
        {
            _progressService = new ProgressService();
            _uiService.Init(_progressService.PlayerProgress);
            _quizService.Init(_uiService, _progressService);
        }
    }
}
