using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLegs : MonoBehaviour
{
    public UdpEuler udp;
    public GameObject leftLeg;
    public GameObject rightLeg;

    Quaternion leftQuat;
    Quaternion rightQuat;
    
    void Start()
    {
        leftQuat = leftLeg.transform.rotation;
        rightQuat = rightLeg.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(udp.y_data);
        
        leftLeg.transform.rotation = leftQuat;
        Debug.Log(leftLeg.transform.rotation);

        rightLeg.transform.rotation = rightQuat;
    }
}
