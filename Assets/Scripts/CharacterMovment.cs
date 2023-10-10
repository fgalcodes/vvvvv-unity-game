using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovment : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 10f;

    public float moveInput;
    private bool facingRight = true;
    private bool facingUp = false;
    private bool top = true;

    private Rigidbody2D rb;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;


    private int extraJumps;
    [SerializeField] private int extraJumpsValue = 2;


    // trying Flip detection

    private Quaternion initialRotation;
    private bool hasFlipped = false;


    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();

        initialRotation = rb.transform.rotation;

    }

    private void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        moveInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Debug.Log("Derecha: " + facingRight);
            FlipHorizontally();
        }
        if (facingRight ==  true && moveInput < 0)
        {
            Debug.Log("Derecha: " + facingRight);
            FlipHorizontally();
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.eulerAngles.z.ToString());

        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
            transform.rotation = Quaternion.identity; 
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && extraJumps > 0)
        {
            if (top == false)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
                
            if (top == true) rb.velocity = Vector2.down * -jumpForce;   
            
            extraJumps--;
        }
        else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && extraJumps == 0 && isGrounded == true)
        {
            if (top == false) rb.velocity = Vector2.up * jumpForce;
            if (top == true) rb.velocity = Vector2.down * -jumpForce;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.gravityScale *= -1;
            FlipVertically();
        }

        // Check if the player's object has completed a full 360-degree rotation around the z-axis
        if (transform.eulerAngles.z <= -360 || transform.eulerAngles.z >= 360)
        {
            Debug.Log("Player has completed a 360-degree flip!");
        }
        //    if (!hasFlipped)
        //    {
        //        // The player has completed a 360-degree flip, run your script here
        //        Debug.Log("Player has completed a 360-degree flip!");

        //        // You can add your script logic here

        //        // Set hasFlipped to true to prevent the script from running repeatedly
        //        hasFlipped = true;
        //    }
        //}
        //else
        //{
        //    // Reset hasFlipped if the rotation is not greater than or equal to 360 degrees
        //    hasFlipped = false;
        //}
    }

    void FlipHorizontally()
    {
        facingRight = !facingRight;
        Vector3 Scaler = rb.transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
    void FlipVertically()
    {
        facingUp = !facingUp;
        Vector3 Scaler = rb.transform.localScale;
        Scaler.y *= -1;
        transform.localScale = Scaler;

    }
}
