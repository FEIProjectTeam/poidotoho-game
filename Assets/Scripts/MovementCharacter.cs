using UnityEngine;
using UnityEngine.AI;

public class MovementCharacter : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Camera cam;
    public LayerMask navmeshLayer; // Add a LayerMask for the sidewalk
    public ParticleSystem rippleEffectPrefab;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Detect left mouse button click
        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.isQuizOpened)
        {
            // Create a ray from the camera, passing through the mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If the ray hits something on the Sidewalk layer
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, navmeshLayer))
            {
                // Set the agent's destination to the point where the ray hit
                _agent.destination = hit.point;

                // Instantiate the ripple effect at the point where the ray hit the ground
                ParticleSystem rippleEffectInstance = Instantiate(rippleEffectPrefab, hit.point, Quaternion.identity);
                rippleEffectInstance.transform.forward = hit.normal; // Align with the surface normal

                // Play the particle system
                rippleEffectInstance.Play();

                // Optionally, destroy the particle system after it has finished
                Destroy(rippleEffectInstance.gameObject, rippleEffectInstance.main.duration);
            }
        }
    }
}
