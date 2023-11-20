using UnityEngine;

namespace Incidents
{
    public abstract class BaseIncident : MonoBehaviour
    {
        [SerializeField] private GameObject incidentInstigator;
        [SerializeField] private GameObject incidentReplacement;

        public void SpawnIncident()
        {
            Instantiate(incidentInstigator, transform.position, transform.rotation);
        }

        public void SpawnReplacement()
        {
            if (incidentReplacement != null)
                Instantiate(incidentReplacement, transform.position, transform.rotation);
        }
    }
}
