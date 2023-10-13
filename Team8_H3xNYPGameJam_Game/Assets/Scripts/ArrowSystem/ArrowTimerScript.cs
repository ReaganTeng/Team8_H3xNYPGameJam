using DG.Tweening;
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
        gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
        Player2.instance.transform.DOMoveY(Player2.instance.transform.position.y-1, 1);
        Player2.instance.transform.DOScale(Vector3.zero, 1).OnComplete(GO);
       
    }
    public void setSliderValue(float val)
    {
        timerSlider.value = val;
    }
    public float getSliderValue()
    {
        return timerSlider.value;
    }
    public void GO()
    {
        GameManagerScript.gmInstance.gameState = GameManagerScript.GameStates.LOSE;
        gameOverPanel.SetActive(true);
        pauseButton.SetActive(false);
        gameObject.SetActive(false);
    }
}
