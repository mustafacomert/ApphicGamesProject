using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject itemPrefab;
    private List<GameObject> itemList;
    private Vector3 pos;
    private float length;
    [SerializeField]
    private float speed = 10f;
    void Awake()
    {
        itemList = new List<GameObject>();
        pos = transform.position;
        length = 40;
    }

    public void CreateItems()
    {
        GameObject tmp;
        for (int i = 0; i < Random.Range(1, 10); ++i)
        {
            if (i % 3 == 0)
            {
                tmp = Instantiate(itemPrefab, new Vector3(-2, 0.5f, Random.Range(pos.y - length / 4, pos.y + length / 2)),
                   Quaternion.identity) as GameObject;
            }
            else if (i % 3 == 1)
            {
                tmp = Instantiate(itemPrefab, new Vector3(2, 0.5f, Random.Range(pos.y - length / 2, pos.y + length / 2)),
                              Quaternion.identity) as GameObject;
            }
            else
            {
                tmp = Instantiate(itemPrefab, new Vector3(0, 0.5f, Random.Range(pos.y - length / 2, pos.y + length / 2)),
                                Quaternion.identity) as GameObject;
                
            }
            itemList.Add(tmp);
            tmp.transform.SetParent(transform);
        }
    }

    public void DeleteItems()
    {
        foreach(GameObject item in itemList)
        {
            Destroy(item);
        }
        itemList.Clear();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
    }
}
