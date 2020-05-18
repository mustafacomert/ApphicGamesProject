using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.GetInt("isSoundOn") == 0)
        {
            foreach(var i in gameObject.GetComponents<AudioSource>())
                i.enabled = false;
        }
    }
}
