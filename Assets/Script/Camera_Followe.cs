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
        
            if (Target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    Target = player.transform;
                }
                else
                {
                    Debug.LogError("Camera_Followe: Player with tag 'Player' not found.");
                }
            }
        


    }

    void LateUpdate()
    {
        if (Target == null)
        {
            Debug.LogWarning("Camera_Followe: Target is not assigned!");
            return;
        }

        transform.position = Target.position + offset;
        transform.LookAt(Target);
    }

}
