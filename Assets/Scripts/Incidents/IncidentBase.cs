using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Incidents
{
    public abstract class IncidentBase : MonoBehaviour
    {
        public static event Action<IncidentBase> OnIncidentFound;
        public static event Action OnQuizAnswered;
        public QNAData ActiveQNA { get; private set; }

        protected virtual List<QNAData> QNAs { get; set; }

        [SerializeField]
        private GameObject[] _incidents;

        [SerializeField]
        private GameObject[] _incidentReplacements;

        [SerializeField]
        private GameObject[] _removeOnSpawn;

        [SerializeField]
        private bool _isSpawner;

        private bool _isQuizAnswered;

        protected virtual void Awake()
        {
            if (_isSpawner)
            {
                GetComponent<MeshRenderer>().enabled = false;
            }

            ActiveQNA = null;
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
            foreach (GameObject obj in _removeOnSpawn)
            {
                Road road = obj.transform.parent.GetComponent<Road>();
                if (road != null)
                    road.removeOutPoint(obj);

                Crossroad crossroad = obj.transform.parent.GetComponent<Crossroad>();
                if (crossroad != null)
                    Destroy(crossroad);

                Destroy(obj);
            }
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
            if (ActiveQNA == null)
            {
                var rndIdx = Random.Range(0, QNAs.Count);
                ActiveQNA = QNAs[rndIdx];
            }

            OnIncidentFound?.Invoke(this);
        }

        public void SetQuizAnswered()
        {
            _isQuizAnswered = true;
            OnQuizAnswered?.Invoke();
        }
    }
}
