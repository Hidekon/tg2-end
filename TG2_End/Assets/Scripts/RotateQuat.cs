using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateQuat : MonoBehaviour
{

    public Quaternion quat1_test = Quaternion.Euler(90, 0, 0);
    
    
    void Start()
    {
        Debug.Log(quat1_test);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up);
        
    }
}
