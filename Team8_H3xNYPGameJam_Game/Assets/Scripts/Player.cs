using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animController;
    public AudioClip[] swordSounds;
    public AudioClip[] hurtSounds;
    public AudioClip[] dodgeSounds;

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
    int totalenemiesDefeated;
    [SerializeField] Sprite[] idleAnim;
    [SerializeField] Sprite[] attackingAnim;
    [SerializeField] Sprite[] dodgeAnim;
    [SerializeField] Sprite[] hurtAnim;
    Sprite[] current;
    SpriteMan sm;
    public Canvas shopCanvas;

    public Sprite[] testing;
    playerState currentPlayerState=playerState.IDLE;
    playerState oldPlayerState;

    private void Start()
    {
        oldPlayerState = currentPlayerState;
        shopCanvas.enabled = false;
        enemiesDefeated = 0;

        playerStrength = 1.0f;
        playerStrengthText.text = playerStrength.ToString();
        playerWeight = 1.0f;
        playerWeightText.text = playerWeight.ToString();
        playerSpeed = 1.0f;
        playerSpeedText.text = playerSpeed.ToString();

        sm=GetComponent<SpriteMan>();
        rb = GetComponent<Rigidbody2D>();
        animController = GetComponent<Animator>();
        gameObject.AddComponent<SpriteMan>();
        AS = GetComponent<AudioSource>();

    }

    private void GetAnimation()
    {
        idleAnim = Resources.LoadAll<Sprite>("PlayerAnimation/MC_attack");
        attackingAnim = Resources.LoadAll<Sprite>("PlayerAnimation/boxer_attack");
        dodgeAnim = Resources.LoadAll<Sprite>("PlayerAnimation/boxer_dodge");
        hurtAnim = Resources.LoadAll<Sprite>("PlayerAnimation/MC_attack");
    }

    private void Update()
    {
        currentAnimationState = animController.GetCurrentAnimatorStateInfo(0);

        if (!shopCanvas.enabled)
        {
            CheckMobileInput();
            CheckPCInput();
        }
       
        //MY ANIMATION
        animationCheck();

        //JUN KAI'S ANIMATION
       
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
        if(enemiesDefeated >= 3)
        {
            shopCanvas.enabled = true;
        }
    }


    //WHEN ANIMATION OTHER THAN IDLE HAS STOPPED PLAYING
    private void animationCheck()
    {
        currentAnimationState = animController.GetCurrentAnimatorStateInfo(0);

        if (animController.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
            && !currentAnimationState.IsName("player_idle"))
        {
            
            animController.SetBool("attack", false);
            animController.SetFloat("dodge", 0);
            animController.SetBool("hurt", false);

            currentPlayerState = playerState.IDLE;

            isAttacking = false; // Reset attack flag
        }


        if(currentAnimationState.IsName("player_dodge")
            || currentAnimationState.IsName("player_attack"))
        {
            animController.speed = currentAnimationState.speed * playerSpeed;
        }
        else
        {
            animController.speed = 1;
        }
    }

    private void playAnimation()
    {
        if (idleAnim != null
           && attackingAnim != null
           && dodgeAnim != null
           && hurtAnim != null
           && sm != null)
        {
            if (oldPlayerState == currentPlayerState)
            {
                if (sm.ReturnDone())
                {
                    if (currentPlayerState != playerState.IDLE)
                    {
                        sm.RunAnimation(idleAnim, playerSpeed);
                    }
                    else
                    {
                        sm.RunAnimation(idleAnim, playerSpeed);
                    }
                    currentPlayerState = playerState.IDLE;
                }
            }
            else
            {
                Debug.Log("asdasd");
                switch (currentPlayerState)
                {
                    case playerState.IDLE:
                        sm.RunAnimation(idleAnim, playerSpeed);
                        break;
                    case playerState.ATTACK:
                        sm.RunAnimation(attackingAnim, playerSpeed);
                        break;
                    case playerState.DODGELEFT:
                        sm.RunAnimation(dodgeAnim, playerSpeed);
                        break;
                    case playerState.HURT:
                        sm.RunAnimation(hurtAnim, playerSpeed);
                        break;
                }
            }

            oldPlayerState = currentPlayerState;
        }
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
            DodgeLeft();
            currentPlayerState = playerState.DODGELEFT;
        }

        // Check for D key (dodge right)
        if (Input.GetKeyDown(KeyCode.D))
        {
            DodgeRight();

            currentPlayerState = playerState.DODGELEFT;
        }

        // Check for W key (attack)
        if (Input.GetKeyDown(KeyCode.W))
        {
            Attack();

            currentPlayerState = playerState.ATTACK;
        }

        //COMMENT THIS OUT LATER
        if (Input.GetKeyDown(KeyCode.S))
        {
            hurt();
        }
    }


    


    //WHAT HAPPENS WHEN PLAYER DODGES
    private void DodgeLeft()
    {
        if (currentAnimationState.IsName("player_idle")
         && !isAttacking)
        {
            animController.Rebind();

            isAttacking = false;
            animController.SetBool("attack", false);
            animController.SetBool("hurt", false);
            animController.SetFloat("dodge", 1f);

            //PLAY A RANDOMISED DODGE SOUND
            PlayRandomSound(dodgeSounds);

            Debug.Log("Dodge Left");
        }
    }

    private void DodgeRight()
    {
        if (currentAnimationState.IsName("player_idle")
         && !isAttacking)
        {
            animController.Rebind();

            isAttacking = false;
            animController.SetBool("attack", false);
            animController.SetBool("hurt", false);
            animController.SetFloat("dodge", 3f);

            //PLAY A RANDOMISED DODGE SOUND
            PlayRandomSound(dodgeSounds);

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
            animController.Rebind();

            animController.SetBool("attack", false);
            animController.SetFloat("dodge", 0);
            animController.SetBool("hurt", true);
            //RANDOMISE A HURT SOUND TO PLAY
            PlayRandomSound(hurtSounds);
            Debug.Log("Hurt");
        }
    }

    //WHAT HAPPENS WHEN PLAYER ATTACKS
    private void Attack()
    {

        if (currentAnimationState.IsName("player_idle")
         && !isAttacking)
        {
           animController.Rebind();

            animController.SetFloat("dodge", 0);
            animController.SetBool("hurt", false);
            animController.SetBool("attack", true);
            //RANDOMISE A PUNCH SOUND TO PLAY
            PlayRandomSound(swordSounds);
            Debug.Log("Attack");

            enemiesDefeated += 1;
            totalenemiesDefeated += 1;

            EnemyTracker();
            isAttacking = true; 
        }
    }
}

