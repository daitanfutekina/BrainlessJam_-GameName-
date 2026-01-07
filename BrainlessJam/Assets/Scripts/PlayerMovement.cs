using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float moveSpeedBase = 5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float rotateSpeed = 120f;
    [SerializeField] float runBoost = 5f;

    [Header("Particles")]
    [SerializeField] ParticleSystem walkParticles;

    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ProcessMovement();
    }

    void ProcessMovement()
    {
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");
        float moveSpeed = moveSpeedBase; 

        
        if (animator != null)
        {
            animator.SetBool("IsWalking", (vertical > 0.3 || vertical < -0.3 || 
            horizontal > 0.3 || horizontal < -0.3) && !Keyboard.current.shiftKey.isPressed);
        }
        if (Keyboard.current.shiftKey.isPressed)
        {
            moveSpeed += runBoost;
        }
        else
        {
            moveSpeed = moveSpeedBase;
        }
        animator.SetBool("IsRunning", Keyboard.current.shiftKey.isPressed);
        
        transform.Translate(0f, 0f, vertical * moveSpeed * Time.deltaTime);
        if (vertical > 0.3 || vertical < -0.3 || horizontal > 0.3 || horizontal < -0.3)
        {
            if (!walkParticles.isPlaying)
            {
                walkParticles.Play();
            }
        }
        else
        {
            walkParticles.Stop();
        }

        
        transform.Rotate(0f, horizontal * rotateSpeed * Time.deltaTime, 0f);
    }

}
