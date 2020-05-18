using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenuController : MonoBehaviour
{
    private HeartController heartController;
    [SerializeField]
    private GameObject popupMenu;
    private void Awake()
    {
        heartController = GameObject.FindObjectOfType<HeartController>();
    }
    public void LoadGameScene()
    {
        if(heartController.currentHeartCount != 0)
            SceneManager.LoadScene("Game");
        else
        {
            Popup();
        }
    }
    public void Popup()
    {
        popupMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
