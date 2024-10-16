using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Followe : MonoBehaviour
{
    public Transform Target;
    public Vector3 offset;
   // float smooth_speed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
       //Look At Camera 
       //transform.LookAt(Target);
       // Camera followe script
       transform.position = Target.position + offset;
       transform.LookAt(Target);
      
    }
}
