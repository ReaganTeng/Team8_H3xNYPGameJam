using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using static GameManagerScript;

public class Player2 : MonoBehaviour
{

    public static Player2 instance;
    public AudioClip[] swordSounds;
    public AudioClip[] hurtSounds;
    public AudioClip[] dodgeSounds;

    private AudioSource AS;


    public float playerStrength;
    public TextMeshProUGUI playerStrengthText;
    public float playerWeight;
    public TextMeshProUGUI playerWeightText;
    public float playerSpeed;

    public int enemiesDefeated;
    public int totalenemiesDefeated;
    [SerializeField] Sprite[] idleAnim;
    [SerializeField] Sprite[] attackingAnim;
    [SerializeField] Sprite[] dodgeLeftAnim;
    [SerializeField] Sprite[] dodgeRightAnim;
    [SerializeField] Sprite[] dodgeLeftBackAnim;
    [SerializeField] Sprite[] dodgeRightBackAnim;
    [SerializeField] Sprite[] hurtAnim;
    [SerializeField] Sprite[] climbAnim;
    Sprite[] current;
    SpriteMan sm;
    public bool CantHit;
    public Canvas shopCanvas;

    public Sprite[] testing;
    public playerState currentPlayerState = playerState.IDLE;
    playerState oldPlayerState;

    Tween hit;
    Tween moving;
    public bool isBack = true;

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

    public void Recover()
    {
        startLocation = new Vector3(0, levelMang.Instance.getMinHeight() + 0.3f, 0);
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
        climbAnim = Resources.LoadAll<Sprite>("PlayerAnimation/Player_climb");
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

        if (!shopCanvas.gameObject.activeSelf && GameManagerScript.gmInstance.gameState== GameStates.PLAYING)
        {
            if (isBack)
            {
                CheckMobileInput();
                CheckPCInput();
            }
        }

        if(GameManagerScript.gmInstance.gameState == GameStates.FALLING)
        {
            if(sm.ReturnDone())
            {

                sm.RunAnimation(climbAnim, 1);
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
    public  bool EnemyTracker()
    {
        if (enemiesDefeated >= 1) //CHANGEE TO 3
        {
            shopCanvas.gameObject.SetActive(true);
            return true;

        }
        return false;
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
                sm.RunAnimation(attackingAnim, 1);
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
                if (deltaPosition.y > Screen.height * 0.1f)
                {
                    Attack();
                }
                //IF NO, THEN IT MUST BE SIDE SWIPE
                else
                {
                    // SWIPE LEFT
                    if (deltaPosition.x < -Screen.width * 0.1f)
                    {
                        DodgeLeft();
                    }
                    // SWIPE RIGHT
                    else if (deltaPosition.x > Screen.width * 0.1f)
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
            DodgeLeft();
           
        }

        // Check for D key (dodge right)
        if (Input.GetKeyDown(KeyCode.D))
        {
            DodgeRight();
        }

        // Check for W key (attack)
        if (Input.GetKeyDown(KeyCode.W))
        {
            Attack();
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
        hit = transform.DOMove(startLocation, playerSpeed).OnComplete(() => { isBack = true;
            currentPlayerState = playerState.IDLE;

        hit.OnUpdate(() =>{
            if (GameManagerScript.gmInstance.gameState == GameManagerScript.GameStates.FALLING)
            {
                hit.Kill();
            }
        });
        });
    }

    void callMoveBack()
    {
        StartCoroutine("moveBack");
    }
    public void InstantMove()
    {
        transform.DOMove(startLocation, playerSpeed).OnComplete(() => { isBack = true; currentPlayerState = playerState.IDLE; });
    }

    //WHAT HAPPENS WHEN PLAYER DODGES
    private void DodgeLeft()
    {

        isBack = false;
        moving = transform.DOMoveX(transform.position.x - 0.6f, playerSpeed).OnComplete(callMoveBack);
        currentPlayerState = playerState.DODGELEFT;
        //PLAY A RANDOMISED DODGE SOUND
        PlayRandomSound(dodgeSounds);
    }

    private void DodgeRight()
    {
        isBack = false;
        moving = transform.DOMoveX(transform.position.x + 0.6f, playerSpeed).OnComplete(callMoveBack);
        currentPlayerState = playerState.DODGERIGHT;
        //PLAY A RANDOMISED DODGE SOUND
        PlayRandomSound(dodgeSounds);

    }
    //

    //WHAT HAPPENS WHEN PLAYER IS HURT
    public void hurt()
    {
        if (currentPlayerState == playerState.HURT)
        {
            currentPlayerState = playerState.IDLE;
            sm.RunAnimation(idleAnim, 1);
            return;
        }
        currentPlayerState = playerState.HURT;
        sm.RunAnimation(hurtAnim, 1);
        PlayRandomSound(hurtSounds);


        Debug.Log("Hurt");
        
    }

    //WHAT HAPPENS WHEN PLAYER ATTACKS
    private void Attack()
    {

        isBack = false;
        if (enemyManeger.EM.SendHIt() == true)
        {
            CantHit = true;
        }
        moving = transform.DOMoveY(transform.position.y + 0.5f, playerSpeed).OnComplete(() =>
        {
            if (!CantHit)
            {
                CantHit = false;
                callMoveBack();
            }
        });
        //currentPlayerState = playerState.ATTACK;

        PlayRandomSound(swordSounds);


       
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
        currentPlayerState=playerState.ATTACK;
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