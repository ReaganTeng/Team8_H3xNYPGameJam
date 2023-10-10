using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    [SerializeField] private ScaleTextScript scaleTextScript;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
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
}
