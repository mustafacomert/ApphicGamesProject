using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region singleton instance
    public static GameManager instance;
    #endregion

    #region UI
    public RectTransform gameOverPanelRect;
    public Text gameoverText;
    public Button restartButton;
    #endregion

    #region GameVars
    public float extraFuel;
    public GameObject playerPrefab;
    public GameObject landingPadPrefab;
    #endregion

    #region private fields
    private Vector2 gameOverPanelHiddenPos;
    #endregion

    void Awake() {
        if (!instance) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        gameOverPanelHiddenPos = gameOverPanelRect.anchoredPosition;
        restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(RestartLevel);
    }

    public void RestartLevel() {
        
        // Destroy any old player objects
        var oldPlayers = FindObjectsOfType<Lander>();
        foreach (var plr in oldPlayers) {
            Destroy(plr.gameObject);
        }

        // Destroy old landing pad
        var landingPad = FindObjectOfType<LanderObjective>();
        if (landingPad != null) {
            Destroy(landingPad.gameObject);
        }

        var landingPadSpawn = Instantiate(landingPadPrefab, new Vector2(-8.6f, -13.2f), Quaternion.identity);
        landingPadSpawn.name = "LanderObjective";
        var player = (GameObject)Instantiate(playerPrefab, new Vector2(-8f, -1.9f), Quaternion.identity);
        player.name = "Lander";
        Camera.main.GetComponent<CameraLerpToTransform>().cameraTrackTarget = player.transform;
        ToggleGameOverPanel(false, false);
    }

    public void ToggleGameOverPanel(bool show, bool gameWon) {
        if (restartButton == null) restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(RestartLevel);

        if (gameOverPanelRect == null) {
            gameOverPanelRect = GameObject.Find("GameOverPanel").GetComponent<RectTransform>();
        }

        if (gameoverText == null) {
            gameoverText = GameObject.Find("GameOverTitleText").GetComponent<Text>();
        }

        if (show) {
            gameOverPanelRect.anchoredPosition = Vector2.zero;
        }
        else {
            gameOverPanelRect.anchoredPosition = gameOverPanelHiddenPos;
        }

        if (gameWon) {
            gameoverText.text = "You landed safely!";
        }
        else {
            gameoverText.text = "You crashed!";
        }
    }
}
