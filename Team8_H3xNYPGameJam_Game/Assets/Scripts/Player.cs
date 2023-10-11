using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animController;
    private SpriteRenderer spriteRenderer;
    public AudioClip[] swordSounds;
    public AudioClip[] hurtSounds;

    private AudioSource AS;
    private bool isAttacking = false; // Flag to prevent multiple attacks


    public float playerStrength;
    public TextMeshProUGUI playerStrengthText;
    public float playerWeight;
    public TextMeshProUGUI playerWeightText;
    float speed = 2;

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
        sm=GetComponent<SpriteMan>();
        rb = GetComponent<Rigidbody2D>();
        animController = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    public void Animation()
    {

    }

    //WHEN ANIMATION OTHER THAN IDLE HAS STOPPED PLAYING
    private void animationCheck()
    {
        AnimatorStateInfo currentAnimationState = animController.GetCurrentAnimatorStateInfo(0);

        if (animController.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
            && !currentAnimationState.IsName("player_idle"))
        {
            if (spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
            }
            animController.SetBool("attack", false);
            animController.SetBool("dodge", false);
            animController.SetBool("hurt", false);

            currentPlayerState = playerState.IDLE;

            isAttacking = false; // Reset attack flag
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
                        sm.RunAnimation(idleAnim, speed);
                    }
                    else
                    {
                        sm.RunAnimation(idleAnim, speed);
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
                        sm.RunAnimation(idleAnim, speed);
                        break;
                    case playerState.ATTACK:
                        sm.RunAnimation(attackingAnim, speed);
                        break;
                    case playerState.DODGE:
                        sm.RunAnimation(dodgeAnim, speed);
                        break;
                    case playerState.HURT:
                        sm.RunAnimation(hurtAnim, speed);
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
            currentPlayerState = playerState.DODGE;
        }

        // Check for D key (dodge right)
        if (Input.GetKeyDown(KeyCode.D))
        {
            DodgeRight();

            currentPlayerState = playerState.DODGE;
        }

        // Check for W key (attack)
        if (Input.GetKeyDown(KeyCode.W))
        {
            Attack();

            currentPlayerState = playerState.ATTACK;
        }
    }


    //WHAT HAPPENS WHEN PLAYER DODGES
    private void DodgeLeft()
    {
        animController.Rebind();

        if (spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }

        isAttacking = false;
        animController.SetBool("attack", false);
        animController.SetBool("hurt", false);
        animController.SetBool("dodge", true);
        Debug.Log("Dodge Left");
    }

    private void DodgeRight()
    {
        animController.Rebind();

        spriteRenderer.flipX = true;

        isAttacking = false;
        animController.SetBool("attack", false);
        animController.SetBool("hurt", false);
        // Set "dodge" to true to start the animation
        animController.SetBool("dodge", true);
        Debug.Log("Dodge Right");
    }

    //

    //WHAT HAPPENS WHEN PLAYER IS HURT
    private void hurt()
    {
        if (!isAttacking)
        {
            animController.Rebind();

            animController.SetBool("attack", false);
            animController.SetBool("dodge", false);
            animController.SetBool("hurt", true);
            //RANDOMISE A HURT SOUND TO PLAY
            PlayRandomSound(hurtSounds);
            Debug.Log("Hurt");
        }
    }


    //WHAT HAPPENS WHEN PLAYER ATTACKS
    private void Attack()
    {
        if (!isAttacking) // Only allow attacking if not already attacking
        {
           animController.Rebind();

            animController.SetBool("dodge", false);
            animController.SetBool("hurt", false);
            animController.SetBool("attack", true);
            //RANDOMISE A PUNCH SOUND TO PLAY
            PlayRandomSound(swordSounds);
            Debug.Log("Attack");

            //enemiesDefeated += 1;
            totalenemiesDefeated += 1;

            EnemyTracker();
            isAttacking = true; 
        }
    }
}

enum playerState
{
    IDLE,
    DODGE,
    ATTACK,
    HURT
}