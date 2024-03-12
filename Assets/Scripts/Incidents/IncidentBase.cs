using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Incidents
{
    public abstract class IncidentBase : MonoBehaviour
    {

        [SerializeField] private GameObject[] incidents;
        [SerializeField] private GameObject[] incidentReplacements;
        [SerializeField] private bool isSpawner;
        private bool _isQuizAnswered;
        int randomIntInRange;

        public QNAData qna;
        public static event Action<IncidentBase> IncidentFound;

        private void Awake()
        {
            if (isSpawner)
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
            qna = new QNAData();
            qna.question = "Ak� je pravdepodobnos�, �e poistenie kryje n�klady na �kodu sp�soben� zr�kou s lampou?";
            qna.correctAnswers = new List<string>() { "100%, poistenie v�dy kryje �kodu sp�soben� zr�kou s lampou." };
            qna.wrongAnswers = new List<string>() {
                "100%, poistenie v�dy kryje �kodu sp�soben� zr�kou s lampou.",
                "100%, poistenie v�dy kryje �kodu sp�soben� zr�kou s lampou. Dlha odpove�, mo�no aj na viac riadkov.",
                "100%, poistenie v�dy kryje �kodu sp�soben� zr�kou s lampou." };
        }

        private void Start()
        {

        }

        public void SpawnIncident()
        {
            GameObject incident = Instantiate(incidents[Random.Range(0, incidents.Length)], transform.position, transform.rotation, transform.parent);
            incident.AddComponent(GetType());
            Destroy(gameObject);
        }

        public void SpawnReplacement()
        {
            if (incidentReplacements != null && incidentReplacements.Length > 0)
                Instantiate(incidentReplacements[Random.Range(0, incidentReplacements.Length)], transform.position,
                    transform.rotation);
            Destroy(gameObject);
        }

        private void OnMouseDown()
        {
            if (_isQuizAnswered || GameManager.Instance.isQuizOpened)
                return;
            IncidentFound?.Invoke(this);
        }

        public void SetQuizAnswered()
        {
            _isQuizAnswered = true;
        }
    }
}
