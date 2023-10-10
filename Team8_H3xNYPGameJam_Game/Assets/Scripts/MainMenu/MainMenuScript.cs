using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    private CanvasGroup mainMenuCanvasGroup;
    [SerializeField] private ScaleTextScript scaleTextScript;
    [SerializeField] private TMP_Text startText;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private TMP_Text settingsText;
    [SerializeField] private GameObject pauseButton;

    private void Awake()
    {
        mainMenuCanvasGroup = mainMenuPanel.GetComponent<CanvasGroup>();
    }
    private IEnumerator fadeOut()
    {
        scaleTextScript.StopScaling();                      
        while (mainMenuCanvasGroup.alpha > 0f)
        {
            mainMenuCanvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }
        mainMenuPanel.SetActive(false);
        pauseButton.SetActive(true);
        GameManagerScript.gmInstance.gameState = GameManagerScript.GameStates.PLAYING;
    }
    public void StartGame()
    {
        StartCoroutine(fadeOut());
    }
    public void onSettingsButtonClicked()
    {
        GameManagerScript.gmInstance.gameState = GameManagerScript.GameStates.SETTINGS;
        Color newColor = new Vector4(0, 0, 0, 0);
        startText.color = newColor;
        settingsButton.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void onSettingsBackButtonClicked()
    {
        if(GameManagerScript.gmInstance.gameState == GameManagerScript.GameStates.SETTINGS)
        {
            Color newColor = new Vector4(0, 0, 0, 1);
            startText.color = newColor;
            GameManagerScript.gmInstance.gameState = GameManagerScript.GameStates.START;
        }
        else if(GameManagerScript.gmInstance.gameState == GameManagerScript.GameStates.PAUSED)
        {
            pauseButton.SetActive(true);
            GameManagerScript.gmInstance.gameState = GameManagerScript.GameStates.PLAYING;
        }
        settingsButton.SetActive(true);
        settingsPanel.SetActive(false);
    }
    public void onPauseButtonClicked()
    {
        GameManagerScript.gmInstance.gameState = GameManagerScript.GameStates.PAUSED;
        pauseButton.SetActive(false);
        settingsText.text = "PAUSED";
        settingsPanel.SetActive(true);
    }
}
