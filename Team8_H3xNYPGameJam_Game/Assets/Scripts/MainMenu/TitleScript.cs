using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    [SerializeField] private List<Sprite> imageList = new List<Sprite>();
    private Image titleImage;
    private int imageIndex = 0;
    private Vector3 originalScale;
    private Vector3 scaleTo;
    private Tween scaleTween;
    void Start()
    {
        titleImage = GetComponent<Image>();
        originalScale = GetComponent<RectTransform>().localScale;
        scaleTo = originalScale * 1.25f;
        StartCoroutine(SwitchImage());
        scaleTween = transform.DOScale(scaleTo, 2.0f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    private IEnumerator SwitchImage()
    {
        titleImage.sprite = imageList[imageIndex];
        if (imageIndex == 0)
            imageIndex = 1;
        else imageIndex = 0;
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(SwitchImage());
    }
    public void StopScaling()
    {
        scaleTween.Kill();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
