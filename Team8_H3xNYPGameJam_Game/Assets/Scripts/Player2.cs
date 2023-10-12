using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Player2 : MonoBehaviour
{

    public static Player2 instance;
    public AudioClip[] swordSounds;
    public AudioClip[] hurtSounds;

    private AudioSource AS;
    private bool isAttacking = false; // Flag to prevent multiple attacks

    AnimatorStateInfo currentAnimationState;

    public float playerStrength;
    public TextMeshProUGUI playerStrengthText;
    public float playerWeight;
    public TextMeshProUGUI playerWeightText;
    public float playerSpeed;
    public TextMeshProUGUI playerSpeedText;

    public int enemiesDefeated;
    public int totalenemiesDefeated;
    [SerializeField] Sprite[] idleAnim;
    [SerializeField] Sprite[] attackingAnim;
    [SerializeField] Sprite[] dodgeLeftAnim;
    [SerializeField] Sprite[] dodgeRightAnim;
    [SerializeField] Sprite[] dodgeLeftBackAnim;
    [SerializeField] Sprite[] dodgeRightBackAnim;
    [SerializeField] Sprite[] hurtAnim;
    Sprite[] current;
    SpriteMan sm;
    public bool CantHit;
    public Canvas shopCanvas;

    public Sprite[] testing;
    playerState currentPlayerState = playerState.IDLE;
    playerState oldPlayerState;

    Tween moving;
    bool isBack = true;

    Vector3 TrueOGPos;
    public playerState GetPlayerState()
    {
        return currentPlayerState;
    }

    Vector3 startLocation;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        TrueOGPos = transform.position;
        startLocation = transform.position;
        shopCanvas.gameObject.SetActive(false);
        enemiesDefeated = 0;


        sm = GetComponent<SpriteMan>();
        gameObject.AddComponent<SpriteMan>();
        AS = GetComponent<AudioSource>();
        GetAnimation();
    }

    private void GetAnimation()
    {
        idleAnim = Resources.LoadAll<Sprite>("PlayerAnimation/player_idle");
        attackingAnim = Resources.LoadAll<Sprite>("PlayerAnimation/player_attack");
        dodgeLeftAnim = Resources.LoadAll<Sprite>("PlayerAnimation/Player_dodge_left");
        dodgeLeftBackAnim = Resources.LoadAll<Sprite>("PlayerAnimation/Player_dodge_left Back");
        dodgeRightAnim = Resources.LoadAll<Sprite>("PlayerAnimation/Player_dodge_right");
        dodgeRightBackAnim = Resources.LoadAll<Sprite>("PlayerAnimation/Player_dodge_right_Back");
        hurtAnim = Resources.LoadAll<Sprite>("PlayerAnimation/player_hurt");
    }
    public Vector3 startingLocation()
    {
        return startLocation;
    }

    public void updateStaringLoc()
    {
        startLocation = new Vector3(0, transform.position.y, 0);
        CantHit = false;
    }

    private void Update()
    {

        if (!shopCanvas.gameObject.activeSelf)
        {
            if (isBack)
            {
                CheckMobileInput();
                CheckPCInput();
            }
        }

        playAnimation();

    }

    //RANDOMISE A SOUND TO PLAY
    void PlayRandomSound(AudioClip[] soundList)
    {
        int idx = Random.Range(0, soundList.Length);
        Debug.Log("Sound Played " + soundList[idx].name);
        // Play the sound at the specified index
        AS.clip = soundList[idx];
        AS.Play();
    }
    //

    //TRACK HOW MANY ENEMIES ARE DEFEATED
    private void EnemyTracker()
    {
        if (enemiesDefeated >= 3)
        {
            shopCanvas.gameObject.SetActive(true);

        }
    }




    //WHEN ANIMATION OTHER THAN IDLE HAS STOPPED PLAYING

    private void playAnimation()
    {
        if(!sm.ReturnDone() || oldPlayerState == currentPlayerState)
        {
            return;
        }
        switch (currentPlayerState)
        {
            case playerState.IDLE:
                sm.RunAnimation(idleAnim, playerSpeed);
                break;
            case playerState.ATTACK:
                sm.RunAnimation(attackingAnim, playerSpeed);
                break;
            case playerState.DODGELEFT:
                sm.RunAnimation(dodgeLeftAnim, playerSpeed);
                break;
            case playerState.DODGELEFTBACK:
                sm.RunAnimation(dodgeLeftBackAnim, playerSpeed);
                break;
            case playerState.DODGERIGHTBACK:
                sm.RunAnimation(dodgeRightBackAnim, playerSpeed);
                break;
            case playerState.DODGERIGHT:
                sm.RunAnimation(dodgeRightAnim, playerSpeed);
                break;
            case playerState.HURT:
                sm.RunAnimation(hurtAnim, 0.1f);
                break;
        }

        oldPlayerState = currentPlayerState;

    }

    //MOBILE CONTROLS
    private void CheckMobileInput()
    {
        // Check for swipe
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 deltaPosition = touch.deltaPosition;

                // DETECT WHETHER PLAYER SWIPES UP
                if (deltaPosition.y > Screen.height * 0.05f)
                {
                    Attack();
                }
                //IF NO, THEN IT MUST BE SIDE SWIPE
                else
                {
                    // SWIPE LEFT
                    if (deltaPosition.x < -Screen.width * 0.05f)
                    {
                        DodgeLeft();
                    }
                    // SWIPE RIGHT
                    else if (deltaPosition.x > Screen.width * 0.05f)
                    {
                        DodgeRight();
                    }
                }
            }
        }
    }

    //KEYBOARD CONTROLS
    private void CheckPCInput()
    {
        // Check for A key (dodge left)
        if (Input.GetKeyDown(KeyCode.A))
        {
            isBack = false;
            DodgeLeft();

            moving = transform.DOMoveX(transform.position.x - 0.6f, playerSpeed).OnComplete(callMoveBack);
            currentPlayerState = playerState.DODGELEFT;
        }

        // Check for D key (dodge right)
        if (Input.GetKeyDown(KeyCode.D))
        {
            isBack = false;
            DodgeRight();
            moving = transform.DOMoveX(transform.position.x + 0.6f, playerSpeed).OnComplete(callMoveBack);
            currentPlayerState = playerState.DODGERIGHT;
        }

        // Check for W key (attack)
        if (Input.GetKeyDown(KeyCode.W))
        {
            isBack = false;
            Attack();
            if (enemyManeger.EM.SendHIt() == true)
            {
                CantHit = true;
            }

            moving = transform.DOMoveY(transform.position.y + 0.5f, playerSpeed).OnComplete(() =>
            {
               if(!CantHit)
                {
                    CantHit = false;
                    callMoveBack();
                }
            });
           
            currentPlayerState = playerState.ATTACK;
        }
    }


    IEnumerator moveBack()
    {
        yield return new WaitForSeconds(0.7f);
        Debug.Log("asdasda");
        if (currentPlayerState == playerState.DODGERIGHT)
        {
            currentPlayerState = playerState.DODGERIGHTBACK;
        }
        else if (currentPlayerState == playerState.DODGELEFT)
        {

            currentPlayerState = playerState.DODGELEFTBACK;

        }
       
        transform.DOMove(startLocation, playerSpeed).OnComplete(() => { isBack = true;
            currentPlayerState = playerState.IDLE;
        });
    }

    void callMoveBack()
    {
        StartCoroutine("moveBack");
    }
    public void InstantMove()
    {
        transform.DOMove(startLocation, playerSpeed).OnComplete(() => { isBack = true; });
    }

    //WHAT HAPPENS WHEN PLAYER DODGES
    private void DodgeLeft()
    {

        if (currentAnimationState.IsName("player_idle"))
        {
            
        }
    }

    private void DodgeRight()
    {
        if (currentAnimationState.IsName("player_idle"))
        {

           
            //animController.SetBool("dodgeDirection", "l");

            Debug.Log("Dodge Right");
        }
    }
    //

    //WHAT HAPPENS WHEN PLAYER IS HURT
    private void hurt()
    {
        if (currentAnimationState.IsName("player_idle")
          && !isAttacking)
        {
           
            Debug.Log("Hurt");
        }
    }

    //WHAT HAPPENS WHEN PLAYER ATTACKS
    private void Attack()
    {

        if (!isAttacking
         && currentAnimationState.IsName("player_idle")
            ) // Only allow attacking if not already attacking
        {
    
            //RANDOMISE A PUNCH SOUND TO PLAY
            PlayRandomSound(swordSounds);
            callMoveBack();

            enemiesDefeated += 1;
            totalenemiesDefeated += 1;

            EnemyTracker();
            isAttacking = true;
        }
    }

    public void AddScore()
    {

        enemiesDefeated++;
    }

    public void StopMoving()
    {
        moving.Kill();
    }
    public void stopMove()
    {
        StopCoroutine("moveBack");
    }
    public void GotHit()
    {
        if(currentPlayerState==playerState.HURT)
        {
            currentPlayerState = playerState.IDLE;
            sm.RunAnimation(idleAnim, 1);
            return;
        }
        currentPlayerState = playerState.HURT;
        sm.RunAnimation(hurtAnim,1);
    }

    public void playerMoveBack()
    {
        transform.DOMove(TrueOGPos,0.1f).OnComplete(() =>
        {
            AddScore();
            isBack = true;
            updateStaringLoc();
        });
    }

    public void PlayerFall() { 
    
    }
}

public enum playerState
{
    IDLE,
    DODGERIGHT,
    DODGELEFT,
    ATTACK,
    HURT,
    DODGELEFTBACK,
    DODGERIGHTBACK
}