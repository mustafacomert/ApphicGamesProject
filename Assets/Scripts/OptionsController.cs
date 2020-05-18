using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField]
    private GameObject SoundToggle;

    private void Awake()
    {
        Toggle tmp = SoundToggle.GetComponent<Toggle>();
        if (PlayerPrefs.GetInt("isSoundOn", 1) == 0)
            tmp.isOn = false;
        else
            tmp.isOn = true;
    }
    public void Toggle_Changed(bool newValue)
    {
        if(newValue)
        {
            PlayerPrefs.SetInt("isSoundOn", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isSoundOn", 0);
        }
    }
}
