using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animController;
    private SpriteRenderer spriteRenderer;
    public AudioClip[] punchingSounds;

    private bool isAttacking = false; // Flag to prevent multiple attacks

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animController = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Check for mobile controls (swipes)
        CheckMobileInput();

        // Check for PC controls
        CheckPCInput();

        animationCheck();
    }

    void PlayPunchingSound()
    {
        int idx = Random.Range(0, punchingSounds.Length);
        Debug.Log("Punching Sound Played " + idx);
        // Play the sound at the specified index
        AudioSource.PlayClipAtPoint(punchingSounds[idx], transform.position);
    }

    private void animationCheck()
    {
        // Get information about the current animation state in layer 0
        AnimatorStateInfo currentAnimationState = animController.GetCurrentAnimatorStateInfo(0);
        // Check if the current animation has finished
        if (animController.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
            && !currentAnimationState.IsName("boxer_idle"))
        {
            if (spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
            }
            animController.SetBool("attack", false);
            animController.SetBool("dodge", false);
            isAttacking = false; // Reset attack flag
        }
    }

    private void CheckMobileInput()
    {
        // Check for swipe
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 deltaPosition = touch.deltaPosition;
                // SWIPE LEFT
                if (deltaPosition.x < 0)
                {
                    DodgeLeft();
                }
                // SWIPE RIGHT
                else if (deltaPosition.x > 0)
                {
                    DodgeRight();
                }
                // SWIPE UP (with a sensitivity threshold)
                else if (deltaPosition.y > deltaPosition.x && deltaPosition.y > Screen.height * 0.05f)
                {
                    Attack();
                }
            }
        }
    }

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

    private void DodgeLeft()
    {
        if (!isAttacking) // Only allow dodging if not attacking
        {
            animController.SetBool("attack", false);
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
            animController.SetBool("dodge", true);
            spriteRenderer.flipX = true;
            // Implement dodge right logic here
            Debug.Log("Dodge Right");
        }
    }

    private void Attack()
    {
        if (!isAttacking) // Only allow attacking if not already attacking
        {
            animController.SetBool("dodge", false);
            animController.SetBool("attack", true);
            PlayPunchingSound();
            // Implement attack logic here
            Debug.Log("Attack");
            isAttacking = true; // Set attack flag
        }
    }
}
