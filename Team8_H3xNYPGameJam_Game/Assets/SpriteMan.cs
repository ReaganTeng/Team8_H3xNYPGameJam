using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMan : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Sprite[] animationToRunThoruh;
    int current;
    
    IEnumerable RunSprite()
    {
        yield return new WaitForEndOfFrame();
        spriteRenderer.sprite = animationToRunThoruh[current];
    }

    void checkIfSpriteEnding()
    {
        if(current >= animationToRunThoruh.Length)
        {
            return;
        }
        current++;
        StartCoroutine("RunSprite");

    }
    private void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void RunAnimation(Sprite[] anim)
    {
        current = 0;
        animationToRunThoruh = anim;
        StartCoroutine("RunSprite");
    }
}
