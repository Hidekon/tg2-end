using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLegs : MonoBehaviour
{
    public UdpEuler udp;
    public GameObject leftLeg;
    public GameObject rightLeg;

    Vector3 leftStartRotation;
    Vector3 rightStartRotation;
    
    void Start()
    {
        leftStartRotation = leftLeg.transform.eulerAngles;
        rightStartRotation = rightLeg.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(udp.y_data);
        
        leftLeg.transform.eulerAngles = leftStartRotation + new Vector3(0, 0, -udp.y_data);
        rightLeg.transform.eulerAngles = rightStartRotation + new Vector3(0, 0, -udp.y_data);
        
        //Debug.Log(leftLeg.transform.eulerAngles);


    }
}
