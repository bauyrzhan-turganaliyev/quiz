using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "Question_Data", menuName = "ScriptableObjects/Question_Data", order = 1)]
    public class QuestionData : ScriptableObject
    {
        public string Question;
        public int CorrectAnswerIndex;
        public List<string> Answers;
    }
}