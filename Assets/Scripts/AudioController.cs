using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.GetInt("isSoundOn") == 0)
            gameObject.GetComponent<AudioSource>().enabled = false;

    }
}
