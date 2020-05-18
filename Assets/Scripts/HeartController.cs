using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
public class HeartController : MonoBehaviour
{
    [SerializeField]
    private GameObject heartPrefab;
    private Vector3 headHeart;
    private Vector3 nextStepVector;
    private float nextStep = 64;

    [SerializeField]
    private int maxHeartCount = 3;
    public int currentHeartCount { set;  get; }

    private List<GameObject> heartsList;
    private void Awake()
    {
        //second parameter is default value = maxHeartCount 
        currentHeartCount = PlayerPrefs.GetInt("currentHeartCount", maxHeartCount);
        nextStepVector = new Vector3(nextStep, 0, 0);
        if(currentHeartCount != 3)
        {
            headHeart = transform.position + (3 - currentHeartCount) * nextStepVector;
        }
        else
        {
            headHeart = transform.position;
        }
        if(heartsList != null)
        {
            heartsList.Clear();
            heartsList = null;
        }
        drawHeartsOnMenu();
    }
    
  
    private void drawHeartsOnMenu()
    {
        heartsList = new List<GameObject>();
        Vector3 avaibleSlot = headHeart;
        for(int i = 0; i < currentHeartCount; ++i)
        {
            GameObject tmp = Instantiate(heartPrefab, avaibleSlot, Quaternion.identity) as GameObject;
            tmp.transform.SetParent(gameObject.transform);
            Debug.Log("created");
            heartsList.Add(tmp);
            avaibleSlot += nextStepVector;
        }
    }

    public void decreaseHeartCount()
    {
        if (currentHeartCount == 0)
            return;
        --currentHeartCount;
        PlayerPrefs.SetInt("currentHeartCount", currentHeartCount);
    }

    public bool A()
    {
        return true;
    }
}
