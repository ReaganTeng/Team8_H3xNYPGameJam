using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMan : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Sprite[] animationToRunThoruh=null;
    int current;
    float animSpeed;
    float frameDuration;
    IEnumerator RunSprite()
    {
        if (current < animationToRunThoruh.Length)
        {
            spriteRenderer.sprite = animationToRunThoruh[current];
            float frameDuration = animSpeed / animationToRunThoruh.Length;
            yield return new WaitForSeconds(frameDuration);
            StartCoroutine("RunSprite");
            current++;
        }
    }

    public bool ReturnDone()
    {
        
        if ( animationToRunThoruh == null || current >= animationToRunThoruh.Length - 1)
        {
            return true;
        }
        return false;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public int GetCurrentFrame()
    {
        return current;
    }

    public void RunAnimation(Sprite[] anim,float speed)
    {
        if(anim==null || anim.Length == 0)
        {
            return;
        }
        current = 0;
        animationToRunThoruh = anim;
        animSpeed =  speed;
        StopCoroutine("RunSprite");
        StartCoroutine("RunSprite");
    }
}
