using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    public float jumpButtonGracePeriod;

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("IsJumping", true);
            jumpButtonPressedTime = Time.time;
        }
        else
            animator.SetBool("IsJumping", false);

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection == Vector3.forward)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
        if (movementDirection == Vector3.forward && Input.GetButton("Sprint"))
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        if (movementDirection == Vector3.left)
        {
            animator.SetBool("StrafeL", true);
        }
        else
        {
            animator.SetBool("StrafeL", false);
        }
        if (movementDirection == Vector3.right)
        {
            animator.SetBool("StrafeR", true);
        }
        else
        {
            animator.SetBool("StrafeR", false);
        }
        if (movementDirection == Vector3.back)
        {
            animator.SetBool("IsWalkingBack", true);
        }
        else
        {
            animator.SetBool("IsWalkingBack", false);
        }
        if (Input.GetButton("Impact"))
        {
            animator.SetBool("IsImpact", true);
        }
        else
        {
            animator.SetBool("IsImpact", false);
        }
        if (Input.GetButton("Fire1"))
        {
            animator.SetBool("Attack1", true);
        }
        else
        {
            animator.SetBool("Attack1", false);
        }
        if (Input.GetButton("Fire2"))
        {
            animator.SetBool("Attack2", true);
        }
        else
        {
            animator.SetBool("Attack2", false);
        }
    }
}