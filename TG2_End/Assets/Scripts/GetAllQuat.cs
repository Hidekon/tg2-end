using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAllQuat : MonoBehaviour
{
    public UdpSocket udp;

    public Transform imu1, imu2, imu3, imu4;
    public Quaternion imu1_init, imu2_init, imu3_init, imu4_init;
    Quaternion imu1_offset, imu2_offset, imu3_offset, imu4_offset;

    void Start()
    {
        udp = GetComponent<UdpSocket>(); 
        
        imu1_init = imu1.rotation;
        imu2_init = imu2.rotation;
        imu3_init = imu3.rotation;
        imu4_init = imu4.rotation;

        imu1_offset = new Quaternion(0, 0, 0, 1); 
        imu2_offset = new Quaternion(0, 0, 0, 1);
        imu3_offset = new Quaternion(1, 0, 0, 0);
        imu4_offset = new Quaternion(1, 0, 0, 0);

        Debug.Log(Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        imu1.rotation = Quaternion.Slerp(imu1.rotation, udp.quat1 * imu1_offset, 1);
        imu2.rotation = Quaternion.Slerp(imu2.rotation, udp.quat2 * imu2_offset, 1);
        imu3.rotation = Quaternion.Slerp(imu3.rotation, udp.quat3 * imu3_offset, 1);
        imu4.rotation = Quaternion.Slerp(imu4.rotation, udp.quat4 * imu4_offset, 1);

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {                     
            imu1_offset = Quaternion.Inverse(udp.quat1) * imu1_init;
            imu2_offset = Quaternion.Inverse(udp.quat2) * imu2_init;
            imu3_offset = Quaternion.Inverse(udp.quat3) * imu3_init;
            imu4_offset = Quaternion.Inverse(udp.quat4) * imu4_init;

        }

        
    }
}
