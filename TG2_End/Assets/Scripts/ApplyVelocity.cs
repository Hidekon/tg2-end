using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyVelocity : MonoBehaviour
{
    private Vector3 startPos;
    public UdpEuler udp;
    float timer = 0;
    float velocity;

    [SerializeField] private float speed;
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (udp.velocity > 10)
        {
            velocity = 1;
        } else
        {
            velocity = udp.velocity;
        }
        transform.position = startPos + new Vector3(0, 0, velocity * speed * timer);
    }
}
