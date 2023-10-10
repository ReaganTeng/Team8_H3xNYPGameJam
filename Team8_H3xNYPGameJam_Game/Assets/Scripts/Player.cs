using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animController;
    private SpriteRenderer spriteRenderer;
    public AudioClip[] punchingSounds;
    public AudioClip[] hurtSounds;

    private AudioSource AS;
    private bool isAttacking = false; // Flag to prevent multiple attacks

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animController = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        AS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckMobileInput();

        CheckPCInput();

        animationCheck();
    }

    //RANDOMISE A SOUND TO PLAY
    void PlayRandomSound(AudioClip[] soundList)
    {
        int idx = Random.Range(0, soundList.Length);
        Debug.Log("Punching Sound Played " + idx);
        // Play the sound at the specified index
        AS.clip = soundList[idx];
        AS.Play();
    }
    //


    //WHEN ANIMATION OTHER THAN IDLE HAS STOPPED PLAYING
    private void animationCheck()
    {
        AnimatorStateInfo currentAnimationState = animController.GetCurrentAnimatorStateInfo(0);

        if (animController.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
            && !currentAnimationState.IsName("boxer_idle"))
        {
            if (spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
            }
            animController.SetBool("attack", false);
            animController.SetBool("dodge", false);
            animController.SetBool("hurt", false);
            isAttacking = false; // Reset attack flag
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


    //WHAT HAPPENS WHEN PLAYER DODGES
    private void DodgeLeft()
    {
        if (!isAttacking) // Only allow dodging if not attacking
        {
            animController.SetBool("attack", false);
            animController.SetBool("hurt", false);
            animController.SetBool("dodge", true);
            // Implement dodge left logic here
            Debug.Log("Dodge Left");
        }
    }

    private void DodgeRight()
    {
        if (!isAttacking) // Only allow dodging if not attacking
        {
            animController.SetBool("attack", false);
            animController.SetBool("hurt", false);
            animController.SetBool("dodge", true);
            spriteRenderer.flipX = true;
            // Implement dodge right logic here
            Debug.Log("Dodge Right");
        }
    }
    //

    //WHAT HAPPENS WHEN PLAYER IS HURT
    private void hurt()
    {
        animController.SetBool("attack", false);
        animController.SetBool("dodge", false);
        animController.SetBool("hurt", true);
        //RANDOMISE A HURT SOUND TO PLAY
        PlayRandomSound(hurtSounds);
        Debug.Log("Hurt");
    }


    //WHAT HAPPENS WHEN PLAYER ATTACKS
    private void Attack()
    {
        if (!isAttacking) // Only allow attacking if not already attacking
        {
            animController.SetBool("dodge", false);
            animController.SetBool("hurt", false);
            animController.SetBool("attack", true);
            //RANDOMISE A PUNCH SOUND TO PLAY
            PlayRandomSound(punchingSounds);
            Debug.Log("Attack");
            isAttacking = true; 
        }
    }
}
