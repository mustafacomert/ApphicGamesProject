//using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    public LayerMask mask;
    Rigidbody rb;
    //-1 means player at the left
    //0 means player at the center;
    //1 means player at the right;
    int location;
    //target olabilecek 3 farklı nokta
    Vector3 leftPos, rightPos, centerPos;
    //sağa ve sola kaç birim
    float distance = 2;
    //x eksenindeki hız
    float lateralSpeed = 10;
    //eğer karakter hareketliyse
    //kullanıcı girdilerini yok saymak için kullanılır
    bool isMoving;
    //kullanıcı girdilerin karşılık değişir
    //obje her frame targete ulaşmaya çalışır
    //eğer targetteyse durur.
    Vector3 targetPos;
    float jumpDistance;
    //item toplandıkça artacak olan energy 
    public int energy{set; get;}
    private void Awake()
    {
        jumpDistance = 0;
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
        //frame başına bir kaç kere çağrılır ve hedefe doğru gider,
        //hedefi asla geçmez
        transform.position = Vector3.MoveTowards(transform.position, targetPos + new Vector3(0, jumpDistance,0), Time.fixedDeltaTime * lateralSpeed);
        if (transform.position.Equals(targetPos))
        {
            isMoving = false;
        }
        //if player at the peek point of jump
        if(transform.position.y == centerPos.y + jumpDistance)
        {
            jumpDistance = 0;
        }
        if (IsGrounded())
        {
            if (!isMoving && (Input.touchCount > 0))
            {
                Touch touch = Input.GetTouch(0);

                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    // User touch the UI element
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
    //buton tıklanınca çağrılan fonksiyon
    public void Jump()
    {
        if (IsGrounded())
            jumpDistance = 2.5f; 

    }
    //karakterin yerde olup olmadığını kontrol eder
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
