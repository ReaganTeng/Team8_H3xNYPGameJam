using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleTextScript : MonoBehaviour
{
    private Vector3 originalScale;
    private Vector3 scaleTo;
    private Tween scaleTween;
    void Start()
    {
        originalScale = GetComponent<RectTransform>().localScale;
        scaleTo = originalScale * 1.5f;

        scaleTween = transform.DOScale(scaleTo, 2.0f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
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
