using UnityEngine;

public class SpawnCollider : MonoBehaviour
{
    [SerializeField]
    private Collider spawnCollider;
    private bool canSpawn;

    void Start()
    {
        canSpawn = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        canSpawn = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RearCar")
            canSpawn = true;
    }
    public bool checkSpawn()
    {
        return canSpawn;
    }
}
