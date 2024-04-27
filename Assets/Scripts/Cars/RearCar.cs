using UnityEngine;

public class RearCar : MonoBehaviour
{
    private void OnTriggerEnter(Collider triggerCollider)
    {
        if (triggerCollider.tag == "Car")
            carEnter(triggerCollider.gameObject);
    }
    private void OnTriggerExit(Collider triggerCollider)
    {
        if (triggerCollider.tag == "Car")
            carExit(triggerCollider.gameObject);
    }
    public void carEnter(GameObject go)
    {
        Car car = go.gameObject.GetComponent<Car>();
        if (car != null)
            car.stopCarInFront();
    }
    public void carExit(GameObject go)
    {
        Car car = go.gameObject.GetComponent<Car>();
        if (car != null)
            car.resumeCarInFront();
    }
}
