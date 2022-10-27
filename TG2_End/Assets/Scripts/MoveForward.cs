using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public Transform rLeg;
    public Transform lLeg;
    public float speed = 100;
    public float t = 0.3f;
    public Transform target;
    Transform prevTransform;
    public float totalDistance = 0;


    void Start()
    {
        target.position = transform.position;
        prevTransform = rLeg;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            Debug.Log("Key U Pressed");
            rLeg.Rotate(Vector3.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.J))
        {
            Debug.Log("Key J Pressed");
            rLeg.Rotate(Vector3.back * speed * Time.deltaTime);
        }

        float zDistance = CalculateDistance();
        Vector3 a = transform.position; 
        Vector3 b = target.position + new Vector3(0 ,0, 1);
        Debug.Log(zDistance);

        if (zDistance > 0)
        {
            transform.position = Vector3.Lerp(a, b, t) ;

        }

        prevTransform = transform;
    }

    float CalculateDistance()
    {
        Vector3 distanceXYZ = rLeg.rotation.eulerAngles - prevTransform.rotation.eulerAngles;
        totalDistance = totalDistance + distanceXYZ.z;
        distanceXYZ = Vector3.zero;
        Debug.Log(totalDistance);
        return distanceXYZ.z;
    }
    
}
