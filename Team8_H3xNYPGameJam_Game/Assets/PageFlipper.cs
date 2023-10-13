using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PageFlipper : MonoBehaviour
{
    [SerializeField] private Image currentTutorialImage;
    public Sprite[] tutorialImages;
    int currentidx;

    private void Awake()
    {
        currentidx = 0;
        
    }


    public void FlipNextPage()
    {
        if(currentidx < tutorialImages.Length-1)
        {
            currentidx++;
            currentTutorialImage.sprite = tutorialImages[currentidx];
        }
    }

    public void FlipPrevPage()
    {
        if (currentidx > 0)
        {
            currentidx--;
            currentTutorialImage.sprite = tutorialImages[currentidx];
        }
    }
    public void OnCloseButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
