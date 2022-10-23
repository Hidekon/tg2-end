using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAssign : MonoBehaviour
{
    public RotateQuat rot;
    //public UdpSocket udp;
    public GameObject chest;

    Quaternion initRot;
    Quaternion offset;
    
    void Start()
    {
        initRot = chest.transform.rotation;
        Debug.Log(initRot);
        offset = Quaternion.Inverse(rot.quat1_test) * initRot;
        Debug.Log(offset);

    }

    // Update is called once per frame
    void Update()
    {
        chest.transform.rotation = rot.quat1_test * offset; 
        
    }
}
