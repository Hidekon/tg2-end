using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAssign : MonoBehaviour
{
    //public RotateQuat rot;
    public UdpSocket udp;
    public GameObject chest;

    Quaternion initRot;
    Quaternion offset;
    
    void Start()
    {
        initRot = chest.transform.rotation;
        Debug.Log(initRot);
        offset = Quaternion.Inverse(udp.quat1) * initRot;
        Debug.Log(offset);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            offset = Quaternion.Inverse(udp.quat1) * initRot;
        }
        
        chest.transform.rotation = udp.quat1 * offset; 
        
    }
}
