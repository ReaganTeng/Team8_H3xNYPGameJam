using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private ScaleTextScript scaleTextScript;
    [SerializeField] private TMP_Text startText;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject settingsButton;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    StartCoroutine(fadeOut());
        //}
    }
    private IEnumerator fadeOut()
    {
        scaleTextScript.StopScaling();
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }
    }
    public void onSettingsButtonClicked()
    {
        Color newColor = new Vector4(0, 0, 0, 0);
        startText.color = newColor;
        settingsButton.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void onSettingsBackButtonClicked()
    {
        Color newColor = new Vector4(0, 0, 0, 1);
        startText.color = newColor;
        settingsButton.SetActive(true);
        settingsPanel.SetActive(false);
    }
}
