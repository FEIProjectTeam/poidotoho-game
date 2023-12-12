using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTestBehaviour : MonoBehaviour
{
    Transform mainCam;
    Transform unit;
    Transform floatTextCanvas;

    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main.transform;
        unit = transform.parent;
        floatTextCanvas = GameObject.FindAnyObjectByType<Canvas>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);
        transform.position = unit.position;
    }
}
