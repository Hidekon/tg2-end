using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAllQuat : MonoBehaviour
{
    public UdpSocket udp;

    public Transform imu1;
    public Transform imu2;
    public Transform imu3;
    public Transform imu4;
    void Start()
    {
        udp = GetComponent<UdpSocket>();    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        imu1.rotation = Quaternion.Slerp(imu1.rotation, udp.quat1, 1);
        imu2.rotation = Quaternion.Slerp(imu2.rotation, udp.quat2, 1);
        imu3.rotation = Quaternion.Slerp(imu3.rotation, udp.quat3, 1);
        imu4.rotation = Quaternion.Slerp(imu4.rotation, udp.quat4, 1);

    }

    void Update()
    {
        //imu1.rotation = udp.quat1;
        //imu2.rotation = udp.quat2;
        //imu3.rotation = udp.quat3;
        //imu4.rotation = udp.quat4;

    }
}
