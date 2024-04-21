using System.Collections.Generic;
using UnityEngine;

public class Crosswalk : MonoBehaviour
{
    private MovementCharacter player;
    private List<Car> cars;

    void Start()
    {
        player = null;
        cars = new List<Car>();
    }

    private void OnTriggerEnter(Collider triggerCollider)
    {
        if (triggerCollider.tag == "Character")
            playerEnter(triggerCollider.gameObject);

        else if (triggerCollider.tag == "Car")
            carEnter(triggerCollider.gameObject);
    }
    private void OnTriggerExit(Collider triggerCollider)
    {
        if (triggerCollider.tag == "Character")
            playerExit(triggerCollider.gameObject);

        else if (triggerCollider.tag == "RearCar")
            carExit(triggerCollider.gameObject);
    }
    public void carEnter(GameObject go)
    {
        Car car = go.gameObject.GetComponent<Car>();
        if (car != null)
        {
            cars.Add(car);
            if (player != null)
                car.stopAtCrosswalk();
        }
    }
    public void carExit(GameObject go)
    {
        Car car = go.gameObject.GetComponentInParent<Car>();
        if (car != null)
            cars.Remove(car);
    }
    public void playerEnter(GameObject go)
    {
        MovementCharacter character = go.gameObject.GetComponent<MovementCharacter>();
        if (character != null)
            player = character;
    }
    public void playerExit(GameObject go)
    {
        MovementCharacter character = go.gameObject.GetComponent<MovementCharacter>();
        if (character != null)
        {
            player = null;
            if (cars.Count > 0)
            {
                foreach (Car car in cars)
                    car.resumeAtCrosswalk();
            }
        }
    }
}
