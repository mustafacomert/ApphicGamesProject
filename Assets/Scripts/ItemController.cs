using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    private PlayerController player;
    [SerializeField]
    private int bonusEnergy = 5;
    private TextMeshProUGUI text;
    private static int count = 0;

    private void Awake()
    {
        text = GameObject.FindGameObjectWithTag("ItemCounter").GetComponent<TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        Debug.Log("Count: " + count);
        text.text = count.ToString();

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ++count;
            player.energy += bonusEnergy;
            Destroy(gameObject);
        }
    }
}
