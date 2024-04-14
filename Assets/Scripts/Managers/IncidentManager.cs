using System;
using System.Linq;
using Incidents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class IncidentManager : MonoBehaviour
    {
        public static event Action<int> OnIncidentsSpawned;
        private int _incidentCount;

        private void OnEnable()
        {
            IncidentBase.OnQuizAnswered += HandleQuizAnswered;
        }

        private void OnDisable()
        {
            IncidentBase.OnQuizAnswered -= HandleQuizAnswered;
        }

        private void Start()
        {
            _incidentCount = SpawnRandomIncidents();
            if (_incidentCount == 0)
            {
                Debug.LogError("Error: No incidents were spawned.");
                return;
            }

            OnIncidentsSpawned?.Invoke(_incidentCount);
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

        private void HandleQuizAnswered()
        {
            _incidentCount--;
            if (_incidentCount == 0)
            {
                GameManager.Instance.UpdateGameState(GameManager.GameState.LevelFinished);
            }
        }
    }
}
