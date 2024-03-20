using Managers;
using UnityEngine;
using UnityEngine.AI;

public class MovementCharacter : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Camera cam;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Detect left mouse button click
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.State == GameManager.GameState.RoamingMap)
        {
            // Create a ray from the camera, passing through the mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If the ray hits something (should ideally be the ground/sidewalk with NavMesh)
            if (Physics.Raycast(ray, out hit))
            {
                // Set the agent's destination to the point where the ray hit
                _agent.destination = hit.point;
            }
        }
    }
}
