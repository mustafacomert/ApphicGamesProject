using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerCamera : MonoBehaviour
{
   
    Vector3 offset;
    public Transform player;  
    private void Awake()
    {
        offset = transform.position - player.position;
       
    }

    private void FixedUpdate()
    {
        transform.position = offset + new Vector3(0, 0, player.position.z);
    }

}
