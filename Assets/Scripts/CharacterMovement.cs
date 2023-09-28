using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement vector
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        // Normalize the vector to avoid diagonal movement being faster
        movement.Normalize();

        // Move the character
        rb.velocity = movement * moveSpeed;

        // Optionally, you can flip the character sprite based on movement direction
        FlipSprite(horizontalInput);
    }

    // Flip the character sprite when changing direction
    void FlipSprite(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            // Face right
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (horizontalInput < 0)
        {
            // Face left
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
