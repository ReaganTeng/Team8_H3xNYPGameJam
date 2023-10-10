using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
public class enemy : MonoBehaviour
{
    private float speed;
    private float weight;
    private float strength;
    private Sprite enemySprite;
    private SpriteRenderer sr;
     
    private bool parryable = false;
    private bool attacking = false;
    private bool attackingStrong = false;
    private bool strongAttack = false;
    private bool hitable = false;
    

    Vector3 StartingPos;
    Vector3 targetPos;
    Tween Preattack = DOTween.Sequence();

    public void SetEnemyStats(float s, float w, float str,Sprite ES)
    {
        speed = s;
        weight= w; 
        strength = str;
        sr= GetComponent<SpriteRenderer>();
        sr.sprite = ES;
        StartingPos = transform.position += new Vector3(transform.position.x, 0.7f+ sr.size.y/2,0);
        Debug.Log(StartingPos);
        targetLocation();
        StartCoroutine("attackCD");     
    }

    IEnumerator attackCD()
    {
        yield return new WaitForSeconds(speed);
        StartAttack();

    }
    private void Update()
    {

    }

    void StartAttack()
    {
        
        int MiddleOrSide = Random.Range(0, 2);
        if (MiddleOrSide == 0)// middle attack
        {
            parryable = true;
            Preattack=(transform.DOMoveY(1.5f, speed));
        }
        else
        {
            Preattack=(transform.DOMoveX(Random.Range(0, 2) * 2 - 1, speed));
        }
        attacking=true;


        Preattack.OnComplete(() =>
        {
            Debug.Log(transform.position);
            attacking = false;
            if(MiddleOrSide == 0)
            {
                transform.DOMove(targetPos,speed);
            }
            else
            {
                int a = (transform.position.x==1) ? -1 : 1;
                transform.DOMove(new Vector3(targetPos.x+(a), targetPos.y, targetPos.z),speed).OnComplete(moveBack);
            }
        });
    }

    private void moveBack()
    {
        transform.DOMove(StartingPos, speed).OnComplete(() =>
        {
           StartCoroutine("attackCD");
        });
    }

    void returnToStartingPos()
    {
        transform.position = StartingPos; 
    }

    private void gotHit()
    {
        if (hitable || parryable)
        {
            
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gotHit();

        targetLocation();
    }

    void targetLocation()
    {
        targetPos = new Vector3(0, transform.position.y-0.7f-sr.size.y/2 , 0);
    }
}


