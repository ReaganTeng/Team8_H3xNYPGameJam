using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowTimerScript : MonoBehaviour
{
    [SerializeField] private Slider timerSlider;
    [SerializeField] private float maxSliderValue;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pauseButton;
    private void OnEnable()
    {
        timerSlider.value = maxSliderValue;
        timerSlider.maxValue = maxSliderValue;
        StartCoroutine(CountdownTime());
    }
    private IEnumerator CountdownTime()
    {
        while(timerSlider.value > 0)
        {
            timerSlider.value -= Time.deltaTime;
            yield return null;
        }
        GameManagerScript.gmInstance.gameState = GameManagerScript.GameStates.LOSE;
        gameOverPanel.SetActive(true);
        pauseButton.SetActive(false);
        gameObject.SetActive(false);
    }
    public void setSliderValue(float val)
    {
        timerSlider.value = val;
    }
    public float getSliderValue()
    {
        return timerSlider.value;
    }
}
