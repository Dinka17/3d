using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum PlayerState
    {
        Standing,
        Jumping,
        Falling
    }

    private Rigidbody rigidBody;
    [SerializeField] private float movementSpeed = 6f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;

    [SerializeField] private AudioSource jumpSound;

    private PlayerState currentState = PlayerState.Standing;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case PlayerState.Standing:
                HandleStandingState();
                break;
            case PlayerState.Jumping:
                HandleJumpingState();
                break;
            case PlayerState.Falling:
                HandleFallingState();
                break;
            default:
                break;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            ChangeState(PlayerState.Jumping);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
            PlayerLife.Hp += 5;
        }
    }

    private void Jump()
    {
        Vector3 jumpVelocity = new Vector3(rigidBody.velocity.x, jumpForce, rigidBody.velocity.z);
        rigidBody.velocity = jumpVelocity;
        jumpSound.Play();
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }

    private void HandleStandingState()
    {
        Debug.Log("Standing");

        
        if (currentState == PlayerState.Standing)
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");

            rigidBody.velocity = new Vector3(horizontalInput * movementSpeed, rigidBody.velocity.y, verticalInput * movementSpeed);
        }
    }

    private void HandleJumpingState()
    {
        Debug.Log("Jumping");

       
        if (currentState == PlayerState.Jumping)
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");

            rigidBody.velocity = new Vector3(horizontalInput * movementSpeed, rigidBody.velocity.y, verticalInput * movementSpeed);
        }
    }

    private void HandleFallingState()
    {
        Debug.Log("Falling");

        
       

        if (IsGrounded())
        {
            ChangeState(PlayerState.Standing);
        }
    }


    private void ChangeState(PlayerState newState)
    {
        Debug.Log("Changing state from " + currentState + " to " + newState);
        currentState = newState;
    }
}
