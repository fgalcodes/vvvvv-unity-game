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
    private bool top = true;

    private Rigidbody2D rb;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;


    private int flips;

    private int extraJumps;
    [SerializeField] private int extraJumpsValue = 2;

    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();        

    }

    private void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        moveInput = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Debug.Log("Derecha: " + facingRight);
            Flip();
        }
        if (facingRight ==  true && moveInput < 0)
        {
            Debug.Log("Derecha: " + facingRight);
            Flip();
        }

    }

    // Update is called once per frame
    void Update()
    {
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
            Debug.Log("Space: " + Input.GetKeyDown(KeyCode.Space));
            rb.gravityScale *= -1;
            Rotation();
        }

    }

    void Flip()
    {
        facingRight = !facingRight;

        // Entender como funciona
        Vector3 Scaler = rb.transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
    private void Rotation()
    {
        if (!top)
        {
            transform.eulerAngles = new Vector3(0, 0, 180f);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        //facingRight = !facingRight;
        top = !top;
    }

    private void CheckFlips()
    {
        if (transform.rotation.z == 360)
        {
            Debug.Log("Flip!");
        }
    }

    private void CheckIsGrounded()
    {

    }
}
