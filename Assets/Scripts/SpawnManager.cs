using System.Linq;
using Incidents;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    void Start()
    {
        IncidentSpawner[] allIncidents = FindObjectsByType<IncidentSpawner>(FindObjectsSortMode.None);
        var groupedIncidents = allIncidents.GroupBy(incident => incident.GetType());
        foreach (var group in groupedIncidents)
        {
            IncidentSpawner rndIncident = group.ElementAt(Random.Range(0, group.Count()));
            rndIncident.SpawnIncident();
            foreach (IncidentSpawner incident in group.Where(i => i != rndIncident))
            {
                incident.SpawnReplacement();
            }
        }
    }
}
