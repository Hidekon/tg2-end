using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotator : MonoBehaviour
{    
    public Quaternion prevQuat;
    public float angle = 0.0f;
    public float speed = 1;

    Quaternion test_quat;
    
    void Start()
    {
        test_quat = transform.rotation;
        prevQuat = transform.rotation;
        //Debug.Log(startQuaternion);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.U))
        {   
            transform.Rotate(new Vector3(1 * speed, 0, 0));
        }        
        if (Input.GetKey(KeyCode.Y))
        {
            transform.Rotate(new Vector3(-1 * speed, 0, 0));
        }
        if (Input.GetKey(KeyCode.M))
        {
            test_quat = new Quaternion(1, 0, 0, 0);                         
        }


        Vector3 euler = transform.rotation.eulerAngles;
        Debug.Log(euler); 
        angle += Quaternion.Angle(transform.rotation, prevQuat);
                
        //transform.rotation = Quaternion.Slerp(transform.rotation, test_quat, 0.1f);
        prevQuat = transform.rotation;
    }
    
}
