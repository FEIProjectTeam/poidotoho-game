using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Car : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    public float speed = 5f;
    /*[SerializeField]
    public float rotationSpeed = 5f;*/

    private int currentWaypointIndex = 0;

    [SerializeField]
    public GameObject[] carPrefabs;

    public List<Vector3> waypoints;
    private Quaternion endRotation;

    [SerializeField]
    private Collider triggerCollider;
    void setWay(List<Vector3> waypoints)
    {
        this.waypoints = waypoints;
    }

    private GameObject SelectACarPrefab()
    {
        var randomIndex = Random.Range(0, carPrefabs.Length);
        return carPrefabs[randomIndex];
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Instantiate(SelectACarPrefab(), transform);
        endRotation = transform.rotation;
    }

    void Update()
    {
        if (waypoints.Count > 0)
        {
            MoveToWaypoint();
        }
    }

    private void OnTriggerEnter(Collider triggerCollider)
    {
        //Debug.Log("aaa");
    }

    private void MoveToWaypoint()
    {
        Vector3 targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 moveDirection = (targetWaypoint - transform.position).normalized;
        Debug.DrawRay(transform.position, moveDirection * 5, Color.red);

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, Time.deltaTime * speed);

        Quaternion endYRotation = Quaternion.Euler(0, endRotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, endYRotation, Time.deltaTime * ((speed / 2f) * 110f));

        if (Vector3.Distance(transform.position, targetWaypoint) == 0)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                Destroy(this.gameObject);
                AIManager.Instance.despawnCar();
            }
            else
            {
                /*if ((waypoints[currentWaypointIndex].x - targetWaypoint.x < 0.01f || waypoints[currentWaypointIndex].x - targetWaypoint.x > -0.01f)
                    && (waypoints[currentWaypointIndex].z - targetWaypoint.z < 0.01f || waypoints[currentWaypointIndex].z - targetWaypoint.z > -0.01f)) {

                    Debug.Log("Totok");
                }*/

                if ((waypoints[currentWaypointIndex].x - targetWaypoint.x > 0.01f || waypoints[currentWaypointIndex].x - targetWaypoint.x < -0.01f)     // unity to pocita nepresne, musi to byt takto
                && (waypoints[currentWaypointIndex].z - targetWaypoint.z > 0.01f || waypoints[currentWaypointIndex].z - targetWaypoint.z < -0.01f))
                {
                    //Debug.Log("Zatacame");

                    if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex]) > 5)
                    {
                        Vector3 start = waypoints[currentWaypointIndex - 1];
                        Vector3 end = waypoints[currentWaypointIndex];
                        Vector3 center = (start + (moveDirection * 3.825f));
                        Vector3 newDirection = (end - center).normalized;

                        List<Vector3> curve = new List<Vector3>();

                        curve.Add(center - (moveDirection * 1.175f));
                        curve.Add(center + (newDirection * 1.175f));

                        this.waypoints.InsertRange(currentWaypointIndex, curve);
                    }
                    else
                    {
                        //Debug.Log("else");

                        Vector3 start = waypoints[currentWaypointIndex - 1];
                        Vector3 end = waypoints[currentWaypointIndex];
                        Vector3 center = (start + (moveDirection * 1.175f));
                        Vector3 newDirection = (end - center).normalized;

                        endRotation = Quaternion.LookRotation(-1 * newDirection);

                        /*Debug.Log(start);
                        Debug.Log(end);
                        Debug.Log(center);
                        Debug.Log(newDirection);
                        Debug.Log(startRotation.eulerAngles);
                        Debug.Log(endRotation.eulerAngles);*/

                    }
                }
            }
        }
    }

    // odpad

    //Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
    //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

    /*if (currentWaypointIndex < waypoints.Count - 1)
    {
        //Vector3 nextWaypoint = waypoints[currentWaypointIndex + 1];
        //Vector3 moveDirection = (transform.position - nextWaypoint).normalized;
        //Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }*/


    /*
    private void MoveToWaypoint()
    {
        if (currentWaypointIndex >= waypoints.Count)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetWaypoint = waypoints[currentWaypointIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, Time.deltaTime * speed);

        if (currentWaypointIndex < waypoints.Count - 1)
        {
            Vector3 moveDirection = (transform.position - targetWaypoint).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        if (Vector3.Distance(transform.position, targetWaypoint) < 0.1f)
        {
            currentWaypointIndex++;
        }
    }*/
}
