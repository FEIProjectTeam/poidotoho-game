using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Incidents
{
    public abstract class IncidentBase : MonoBehaviour
    {
        [SerializeField] private GameObject[] incidents;
        [SerializeField] private GameObject[] incidentReplacements;
        [SerializeField] private bool isSpawner = false;
        private List<QuestionAndAnswers> qnaList;
        int randomIntInRange;
        bool isAnswered = false;      
        
        private void Awake()
        {
            if (isSpawner)
            {
                GetComponent<MeshRenderer>().enabled = false;   
            }
        }
        
        private void Start()
        {
            if (qnaList != null)
            {
                randomIntInRange = Random.Range(0, qnaList.Count);
            }
        }

        public void SpawnIncident()
        {
            GameObject incident = Instantiate(incidents[Random.Range(0, incidents.Length)], transform.position, transform.rotation);
            incident.AddComponent(GetType());
        }

        public void SpawnReplacement()
        {
            if (incidentReplacements != null && incidentReplacements.Length > 0)
                Instantiate(incidentReplacements[Random.Range(0, incidentReplacements.Length)], transform.position,
                    transform.rotation);
            this.enabled = false;
        }

        public void addQuestionAndAnswer(List<QuestionAndAnswers> qnaList)
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
