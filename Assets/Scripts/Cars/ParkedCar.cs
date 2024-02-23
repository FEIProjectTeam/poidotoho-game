using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkedCar : MonoBehaviour
{
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
        if (randomIndex > 0)
        {
            var car = Instantiate(SelectACarPrefab(), transform);
            randomIndex = Random.Range(0, 2);
            if (randomIndex > 0)
                car.transform.GetChild(0).Rotate(0, 180, 0);
        }
    }
}
