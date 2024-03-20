using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Incidents
{
    public abstract class IncidentBase : MonoBehaviour
    {
        public static event Action<IncidentBase> OnIncidentFound;
        public QNAData qna;

        [SerializeField]
        private GameObject[] _incidents;

        [SerializeField]
        private GameObject[] _incidentReplacements;

        [SerializeField]
        private bool _isSpawner;

        private bool _isQuizAnswered;

        private void Awake()
        {
            if (_isSpawner)
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
            // tmp for testing
            qna = new QNAData
            {
                question =
                    "Aká je pravdepodobnosť, že poistenie kryje náklady na škodu spôsobené zrážkou s lampou?",
                correctAnswers = new List<string>()
                {
                    "100%, poistenie vody kryje škodu spôsobenú zrážkou s lampou.1"
                },
                wrongAnswers = new List<string>()
                {
                    "100%, poistenie v�dy kryje �kodu sp�soben� zr�kou s lampou.2",
                    "100%, poistenie v�dy kryje �kodu sp�soben� zr�kou s lampou. Dlha odpove�, mo�no aj na viac riadkov.3",
                    "100%, poistenie v�dy kryje �kodu sp�soben� zr�kou s lampou.4"
                }
            };
        }

        public void SpawnIncident()
        {
            GameObject incident = Instantiate(
                _incidents[Random.Range(0, _incidents.Length)],
                transform.position,
                transform.rotation,
                transform.parent
            );
            incident.AddComponent(GetType());
            Destroy(gameObject);
        }

        public void SpawnReplacement()
        {
            if (_incidentReplacements != null && _incidentReplacements.Length > 0)
                Instantiate(
                    _incidentReplacements[Random.Range(0, _incidentReplacements.Length)],
                    transform.position,
                    transform.rotation
                );
            Destroy(gameObject);
        }

        private void OnMouseUpAsButton()
        {
            if (_isQuizAnswered || GameManager.Instance.State != GameManager.GameState.RoamingMap)
                return;
            OnIncidentFound?.Invoke(this);
        }

        public void SetQuizAnswered()
        {
            _isQuizAnswered = true;
        }
    }
}
