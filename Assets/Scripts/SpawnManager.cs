using System.Linq;
using Incidents;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    void Start()
    {
        BaseIncident[] allIncidents = FindObjectsByType<BaseIncident>(FindObjectsSortMode.None);
        var groupedIncidents = allIncidents.GroupBy(incident => incident.GetType());
        foreach (var group in groupedIncidents)
        {
            BaseIncident rndIncident = group.ElementAt(Random.Range(0, group.Count()));
            rndIncident.SpawnIncident();
            foreach (BaseIncident incident in group.Where(i => i != rndIncident))
            {
                incident.SpawnReplacement();
            }
        }
    }
}
