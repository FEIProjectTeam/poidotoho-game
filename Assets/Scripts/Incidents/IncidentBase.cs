using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Incidents
{
    public abstract class IncidentBase : MonoBehaviour
    {
        [SerializeField] public List<QuestionAndAnswers> qnaList;
        int randomIntInRange;
        bool isAnswered = false;      

        private void Start()
        {
            if (qnaList != null)
            {
                randomIntInRange = Random.Range(0, qnaList.Count);
            }
        }

        public void addQuestionAndAnswer(List<QuestionAndAnswers> qnaList )
        {
            this.qnaList = qnaList;        
        }

        public void OnMouseDown()
        {
            if ( !isAnswered )
            {
                GameManager.Instance.openQNAPopup.Invoke(this);
            }
        }

        public string getQuestion()
        {      
            return qnaList[randomIntInRange].question.ToString();
        }

        public List<string> getAnswers()
        {
            return qnaList[randomIntInRange].answers;
        }

        public List<int> getCorrectAnswers()
        {
            return qnaList[randomIntInRange].correctAnswers;
        }
        
        public void setAnswered()
        {
            isAnswered = true;
        }
    }
}
