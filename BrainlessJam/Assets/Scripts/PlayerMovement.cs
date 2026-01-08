using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float moveSpeedBase = 5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float rotateSpeed = 120f;
    [SerializeField] float runBoost = 5f;

    [Header("Particles")]
    [SerializeField] ParticleSystem walkParticles;

    //[Header("Misc/Debug")]
    //[SerializeField] InventoryManager inventoryManager;
    //[SerializeField] Ingredient ingredient;

    Animator animator;
    Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void Update()
    {
        //if (Keyboard.current.eKey.IsPressed())
        //{
            //inventoryManager.GetSelectedSlot()?.SetIngredient(ingredient);
        //}
        //if (Keyboard.current.pKey.IsPressed())
        //{
            //SceneManager.LoadScene("SpearHunting");
        //}
    }

    void FixedUpdate()
    {
        ProcessMovement();
    }

    void ProcessMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float moveSpeed = moveSpeedBase;

        // Animations
        if (animator != null)
        {
            bool isWalking = (Mathf.Abs(horizontal) > 0.3f || Mathf.Abs(vertical) > 0.3f) && !Keyboard.current.shiftKey.isPressed;
            animator.SetBool("IsWalking", isWalking);
        }

        // Running speed
        if (Keyboard.current.shiftKey.isPressed)
        {
            moveSpeed += runBoost;
        }
        animator.SetBool("IsRunning", Keyboard.current.shiftKey.isPressed);

        // ----- PLAYER-RELATIVE INPUT -----
        Vector3 inputDir = new Vector3(horizontal, 0f, vertical);
        if (inputDir.sqrMagnitude > 1f)
            inputDir.Normalize();

        // Convert inputDir from local (player) space to world space
        Vector3 relativeDir = transform.TransformDirection(inputDir);

        // Apply velocity
        Vector3 velocity = relativeDir * moveSpeed;
        velocity.y = rb.linearVelocity.y; // preserve gravity
        rb.linearVelocity = velocity;

        // ----- PARTICLES -----
        if (Mathf.Abs(horizontal) > 0.3f || Mathf.Abs(vertical) > 0.3f)
        {
            if (!walkParticles.isPlaying) walkParticles.Play();
        }
        else
        {
            walkParticles.Stop();
        }

        // ----- ROTATION (unchanged) -----
        transform.Rotate(0f, horizontal * rotateSpeed * Time.fixedDeltaTime, 0f);
    }
}
