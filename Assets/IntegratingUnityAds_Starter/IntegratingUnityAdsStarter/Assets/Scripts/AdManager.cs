using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;
    
    void Awake()
    {
        if (!instance) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start() {
        
    }

    /// <summary>
    /// Used to 'pause' the game when running in the Unity editor (Unity Ads will pause the game on actual Android or iOS devices by default, this is just for the editor)
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForAdEditor() {
        float currentTimescale = Time.timeScale;
        Time.timeScale = 0f;
        AudioListener.pause = true;

        yield return null;

        while (Advertisement.isShowing) {
            yield return null;
        }

        AudioListener.pause = false;
        if (currentTimescale > 0f) {
            Time.timeScale = currentTimescale;
        }
        else {
            Time.timeScale = 1f;
        }
    }
}