using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovment : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 10f;

    public float moveInput;
    private bool _facingRight = true;
    private bool _facingUp;
    private bool _top = true;

    private Rigidbody2D _rb;

    private bool _isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;


    private int _extraJumps;
    [SerializeField] private int extraJumpsValue = 2;

    
    // Start is called before the first frame update
    void Start()
    {
        _extraJumps = extraJumpsValue;
        _rb = GetComponent<Rigidbody2D>();
        
    }

    private void FixedUpdate()
    {

        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        moveInput = Input.GetAxisRaw("Horizontal");

        _rb.velocity = new Vector2(moveInput * speed, _rb.velocity.y);

        if (_facingRight == false && moveInput > 0)
        {
            Debug.Log("Derecha: " + _facingRight);
            FlipHorizontally();
        }
        if (_facingRight && moveInput < 0)
        {
            Debug.Log("Derecha: " + _facingRight);
            FlipHorizontally();
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.eulerAngles.z.ToString());

        if (_isGrounded)
        {
            _extraJumps = extraJumpsValue;
            transform.rotation = Quaternion.identity; 
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _extraJumps > 0)
        {
            if (_top == false)
            {
                _rb.velocity = Vector2.up * jumpForce;
            }
                
            if (_top) _rb.velocity = Vector2.down * -jumpForce;   
            
            _extraJumps--;
        }
        else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _extraJumps == 0 && _isGrounded)
        {
            if (_top == false) _rb.velocity = Vector2.up * jumpForce;
            if (_top) _rb.velocity = Vector2.down * -jumpForce;
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            _rb.gravityScale *= -1;
            FlipVertically();
        }
        
    }

    void FlipHorizontally()
    {
        _facingRight = !_facingRight;
        Vector3 scaler = _rb.transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;

    }
    void FlipVertically()
    {
        _facingUp = !_facingUp;
        Vector3 scaler = _rb.transform.localScale;
        scaler.y *= -1;
        transform.localScale = scaler;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "NextLevel":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case "PreviousLevel":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
                break;
            case "Bullet":
                Destroy(gameObject);
                SceneManager.LoadScene(0);
                break;
        }
    }
}
