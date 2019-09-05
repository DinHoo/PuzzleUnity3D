using UnityEngine;

public class ThirdPersonCharacterControl : MonoBehaviour
{
    public float speed = 6.0F;
    public float gravity = -50.0F;
    public float jumpHigh = 5.0F;
    public float velocityY;


    private Vector3 moveDirection = Vector3.zero;
    public CharacterController controller;

    private void Start()
    {
        // Store reference to attached component
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movimento();
    }

    private void Movimento()
    {
        // Character is on ground (built-in functionality of Character Controller)

        // Use input up and down for direction, multiplied by speed
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection *= speed*2;
        }
        else
        {
            moveDirection *= speed;

        }

        if(Input.GetButton("Jump") && controller.isGrounded)
        {
            velocityY = Mathf.Sqrt(-2 * gravity * jumpHigh); ;
            print("pulo");
        }

        

        // Apply gravity manually.
        velocityY += gravity * Time.deltaTime;
        // Move Character Controller
        controller.Move((moveDirection + Vector3.up*velocityY) * Time.deltaTime);

        if (controller.isGrounded)
        {
            velocityY = 0;
        }
    }
}