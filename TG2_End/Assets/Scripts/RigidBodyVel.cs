using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyVel : MonoBehaviour
{
    public UdpEuler udp;
    private Rigidbody rb;
    [SerializeField] private float velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (udp.velocity > 10)
        {
            velocity = 0;
        }
        else
        {
            velocity = udp.velocity;
        }
        rb.velocity = new Vector3(0, 0, -velocity);
    }
}
