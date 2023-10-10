using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMan : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Sprite[] animationToRunThoruh=null;
    int current;
    float animSpeed;
    IEnumerator RunSprite()
    {
        yield return new WaitForSeconds(animSpeed*0.1f);
        spriteRenderer.sprite = animationToRunThoruh[current];
        checkIfSpriteEnding();
    }

    void checkIfSpriteEnding()
    {
        if(current >= animationToRunThoruh.Length-1)
        {
            return;
        }
        current++;
        StartCoroutine("RunSprite");

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

    public void RunAnimation(Sprite[] anim,float speed)
    {
        current = 0;
        animSpeed = speed;
        animationToRunThoruh = anim;
        StopCoroutine("RunSprite");
        StartCoroutine("RunSprite");
    }
}
