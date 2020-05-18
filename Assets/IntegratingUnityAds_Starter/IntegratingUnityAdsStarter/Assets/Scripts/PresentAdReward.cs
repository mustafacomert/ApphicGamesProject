using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PresentAdReward : MonoBehaviour
{
    public Text cooldownText;
    
    public float coolDown = 30f;

    public float rewardAmount = 20f;

    public string zoneId = "rewardedVideo";

    private Button button;

    private float remainingCooldown;

	// Use this for initialization
	void Start () {
	    button = GetComponent<Button>();
	}

    void Update() {
        
    }
}
