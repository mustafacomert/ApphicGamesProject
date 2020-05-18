using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.TerrainAPI;

public class PlayerController : MonoBehaviour
{

    public LayerMask mask;
    Rigidbody rb;
    //-1 means player at the left
    //0 means player at the center;
    //1 means player at the right;
    int location;
    Vector3 leftPos, rightPos, centerPos;
    float distance = 2;
    float lateralSpeed = 10;
    public int jumpForce = 30;
    bool isMoving;
    Vector3 targetPos;
    Vector3 beganFinger;
    Vector3 endedFinger;
    bool isJumping;
    float jumpDistance;

    public int energy{set;get;}
    private void Awake()
    {
        jumpDistance = 0;
        isJumping = false;
        centerPos = transform.position;
        leftPos = centerPos - new Vector3(distance, 0, 0);
        rightPos = centerPos + new Vector3(distance, 0, 0);
        isMoving = false;
        location = 0;
        targetPos = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
           
        transform.position = Vector3.MoveTowards(transform.position, targetPos + new Vector3(0, jumpDistance,0), Time.fixedDeltaTime * lateralSpeed);
        if (transform.position.Equals(targetPos))
        {
            isMoving = false;
        }
        if(transform.position.y == centerPos.y + jumpDistance)
        {
            jumpDistance = 0;
        }
        if (IsGrounded())
        {
            if (!isMoving && (Input.touchCount > 0))
            {


                /*if (touch.phase == TouchPhase.Began)
                {
                    beganFinger = touch.position;
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    endedFinger = touch.position;
                    if (beganFinger.y < endedFinger.y)
                    {
                        if (IsGrounded())
                        {
                            Jump();
                        }
                    }
                }*/

                Touch touch = Input.GetTouch(0);

                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    // you touched at least one UI element
                    return;
                }
                //if the user touched left side of the screen
                if ((touch.position.x < Screen.width / 2) && (touch.phase == TouchPhase.Ended) && location != -1)
                {
                    Debug.Log("Left");
                    isMoving = true;
                    if (location == 0)
                    {
                        targetPos = leftPos;
                    }
                    else if (location == 1)
                    {
                        targetPos = centerPos;
                    }
                    --location;
                }
                //else if the user touch right of the screen
                else if ((touch.position.x > Screen.width / 2) && (touch.phase == TouchPhase.Ended) && location != 1)
                {
                    Debug.Log("Right");
                    isMoving = true;
                    if (location == 0)
                    {
                        targetPos = rightPos;
                    }
                    else if (location == -1)
                    {
                        targetPos = centerPos;
                    }
                    ++location;
                }
            }
        }
    }

    public void Jump()
    {
        if (IsGrounded())
            jumpDistance = 2.5f; 

    }

    bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, transform.localScale.y + 0.1f, mask))
        {
            Debug.Log("yerde");
            return true;
        }
        else
        {
            Debug.Log("Havada");
            return false;
        }
    }
}
