using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public Rotator rot;
       
    void Start()
    {
        rot = GetComponent<Rotator>();        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rot.angle);
        //transform.position = new Vector3(transform.position.x, transform.position.y, rot.angle);
        
    }
            
}
