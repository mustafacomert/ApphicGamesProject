using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField]
    private GameObject floorPrefab;
    private Vector3 currPos;
    private GameObject lastFloor;
    private Queue<GameObject> floorQueue;
    [SerializeField]
    private GameObject player;
    private int i = 1;
   
    private void Awake()
    {
        floorQueue = new Queue<GameObject>(5);
        lastFloor = gameObject;
        currPos = transform.position;
        InvokeRepeating("CreateFloor", 0, 2);
    }

    

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
