using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float rotateSpeed = 120f;

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

        
        if (animator != null)
        {
            animator.SetBool("IsWalking", vertical > 0.3 || vertical < -0.3 || 
            horizontal > 0.3 || horizontal < -0.3);
        }

        
        transform.Translate(0f, 0f, vertical * moveSpeed * Time.deltaTime);

        
        transform.Rotate(0f, horizontal * rotateSpeed * Time.deltaTime, 0f);
    }

}
