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
    [SerializeField]Sprite[] AttackingSprite;
    Sprite[] StopAttacking;
    bool tired = false;

    public void SetEnemyStats(EnemysStates ES)
    {
        enemysStates = ES;
        sr= GetComponent<SpriteRenderer>();
        sr.sprite = enemysStates.enemySprite;
        StartingPos = transform.position += new Vector3(transform.position.x, 0.7f+ sr.size.y/2,0);
        // Debug.Log(StartingPos);
        sm = GetComponent<SpriteMan>();
        CameraShake.instance.addTargetGroup(transform);
        targetTrans = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        StartCoroutine("attackCD");
        hitimpact = GameObject.Find("HitImpact");
        GETANIMATION();
    }

    public void GETANIMATION()
    {
        StopAttacking = Resources.LoadAll<Sprite>($"EnemyAnimation/{enemysStates.Enemy_name}/{enemysStates.Enemy_name}_stop");
        AttackingSprite = Resources.LoadAll<Sprite>($"EnemyAnimation/{enemysStates.Enemy_name}/{enemysStates.Enemy_name}_attack");
    }

 

    IEnumerator attackCD()
    {
        sm.RunAnimation(StopAttacking, enemysStates.speed);
        yield return new WaitForSeconds(enemysStates.speed);
        StartAttack();
    }

    void StartAttack()
    {

        int MiddleOrSide = Random.Range(0, 2);
        //int MiddleOrSide = 0;
        if (MiddleOrSide == 0)// middle attack
        {
            // parryable = true;
            Preattack = (transform.DOMoveY(transform.position.y + .5f, enemysStates.speed));
        }
        else
        {
            Preattack = (transform.DOMoveX(transform.position.x + Random.Range(0, 2) * 2 - 1, enemysStates.speed));

        }
        attacking = true;

        Preattack.OnComplete(() =>
        {
            StartCoroutine("showStrong", MiddleOrSide);

        });
    }

    IEnumerator showStrong(int MiddleOrSide)
    {

        int a = 0;
        float Delay = 0.2f;
        strongAttack = (Random.Range(0, 1) == 0) ? true : false;
        strongAttack = true;
        if (strongAttack)
        {
            GameObject HI = Instantiate(hitimpact, transform) as GameObject;
            HI.transform.position = transform.position;
            Delay = 1;
            Destroy(HI,Delay);
        }
        yield return new WaitForSeconds(Delay); 
        a = (transform.position.x >= 0) ? -1 : 1;

        Quaternion targetRotation = Quaternion.LookRotation((targetTrans.transform.position + targetTrans.transform.right * a));
        transform.DORotateQuaternion(Quaternion.Euler(0, 0, targetRotation.z + 180), enemysStates.speed);
        Debug.Log(targetRotation.z);
        sm.RunAnimation(AttackingSprite, enemysStates.speed);

        attacking = false;
        if (MiddleOrSide == 0)
        {
            transform.DOMove(targetTrans.transform.position + targetTrans.transform.up, enemysStates.speed).OnUpdate(
                () =>
                {
                    becomeParry();
                }
                ).OnComplete(gotHit);
        }
        else
        {

            transform.DOMove((targetTrans.transform.position + targetTrans.transform.right * a), enemysStates.speed).OnComplete(gotHit);
        }
    }

    private void becomeParry()
    {
        if(sm.GetCurrentFrame()>=enemysStates.ParryStart && sm.GetCurrentFrame() <= enemysStates.ParryEnd && strongAttack==false)
        {
            Debug.Log(sm.GetCurrentFrame());
            parryable = true;
            hitimpact.transform.position = transform.position;
            return;
        }
        hitimpact.transform.parent = null;
        hitimpact.transform.position=new Vector3(-10000,-10000);
        parryable = false;
    }

    private void moveBack()
    {
        if (strongAttack == false)
        {
            transform.DOMove(StartingPos, enemysStates.speed).OnComplete(() =>
            {
                StartCoroutine("attackCD");
            });
            return;
        }
        tired = true;
    }

    void returnToStartingPos()
    {
        transform.position = StartingPos; 
    }

    private void gotHit()
    {

        if (parryable || tired) // get Player hit
        {
            //if player hitting as well
            {
                StartingPos = new Vector3(targetTrans.transform.position.x, transform.position.y + targetTrans.size.y / 2 + 1.4f, targetTrans.transform.position.z);
                transform.DOMove(StartingPos, 1).OnComplete(() =>
                {
                    StartCoroutine("attackCD");
                });
                return;
            }
        }
        if (true) // get Player DodgeLeft / right to see if correct
        {
            CameraShake.instance.ShakeContorl(5 *(strongAttack?2:1) ,0.4f);
            //GameObject gameObject = Instantiate(hitimpact, transform.position, Quaternion.identity) as GameObject;
            //Destroy(gameObject, 0.7f);
            StopCoroutine("attackCD");
            targetTrans.transform.DOMoveY(targetTrans.transform.position.y - (strongAttack? enemysStates.strength*1.5f : enemysStates.strength), 1).OnComplete(() =>
            {
                StartingPos = new Vector3(targetTrans.transform.position.x, targetTrans.transform.position.y + targetTrans.size.y / 2 + 1.4f, targetTrans.transform.position.z);
           
                transform.DOMove(StartingPos, 1).OnComplete(() =>
                {
                    StartCoroutine("attackCD");
                });
            });
            sr.sprite = AttackingSprite[0];
            return;
        }
        moveBack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gotHit();
  
    }

}


