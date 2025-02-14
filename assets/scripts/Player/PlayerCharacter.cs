using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerCharacter : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float floatDuration = 0.5f;
    
    [Header("Visual Effects")]
    public ParticleSystem runDustEffect;
    public ParticleSystem jumpSparkleEffect;
    public ParticleSystem celebrationEffect;
    public TrailRenderer magicTrail;
    
    [Header("Audio")]
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip celebrationSound;
    public AudioSource audioSource;
    
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isFloating;
    
    // Animation parameter names
    private readonly string IS_RUNNING = "IsRunning";
    private readonly string IS_JUMPING = "IsJumping";
    private readonly string CELEBRATE = "Celebrate";

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
        // Initialize trail effect
        if (magicTrail != null)
        {
            magicTrail.enabled = false;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        
        // Apply movement
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        
        // Update visuals
        if (moveInput != 0)
        {
            // Flip sprite based on direction
            spriteRenderer.flipX = moveInput < 0;
            
            // Play run animation
            animator.SetBool(IS_RUNNING, true);
            
            // Play dust effect when moving on ground
            if (isGrounded && !runDustEffect.isPlaying)
            {
                runDustEffect.Play();
            }
            
            // Enable magic trail
            if (magicTrail != null)
            {
                magicTrail.enabled = true;
            }
        }
        else
        {
            animator.SetBool(IS_RUNNING, false);
            runDustEffect.Stop();
            
            if (magicTrail != null)
            {
                magicTrail.enabled = false;
            }
        }
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && !isFloating)
        {
            // Apply jump force
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
            // Play effects
            jumpSparkleEffect.Play();
            audioSource.PlayOneShot(jumpSound);
            
            // Update animation
            animator.SetBool(IS_JUMPING, true);
            isGrounded = false;
            
            // Start float sequence
            StartCoroutine(FloatSequence());
        }
    }

    IEnumerator FloatSequence()
    {
        isFloating = true;
        
        // Apply floating effect
        float originalGravity = rb.gravityScale;
        rb.gravityScale = originalGravity * 0.5f;
        
        yield return new WaitForSeconds(floatDuration);
        
        rb.gravityScale = originalGravity;
        isFloating = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool(IS_JUMPING, false);
            
            // Play landing effects
            audioSource.PlayOneShot(landSound);
            runDustEffect.Play();
        }
    }

    public void Celebrate()
    {
        animator.SetTrigger(CELEBRATE);
        celebrationEffect.Play();
        audioSource.PlayOneShot(celebrationSound);
    }

    // Called by GameFlowManager when answering correctly
    public void OnCorrectAnswer()
    {
        Celebrate();
    }

    // Called by GameFlowManager when completing a level
    public void OnLevelComplete()
    {
        StartCoroutine(LevelCompleteSequence());
    }

    private IEnumerator LevelCompleteSequence()
    {
        // Disable normal movement
        this.enabled = false;
        
        // Play celebration animation and effects
        Celebrate();
        
        // Float upward with sparkles
        rb.velocity = Vector2.up * jumpForce * 0.5f;
        jumpSparkleEffect.Play();
        
        yield return new WaitForSeconds(2f);
        
        // Re-enable movement
        this.enabled = true;
    }
}
