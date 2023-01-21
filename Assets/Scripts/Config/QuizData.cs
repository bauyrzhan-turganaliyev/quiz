using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "Quiz_Data", menuName = "ScriptableObjects/Quiz_Data", order = 1)]
    public class QuizData : ScriptableObject
    {
        public bool IsShuffled;
        public QuestionData[] QuestionsDataList;
        
    }
}