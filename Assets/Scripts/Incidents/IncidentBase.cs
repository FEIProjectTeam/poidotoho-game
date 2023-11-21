using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Incidents
{
    public abstract class IncidentBase : MonoBehaviour
    {
        [SerializeField] private GameObject[] incidentInstigators;
        [SerializeField] private GameObject[] incidentReplacements;

        private void Awake()
        {
            GetComponent<MeshRenderer>().enabled = false;
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
    }
}
