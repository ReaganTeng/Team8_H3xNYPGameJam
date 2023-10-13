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
    Tween AttackingNOW;
    SpriteMan sm;
    [SerializeField]SpriteRenderer targetTrans;
    GameObject hitimpact;
    //animation
    [SerializeField]Sprite[] AttackingSprite;
    [SerializeField] Sprite[] hurtSprite;
    Sprite[] StopAttacking;
    bool tired = false;
    bool dead = false;
    bool hurt = false;
    int LOR = 0;
    int randomLeftOrRight = 0;
    Tween FallOrNo;
    public void SetEnemyStats(EnemysStates ES)
    {
        enemysStates = ES;
        sr= GetComponent<SpriteRenderer>();
        sr.sprite = enemysStates.enemySprite;
        StartingPos = transform.position += new Vector3(transform.position.y, sr.size.y,0);
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
        hurtSprite = Resources.LoadAll<Sprite>($"EnemyAnimation/{enemysStates.Enemy_name}/{enemysStates.Enemy_name}_hurt");
    }

 

    IEnumerator attackCD()
    {
        hurt = false;
        sm.RunAnimation(StopAttacking, enemysStates.speed);
        yield return new WaitForSeconds(enemysStates.speed);
        if(!tired)
        {
            StartAttack();
        }
    }

    void StartAttack()
    {
        LOR = 0;
        int MiddleOrSide = Random.Range(0, 2);
        //MiddleOrSide = 0;
        if (MiddleOrSide == 0)// middle attack
        {
            // parryable = true;
            Preattack = (transform.DOMoveY(transform.position.y + .5f, enemysStates.speed));
        }
        else
        {
            randomLeftOrRight = Random.Range(0, 2);
            //randomLeftOrRight = 0;
            LOR = (randomLeftOrRight == 0) ? -1 : 1;
            Preattack = (transform.DOMoveX(transform.position.x + ((randomLeftOrRight * 2 - 1)*(0.6f*levelMang.Instance.getMaxWidth())), enemysStates.speed));

        }
        attacking = true;

        Preattack.OnUpdate(() =>
        {
           

        }).OnComplete(() =>
        {
            //Vector3 vectorToTarget = transform.position - Player2.instance.startingLocation(); ;
            //float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            //Quaternion q = Quaternion.AngleAxis(-angle, Vector3.forward);
            //transform.DORotateQuaternion(q, 1);
            //Vector3 difference = (Player2.instance.startingLocation()) - transform.position;
            //float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            //transform.DORotateQuaternion( Quaternion.Euler(0.0f, 0.0f, rotationZ),1);

            StartCoroutine("showStrong", MiddleOrSide);

        });
    }

    IEnumerator showStrong(int MiddleOrSide)
    {

        float Delay = 0.2f;
        strongAttack = (Random.Range(0, 1) == 0) ? true : false;
        strongAttack = false;
        if (strongAttack)
        {
            GameObject HI = Instantiate(hitimpact, transform) as GameObject;
            HI.transform.position = transform.position;
            Delay = 1;
            Destroy(HI,Delay);
        }

        yield return new WaitForSeconds(Delay); 


     
        sm.RunAnimation(AttackingSprite, enemysStates.speed);

        attacking = false;
        if (MiddleOrSide == 0)
        {
           AttackingNOW= transform.DOMove( Player2.instance.startingLocation() + new Vector3(0,targetTrans.size.y/2,0) , enemysStates.speed).OnUpdate(
                () =>
                {
                    becomeParry();
                }
                ).OnComplete(gotHit);
        }
        else
        {

            AttackingNOW = transform.DOMove(( Player2.instance.startingLocation() + targetTrans.transform.right * -((randomLeftOrRight * 2 - 1) * 0.6f)), enemysStates.speed).OnUpdate(()=>
            {
                becomeParry();
            }).OnComplete(gotHit);
        }
    }

    private void becomeParry()
    {
        if(sm.GetCurrentFrame()>=enemysStates.ParryStart && sm.GetCurrentFrame() <= enemysStates.ParryEnd && strongAttack==false)
        {
            //Debug.Log(sm.GetCurrentFrame());
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
        if (strongAttack == true)
        {
            tired = true;
        }
        transform.DOMove(StartingPos, enemysStates.speed).OnComplete(() =>
        {
            StartCoroutine("attackCD");
        });
   
    }

    void returnToStartingPos()
    {
        transform.position = StartingPos; 
    }

    private void RotateBack()
    {
       // transform.DORotateQuaternion ( Quaternion.Euler(0,0,180),1);
    }

    public bool PlayerHit()
    {
        if (parryable || tired) // get Player hit
        {

            RotateBack();
            tired = false;
            hitimpact.transform.position = new Vector3(-10000, -10000);
            AttackingNOW.Kill();
            Preattack.Kill();
            Player2.instance.stopMove();
            sm.RunAnimation(hurtSprite, 1);
            CameraShake.instance.ShakeContorl(5 * (Player2.instance.playerStrength), 0.4f);
            transform.DOMoveY(StartingPos.y + Player2.instance.playerStrength / enemysStates.Weight, 1).OnComplete(() =>
            {
                checkIfOut();
                if (!dead)
                {
                    StartingPos = new Vector3(0, transform.position.y, 0);
                    transform.DOMove(StartingPos, 0.4f).OnComplete(() =>
                    {
                        StartCoroutine("attackCD");

                        targetTrans.transform.DOMoveY(StartingPos.y - targetTrans.size.y, 0.4f).OnComplete(() =>
                        {
                            Player2.instance.updateStaringLoc();
                            Player2.instance.InstantMove(); 
                        });
                    });
                  
                }
            });
            //StartingPos = new Vector3( Player2.instance.startingLocation().x, transform.position.y + targetTrans.size.y ,  Player2.instance.startingLocation().z);

            return true;
        }
        return false;
    }

    private void gotHit()
    {
        RotateBack();
        if ((LOR == 0 && (Player2.instance.GetPlayerState() == playerState.DODGELEFT || Player2.instance.GetPlayerState() == playerState.DODGERIGHT)) ||
            (LOR== -1 && Player2.instance.GetPlayerState()==playerState.DODGELEFT) ||
            (LOR == 1 && Player2.instance.GetPlayerState() == playerState.DODGERIGHT)) // get Player DodgeLeft / right to see if correct
        {
            moveBack();
           
            return;
        }
        if (Player2.instance.CantHit)
        {
            Player2.instance.CantHit = false;
            sr.sprite = AttackingSprite[0];
            transform.DOMove(StartingPos, 1).OnComplete(() =>
            {
                StartCoroutine("attackCD");
            });
        }
        else
        {
            Player2.instance.stopMove();
            CameraShake.instance.ShakeContorl(5 * (strongAttack ? 2 : 1), 0.4f);
            //GameObject gameObject = Instantiate(hitimpact, transform.position, Quaternion.identity) as GameObject;
            //Destroy(gameObject, 0.7f);
            StopCoroutine("attackCD");
            Player2.instance.StopMoving();
            Player2.instance.GotHit();
            bool NoFall = false;
            FallOrNo = targetTrans.transform.DOMoveY(Player2.instance.startingLocation().y - (strongAttack ? enemysStates.strength * 1.5f : enemysStates.strength) / Player2.instance.playerWeight, 1).OnUpdate(() =>
            {
                if(targetTrans.transform.position.y <= levelMang.Instance.getMinHeight())
                {
                    if(NoFall==false)
                    {

                    NoFall = true;
                    GameManagerScript.gmInstance.createArrows();
                    GameManagerScript.gmInstance.gameState = GameManagerScript.GameStates.FALLING;
                    }
                }

            }).OnComplete(() =>
            {
                if(NoFall)
                {
                    transform.DOMove(StartingPos, 1);
                    return;
                }
                Player2.instance.GotHit();
                Player2.instance.updateStaringLoc();
                StartingPos = new Vector3(Player2.instance.startingLocation().x, Player2.instance.startingLocation().y + targetTrans.size.y, Player2.instance.startingLocation().z);
                Player2.instance.InstantMove();
                transform.DOMove(StartingPos, 1).OnComplete(() =>
                {
                    StartCoroutine("attackCD");
                });
            });
        }
        sr.sprite = AttackingSprite[0];
    }

    public void Ready()
    {
        StartCoroutine("attackCD");
    }

    private void Falling()
    {
        FallOrNo.Kill();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gotHit();
  
    }
     
    void checkIfOut()
    {
        Tween check;
        if(transform.position.y>levelMang.Instance.getMaxHeight())
        {
            transform.GetComponent<SpriteRenderer>().sortingOrder = -2;
            dead = true;
            transform.DOMoveY(0,2);
            check = transform.DOScale(new Vector3(0, 0, 0), 2).OnComplete(()=>
            {
                Player2.instance.enemiesDefeated++;
                Player2.instance.totalenemiesDefeated++;
                Player2.instance.playerMoveBack();
                enemyManeger.EM.SpawnEnemy();
            });
        }
       
    }                                                                                                                                                                      
}


