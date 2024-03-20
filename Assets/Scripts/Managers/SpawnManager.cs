using System;
using System.Linq;
using Incidents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        public static event Action<int> OnIncidentsSpawned;

        private void Start()
        {
            int incidentCount = SpawnRandomIncidents();
            OnIncidentsSpawned?.Invoke(incidentCount);
        }

        private int SpawnRandomIncidents()
        {
            IncidentBase[] allIncidents = FindObjectsByType<IncidentBase>(FindObjectsSortMode.None);
            var groupedIncidents = allIncidents.GroupBy(incident => incident.GetType());
            int incidentCount = 0;
            foreach (var group in groupedIncidents)
            {
                incidentCount++;
                IncidentBase rndIncident = group.ElementAt(Random.Range(0, group.Count()));
                rndIncident.SpawnIncident();
                foreach (IncidentBase incident in group.Where(i => i != rndIncident))
                {
                    incident.SpawnReplacement();
                }
            }

            return incidentCount;
        }
    }
}
