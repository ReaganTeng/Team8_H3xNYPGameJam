using System.Collections;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    private CanvasGroup mainMenuCanvasGroup;
    [SerializeField] private TitleScript scaleTextScript;
    [SerializeField] private GameObject pausePanel;
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
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
        StartCoroutine(fadeOut());

        }
    }
    public void onPauseButtonClicked()
    {
        GameManagerScript.gmInstance.gameState = GameManagerScript.GameStates.PAUSED;
        pauseButton.SetActive(false);
        pausePanel.SetActive(true);
    }
    public void onResumeButtonClicked()
    {
       pauseButton.SetActive(true);
        pausePanel.SetActive(false);
       GameManagerScript.gmInstance.gameState = GameManagerScript.GameStates.PLAYING;
    }
}
