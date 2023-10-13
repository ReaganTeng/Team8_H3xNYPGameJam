using System.Collections;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    private CanvasGroup mainMenuCanvasGroup;
    [SerializeField] private TitleScript scaleTextScript;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject tutorialButton;

    private void Awake()
    {
        mainMenuCanvasGroup = mainMenuPanel.GetComponent<CanvasGroup>();
    }
    private IEnumerator fadeOut()
    {
        scaleTextScript.StopScaling();
        tutorialButton.SetActive(false);
        while (mainMenuCanvasGroup.alpha > 0f)
        {
            mainMenuCanvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }
        mainMenuPanel.SetActive(false);
        pauseButton.SetActive(true);
        enemyManeger.EM.SpawnEnemy();
        GameManagerScript.gmInstance.gameState = GameManagerScript.GameStates.PLAYING;
    }
    public void StartGame()
    {
        if (GameManagerScript.gmInstance.gameState != GameManagerScript.GameStates.START)
        {
            return;
        }
        StartCoroutine(fadeOut());
    }
    private void Update()
    {
    }
    public void onPauseButtonClicked()
    {
        GameManagerScript.gmInstance.gameState = GameManagerScript.GameStates.PAUSED;
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        pausePanel.SetActive(true);
    }
    public void onResumeButtonClicked()
    {
       pauseButton.SetActive(true);
       pausePanel.SetActive(false);
        Time.timeScale = 1;
       GameManagerScript.gmInstance.gameState = GameManagerScript.GameStates.PLAYING;
    }

    public void toggleTutorialPanel()
    {
        if(GameManagerScript.gmInstance.gameState == GameManagerScript.GameStates.TUTORIAL)
        {
            GameManagerScript.gmInstance.gameState = GameManagerScript.GameStates.START;
        }
        else
        {

            GameManagerScript.gmInstance.gameState = GameManagerScript.GameStates.TUTORIAL;
        }
        tutorialPanel.SetActive(!tutorialPanel.activeSelf);
    }
}
