using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public Rotator rot;
    float speed = 0.01f;
       
    void Start()
    {
                
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rot.angle);
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, rot.angle * speed), 0.1f);
        
    }
            
}
