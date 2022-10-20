using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotator : MonoBehaviour
{
    public Quaternion quat;

    void Start()
    {
        Quaternion startQuaternion = transform.rotation;
        Debug.Log(startQuaternion);
    }

    // Update is called once per frame
    void Update()
    {
        quat = transform.rotation;

        if (Input.GetKey(KeyCode.U))
        {
            
            transform.Rotate(new Vector3(0, 1, 0));
        }
        
        if (Input.GetKey(KeyCode.M))
        {

            transform.rotation = new Quaternion(1, 0, 0, 0);
                            
        }

      
    }
    
}
