using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossroad : MonoBehaviour
{
    List<CrossroadCollider> colliders = new List<CrossroadCollider>();
    List<Car> cars = new List<Car>();
    List<int[]> carColliders = new List<int[]>();
    List<Car> waitingCars = new List<Car>();

    bool[] isColliderFree;
    Vector3[] outPointCollider;

    void Start()
    {
        this.gameObject.GetComponentsInChildren<CrossroadCollider>(colliders);
        int i = 0;
        foreach (CrossroadCollider c in colliders)
        {
            if (c.isValidCollider())
            {
                c.setNumber(i);
                i++;
            }
            else
            {
                colliders.Remove(c);
            }
        }
        isColliderFree = new bool[i];
        outPointCollider = new Vector3[i];
        for (int j = 0; j < i; j++)
        {
            isColliderFree[j] = true;
            outPointCollider[j] = colliders[j].getOutPoint();
        }
    }
    /*if(GameObject.ReferenceEquals(firstGameObject, secondGameObject))
      Debug.Log("first and second are the same");*/
    public void onCarEnter(CrossroadCollider collider, Car car)
    {
        if (!colliders.Contains(collider))
            return;
        if (!cars.Contains(car))
        {
            cars.Add(car);

            int inCollider = collider.getNumber();
            int outCollider = -1;
            Vector3 outPoint = car.getSecondNextWaypoint();
            for (int i = 0; i < this.outPointCollider.Length; i++)
            {
                if (this.outPointCollider[i] == outPoint)
                {
                    outCollider = i;
                    break;
                }
            }
            if (outCollider < 0)
            {
                outPoint = car.getThirdNextWaypoint();
                for (int i = 0; i < this.outPointCollider.Length; i++)
                {
                    if (this.outPointCollider[i] == outPoint)
                    {
                        outCollider = i;
                        break;
                    }
                }
                if (outCollider < 0)
                {
                    Debug.LogError("Collider with out point not found at crossroad.");
                    return;
                }
            }
            this.carColliders.Add(new int[] { inCollider, outCollider });

            if (this.isColliderFree[inCollider] && this.isColliderFree[outCollider])
            {
                this.isColliderFree[inCollider] = false;
                this.isColliderFree[outCollider] = false;
            }
            else
            {
                car.stopAtCrossroad();
                waitingCars.Add(car);
            }
        }
    }
    public void onCarExit(CrossroadCollider collider, Car car)
    {
        if (!colliders.Contains(collider))
            return;
        int colNum = collider.getNumber();
        int i = this.cars.IndexOf(car);
        if (this.carColliders[i][0] == colNum && this.carColliders[i][1] == colNum)
        {
            this.carColliders.RemoveAt(i);
            this.cars.RemoveAt(i);
            isColliderFree[colNum] = true;
            freeNextCar();
        }
        else
        {
            this.carColliders[i][0] = this.carColliders[i][1];
            isColliderFree[colNum] = true;
            freeNextCar();
        }
    }

    private void freeNextCar()
    {
        if (waitingCars.Count > 0)
        {
            foreach (Car car in waitingCars)
            {
                int i = cars.IndexOf(car);
                if (this.isColliderFree[this.carColliders[i][0]] && this.isColliderFree[this.carColliders[i][1]])
                {
                    this.isColliderFree[this.carColliders[i][0]] = false;
                    this.isColliderFree[this.carColliders[i][1]] = false;
                    waitingCars.Remove(car);
                    car.resumeAtCrossroad();
                    return;
                }
            }
        }
        
    }
}

