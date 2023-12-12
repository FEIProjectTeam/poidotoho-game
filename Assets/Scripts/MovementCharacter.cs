using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementCharacter : MonoBehaviour
{
    private NavMeshAgent agent;
    public Camera cam;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Detect left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera, passing through the mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If the ray hits something (should ideally be the ground/sidewalk with NavMesh)
            if (Physics.Raycast(ray, out hit))
            {
                // Set the agent's destination to the point where the ray hit
                agent.destination = hit.point;
            }
        }
    }
}
