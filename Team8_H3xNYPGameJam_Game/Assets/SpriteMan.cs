using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMan : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Sprite[] animationToRunThoruh;
    int current;
    float animSpeed;
    IEnumerator RunSprite()
    {
        yield return new WaitForSeconds(animSpeed*0.1f);
        Debug.Log(current);
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
    bool ReturnDone()
    {
        if (current >= animationToRunThoruh.Length - 1)
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
        StartCoroutine("RunSprite");
    }
}
