using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollider : MonoBehaviour
{
    [SerializeField]
    private Collider spawnCollider;
    private bool canSpawn;

    private void OnTriggerEnter(Collider other)
    {
        canSpawn = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RearCar")
        {
            canSpawn = true;
        }
    }

    public bool checkSpawn()
    {
        return canSpawn;
    }

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
