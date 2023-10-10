using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
public class enemy : MonoBehaviour
{
    EnemysStates enemysStates;
    private SpriteRenderer sr;
     
    private bool parryable = false;
    private bool attacking = false;
    private bool attackingStrong = false;
    private bool strongAttack = false;
    private bool hitable = false;
    

    Vector3 StartingPos;
    Vector3 targetPos;
    Tween Preattack;
    SpriteMan sm;
    [SerializeField]SpriteRenderer targetTrans;
    GameObject hitimpact;
    //animation
    Sprite[] AttackingSprite;

    public void SetEnemyStats(EnemysStates ES)
    {
        enemysStates = ES;
        sr= GetComponent<SpriteRenderer>();
        sr.sprite = enemysStates.enemySprite;
        StartingPos = transform.position += new Vector3(transform.position.x, 0.7f+ sr.size.y/2,0);
        // Debug.Log(StartingPos);
        sm = GetComponent<SpriteMan>();
        CameraShake.instance.addTargetGroup(transform);
        targetTrans = GameObject.Find("player").GetComponent<SpriteRenderer>();
        StartCoroutine("attackCD");
        hitimpact = GameObject.Find("HitImpact");


    }

    IEnumerator attackCD()
    {
        yield return new WaitForSeconds(enemysStates.speed);
        StartAttack();
    }

    void StartAttack()
    {
        
        int MiddleOrSide = Random.Range(0, 2);
        if (MiddleOrSide == 0)// middle attack
        {
           // parryable = true;
            Preattack=(transform.DOMoveY(transform.position.y+.5f, enemysStates.speed));
        }
        else
        {
            Preattack=(transform.DOMoveX(transform.position.x+ Random.Range(0, 2) * 2 - 1, enemysStates.speed));
        }
        attacking=true;


        Preattack.OnComplete(() =>
        {
      
            sm.RunAnimation(enemysStates.AttackAnimation,enemysStates.speed);
         
            attacking = false;
            if(MiddleOrSide == 0)
            {
                transform.DOMove(targetTrans.transform.position, enemysStates.speed).OnComplete(gotHit); 
            }
            else
            {
                int a = (transform.position.x==1) ? -1 : 1;
                transform.DOMove(new Vector3(targetTrans.transform.position.x+(a), targetTrans.transform.position.y, targetTrans.transform.position.z), enemysStates.speed).OnComplete(gotHit);
            }
        });
    }

    private void moveBack()
    {
        transform.DOMove(StartingPos, enemysStates.speed).OnComplete(() =>
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
            GameObject gameObject = Instantiate(hitimpact, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject, 0.7f);
            StopCoroutine("attackCD");
            targetTrans.transform.DOMoveY(targetTrans.transform.position.y - enemysStates.strength, 1).OnComplete(() =>
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
  
    }

}


