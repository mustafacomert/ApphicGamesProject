using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool isGamePaused;
    [SerializeField]
    private GameObject sound;
    public void Pause()
    {
        if(isGamePaused)
        {
            if (PlayerPrefs.GetInt("isSoundOn") == 1)
            {
                foreach( var i in sound.GetComponents<AudioSource>())
                {
                    i.mute = false;
                }
            }
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            isGamePaused = false;
        }
        else
        {
            if (PlayerPrefs.GetInt("isSoundOn") == 1)
            {
                foreach (var i in sound.GetComponents<AudioSource>())
                {
                    i.mute = true;
                }
            }
            gameObject.SetActive(true);
            Time.timeScale = 0f;
            isGamePaused = true;
        }
    }

}
