using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
   private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float vertical;
    private float horizontal;
    private float rotationSpeed = 100f;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private bool hasJumped = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
       

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = transform.forward * vertical;
        controller.Move(move * Time.deltaTime * playerSpeed);

       transform.Rotate(Vector3.up * horizontal * rotationSpeed * Time.deltaTime);

        // Changes the height position of the player..
        if (hasJumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
            groundedPlayer = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
            groundedPlayer = false;
    }

    /// <summary>
    /// Gets input from user.
    /// </summary>
    /// <param name="context"></param>

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        hasJumped = context.ReadValueAsButton();
    } 
}
