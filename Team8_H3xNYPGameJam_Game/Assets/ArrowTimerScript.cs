using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowTimerScript : MonoBehaviour
{
    [SerializeField] private Image timerFill;
    private void OnEnable()
    {
        StartCoroutine(CountdownTime());
    }
    private void OnDisable()
    {
        timerFill.fillAmount = 1;
    }
    private IEnumerator CountdownTime()
    {
        while(timerFill.fillAmount > 0)
        {
            timerFill.fillAmount -= Time.deltaTime / 7;
            yield return null;
        }
        Debug.Log("ass");
        GameManagerScript.gmInstance.gameState = GameManagerScript.GameStates.LOSE;
    }
}
