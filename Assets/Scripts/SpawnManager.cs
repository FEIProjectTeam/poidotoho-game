using System.Linq;
using Incidents;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private void Start()
    {
        SpawnRandomIncidents();
    }

    private void SpawnRandomIncidents()
    {
        IncidentBase[] allIncidents = FindObjectsByType<IncidentBase>(FindObjectsSortMode.None);
        var groupedIncidents = allIncidents.GroupBy(incident => incident.GetType());
        foreach (var group in groupedIncidents)
        {
            IncidentBase rndIncident = group.ElementAt(Random.Range(0, group.Count()));
            rndIncident.SpawnIncident();
            foreach (IncidentBase incident in group.Where(i => i != rndIncident))
            {
                incident.SpawnReplacement();
            }
        }
    }
}
