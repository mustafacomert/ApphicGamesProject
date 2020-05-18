using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using UnityEngine;


//terrain tipindeki zeminlerin oluşmasını
//bu zeminlerin ve  üzerindeki itemlerin
//yok olmasını sağlayan scriptir.
public class FloorController : MonoBehaviour
{
    //yaratılacak objenin prefabı
    [SerializeField]
    private GameObject floorPrefab;
    //objenin yaratıldığı andaki pozisyonu
    private Vector3 currPos;
    //zemin sırasına eklenen son zemin elemanı
    private GameObject lastFloor;
    //zeminleiri sıralı biçimde tutar 
    private Queue<GameObject> floorQueue;
    //ana karakterin objesine bir referans
    [SerializeField]
    private GameObject player;
   
    private void Awake()
    {
        floorQueue = new Queue<GameObject>();
        lastFloor = gameObject;
        currPos = transform.position;
        //CreateFloor fonksiyonunu 2 saniyede 1 çağırarak,
        //zeminlerin yaratılmasını sağlar
        InvokeRepeating("CreateFloor", 0, 2);
    }

    
    //her çağrıldığında bir zemin yaratır
    //eğer karakterin gerisinde kalmış zemin varsa
    //onları ve üzerindeki itemleri yok eder.
    void CreateFloor()
    {
        if (floorQueue.Count != 0)
        {
            Vector3 tmp = lastFloor.transform.position;
            currPos = new Vector3(tmp.x, tmp.y, tmp.z + 40);
        }
        lastFloor = Instantiate(floorPrefab, currPos, Quaternion.identity)
                         as GameObject;
        floorQueue.Enqueue(lastFloor);
        lastFloor.transform.SetParent(gameObject.transform);
        lastFloor.GetComponent<MoveFloor>().CreateItems();
        Debug.Log("floor: " + floorQueue.Peek().transform.position.z);
        Debug.Log("player: " + player.transform.position.z);
        if (floorQueue.Peek().transform.position.z < player.transform.position.z - 50)
        {
            floorQueue.Peek().GetComponent<MoveFloor>().DeleteItems();
            Destroy(floorQueue.Dequeue());
        }
        
    }
    
}
