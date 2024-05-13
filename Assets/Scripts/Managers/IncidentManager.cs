using System;
using System.Linq;
using Incidents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class IncidentManager : MonoBehaviour
    {
        public static IncidentManager Instance { get; private set; }
        public static event Action<int> OnIncidentsSpawned;
        private int _incidentCount;

        private void OnEnable()
        {
            GameManager.OnGameStateChanged += SpawnIncidents;
            IncidentBase.OnQuizAnswered += HandleQuizAnswered;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChanged -= SpawnIncidents;
            IncidentBase.OnQuizAnswered -= HandleQuizAnswered;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        private void SpawnIncidents(GameManager.GameState gameState)
        {
            if (gameState != GameManager.GameState.LevelLoaded)
                return;
            IncidentBase[] allIncidents = FindObjectsByType<IncidentBase>(FindObjectsSortMode.None);
            var groupedIncidents = allIncidents.GroupBy(incident => incident.GetType());
            _incidentCount = 0;
            foreach (var group in groupedIncidents)
            {
                _incidentCount++;
                IncidentBase rndIncident = group.ElementAt(Random.Range(0, group.Count()));
                rndIncident.SpawnIncident();
                foreach (IncidentBase incident in group.Where(i => i != rndIncident))
                {
                    incident.SpawnReplacement();
                }
            }

            if (_incidentCount == 0)
                Debug.LogError("Error: No incidents were spawned.");

            OnIncidentsSpawned?.Invoke(_incidentCount);
            GameManager.Instance.UpdateGameState(GameManager.GameState.RoamingMap);
            AIManager.Instance.startSpawning();
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
