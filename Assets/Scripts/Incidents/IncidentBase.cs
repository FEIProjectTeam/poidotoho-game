using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Incidents
{
    public abstract class IncidentBase : MonoBehaviour
    {
        [SerializeField] private GameObject[] incidentInstigators;
        [SerializeField] private GameObject[] incidentReplacements;
        [SerializeField] public List<QuestionAndAnswers> qnaList;
        int randomIntInRange;

        private void Awake()
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        
        private void Start()
        {
            randomIntInRange = Random.Range(0, qnaList.Count);
        }

        public void SpawnIncident()
        {
            Instantiate(incidentInstigators[Random.Range(0, incidentInstigators.Length)], transform.position, transform.rotation);
        }

        public void SpawnReplacement()
        {
            if (incidentReplacements != null && incidentReplacements.Length > 0)
                Instantiate(incidentReplacements[Random.Range(0, incidentReplacements.Length)], transform.position, transform.rotation);
        }
        
        public void addQuestionAndAnswer(List<QuestionAndAnswers> qnaList )
        {
            this.qnaList = qnaList;        
        }

        public void OnMouseDown()
        {
            GameManager.Instance.openQNAPopup.Invoke(this);
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
    }
}
