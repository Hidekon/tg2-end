using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotator : MonoBehaviour
{
    
    public Quaternion quat;
    Vector3 overallRotation, oldRotation;

    void Start()
    {
        Quaternion startQuaternion = transform.rotation;
        //Debug.Log(startQuaternion);
        oldRotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        quat = transform.rotation;

        if (Input.GetKey(KeyCode.U))
        {   
            transform.Rotate(new Vector3(1, 0, 0));
        }
        if (Input.GetKey(KeyCode.Y))
        {
            transform.Rotate(new Vector3(-1, 0, 0));
        }

        if (Input.GetKey(KeyCode.M))
        {

            transform.rotation = new Quaternion(1, 0, 0, 0);
                            
        }

        //if (oldRotation != transform.rotation.eulerAngles)
        //{
        //    overallRotation += transform.rotation.eulerAngles - oldRotation;
        //    oldRotation = transform.rotation.eulerAngles;
        //    print(overallRotation);
        //}

        Debug.Log(transform.rotation);
        

        
    }
    
}
