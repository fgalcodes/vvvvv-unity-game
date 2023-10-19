using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
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

    private int _currentLevel;

    private int _extraJumps;
    [SerializeField] private int extraJumpsValue = 2;


    // Contador de flips
    public static bool IsFlipping;
    public static int ContadorFlips;

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
            FlipHorizontally();
        }
        if (_facingRight && moveInput < 0)
        {
            FlipHorizontally();
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGrounded)
        {
            _extraJumps = extraJumpsValue;
            transform.rotation = Quaternion.identity;
            ContadorFlips = 0;
            IsFlipping = false;
        } else
        {
            IsFlipping = true;
            FlipCounter();
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

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _extraJumps == 0 && _isGrounded)
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
                _currentLevel = SceneManager.GetActiveScene().buildIndex;
                StaticData.CurrentLevel = _currentLevel;
                LoadGameOver();
                break;
        }
    }

    void LoadGameOver()
    {
        Destroy(gameObject);
        StaticData.GameOver = true;
        SceneManager.LoadScene(0);
    }

    public void FlipCounter()
    {
        // Flips
        float currentRotation = transform.rotation.z;
        if (currentRotation < 0) currentRotation *= -1;
        //Debug.Log(currentRotation);
        if (currentRotation >= 0.9)
        {
            var rotation = transform.rotation;
            rotation = Quaternion.Euler(new Vector3(rotation.x, rotation.y, 0f));
            transform.rotation = rotation;
            ContadorFlips++;

            Debug.Log(ContadorFlips);
        }
    }
}
