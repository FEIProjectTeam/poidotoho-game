using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkedCar : MonoBehaviour
{
    [SerializeField]
    public bool allwaysSpawn;
    [SerializeField]
    public bool randomRotation;
    [SerializeField]
    public GameObject[] carPrefabs;

    private GameObject SelectACarPrefab()
    {
        var randomIndex = Random.Range(0, carPrefabs.Length);
        return carPrefabs[randomIndex];
    }

    void Start()
    {
        Destroy(this.transform.GetChild(0).gameObject);

        var randomIndex = Random.Range(0, 2);
        if (randomIndex > 0 || allwaysSpawn)
        {
            var car = Instantiate(SelectACarPrefab(), transform);
            randomIndex = Random.Range(0, 2);
            if (randomIndex > 0 && randomRotation)
            {
                Vector3 currentRotation = car.transform.GetChild(0).localRotation.eulerAngles;
                car.transform.GetChild(0).localRotation = Quaternion.Euler(currentRotation.x, currentRotation.y + 180f, currentRotation.z);
            }
        }
    }
}
