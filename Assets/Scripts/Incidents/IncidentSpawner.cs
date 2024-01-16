using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncidentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] incidentInstigators;
    [SerializeField] private GameObject[] incidentReplacements;

    private void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Start is called before the first frame update
    public void SpawnIncident()
    {
        Instantiate(incidentInstigators[Random.Range(0, incidentInstigators.Length)], transform.position, transform.rotation);
    }

    public void SpawnReplacement()
    {
        if (incidentReplacements != null && incidentReplacements.Length > 0)
            Instantiate(incidentReplacements[Random.Range(0, incidentReplacements.Length)], transform.position, transform.rotation);
        this.enabled = false;
    }
}
