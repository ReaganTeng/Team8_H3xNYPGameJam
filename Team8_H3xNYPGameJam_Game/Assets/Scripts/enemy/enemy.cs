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
    [SerializeField]SpriteRenderer targetTrans;

    public void SetEnemyStats(float s, float w, float str,Sprite ES)
    {
        speed = s;
        weight= w; 
        strength = str;
        sr= GetComponent<SpriteRenderer>();
        sr.sprite = ES;
        StartingPos = transform.position += new Vector3(transform.position.x, 0.7f+ sr.size.y/2,0);
        Debug.Log(StartingPos);
        CameraShake.instance.addTargetGroup(transform);
        targetTrans = GameObject.Find("player").GetComponent<SpriteRenderer>();
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
           // parryable = true;
            Preattack=(transform.DOMoveY(transform.position.y+.5f, speed));
        }
        else
        {
            Preattack=(transform.DOMoveX(transform.position.x+ Random.Range(0, 2) * 2 - 1, speed));
        }
        attacking=true;


        Preattack.OnComplete(() =>
        {
            Debug.Log(transform.position);
            attacking = false;
            if(MiddleOrSide == 0)
            {
                transform.DOMove(targetTrans.transform.position, speed).OnComplete(moveBack); 
            }
            else
            {
                int a = (transform.position.x==1) ? -1 : 1;
                transform.DOMove(new Vector3(targetTrans.transform.position.x+(a), targetTrans.transform.position.y, targetTrans.transform.position.z),speed).OnComplete(gotHit);
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
            //if player hitting as well
            {

            return;
            }
        }
        if (true)
            {
            CameraShake.instance.Shake();

            StopCoroutine("attackCD");
            targetTrans.transform.DOMoveY(targetTrans.transform.position.y - strength, 1).OnComplete(() =>
            {
                StartingPos = new Vector3(targetTrans.transform.position.x, targetTrans.transform.position.y + targetTrans.size.y / 2 + 1.4f, targetTrans.transform.position.z);

                transform.DOMove(StartingPos, 1).OnComplete(() =>
                {
                    StartCoroutine("attackCD");
                });
            });
            return;
        }
        moveBack();
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


