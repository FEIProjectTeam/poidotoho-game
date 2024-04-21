using UnityEngine;

public class CrossroadCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject inPoint;
    [SerializeField]
    private GameObject outPoint;

    private Vector3 inPointPosition;
    private Vector3 outPointPosition;
    private int number = -1;
    private Crossroad crossroad;

    void Start()
    {
        if (this.inPoint != null && this.outPoint != null)
        {
            this.crossroad = this.gameObject.GetComponentInParent<Crossroad>();
            this.inPointPosition = inPoint.transform.position;
            this.outPointPosition = outPoint.transform.position;
        }
        else 
            this.crossroad = null;
    }

    public bool isValidCollider()
    {
        if (this.inPoint == null || this.outPoint == null)
            return false;
        else
            return true;
    }

    public void setNumber(int number)
    {
        this.number = number;
    }
    public int getNumber() 
    { 
        return this.number;
    }

    private void OnTriggerEnter(Collider triggerCollider)
    {
        if (this.crossroad != null && triggerCollider.tag == "Car")
        {
            Car car = triggerCollider.gameObject.GetComponent<Car>();
            if (car != null)
                crossroad.onCarEnter(this, car);
        }
    }
    private void OnTriggerExit(Collider triggerCollider)
    {
        if (this.crossroad != null && triggerCollider.tag == "RearCar")
        {
            Car car = triggerCollider.gameObject.GetComponentInParent<Car>();
            if (car != null)
                crossroad.onCarExit(this, car);
        }
    }

    public Vector3 getInPoint()
    {
        return inPointPosition;
    }
    public Vector3 getOutPoint()
    {
        return outPointPosition;
    }
}
