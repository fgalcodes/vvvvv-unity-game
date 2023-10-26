using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private AudioClip[] soundFx = new AudioClip[4];
    
    private int _specialFlipAttack = 1;

    public float moveInput;
    private bool _facingRight = true;
    private bool _facingUp;
    private bool _top = true;


    private Rigidbody2D _rb;
    private Animator _anim;
    private GameObject[] _enemies;

    public static bool IsGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int _currentLevel;

    private int _extraJumps;
    private int _extraJumpsValue = 2;


    // Counter de flips
    public static bool IsFlipping;
    public static int CounterFlips;

    // Animation States
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int SetDeath = Animator.StringToHash("setDeath");
    private static readonly int SetDeathAir = Animator.StringToHash("setDeathAir");


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnManager.Instance.SpawnPlayer("Spawn", 0);

        _extraJumps = _extraJumpsValue;
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void FixedUpdate()
    {

        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        moveInput = Input.GetAxisRaw("Horizontal");

        _rb.velocity = new Vector2(moveInput * speed, _rb.velocity.y);

        _anim.SetBool(IsRunning, moveInput != 0f);
        _anim.SetBool(IsJumping, _rb.velocity.y > 1f);

        if (!_facingRight && moveInput > 0)
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
        if (IsGrounded)
        {
            _extraJumps = _extraJumpsValue;
            transform.rotation = Quaternion.identity;
            CounterFlips = 0;
            IsFlipping = false;
        }
        else
        {
            IsFlipping = true;
            FlipCounter();
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _extraJumps > 0)
        {
            // _anim.SetBool("isJumping", true);

            if (_top == false)
            {
                _rb.velocity = Vector2.up * jumpForce;
            }

            if (_top) _rb.velocity = Vector2.down * -jumpForce;

            _extraJumps--;
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _extraJumps == 0 && IsGrounded)
        {
            // _anim.SetBool("isJumping", true);

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
                SoundManager.Instance.PlaySound(soundFx[0]);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case "PreviousLevel":
                SoundManager.Instance.PlaySound(soundFx[1]);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                break;
            case "Bullet":
            case "Enemy":
                SoundManager.Instance.PlaySound(soundFx[2]);
                
                _currentLevel = SceneManager.GetActiveScene().buildIndex;
                StaticData.CurrentLevel = _currentLevel;

                if (IsGrounded)
                {
                    _rb.bodyType = RigidbodyType2D.Static;
                    _anim.SetTrigger(SetDeath);

                }
                else
                {
                    _rb.bodyType = RigidbodyType2D.Static;
                    _anim.SetTrigger(SetDeathAir);
                }
                // LoadGameOver();
                break;
        }
    }

    private void LoadGameOver()
    {
        // Destroy(gameObject);
        StaticData.GameOver = true;
        SoundManager.Instance.StopBGM();
        SceneManager.LoadScene(0);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void FlipCounter()
    {
        // Flips
        float currentRotation = transform.rotation.z;
        if (currentRotation < 0) currentRotation *= -1;

        //Debug.Log(currentRotation);
        if (currentRotation >= 0.9)
        {
            SoundManager.Instance.PlaySound(soundFx[3]);
            
            var rotation = transform.rotation;
            rotation = Quaternion.Euler(new Vector3(rotation.x, rotation.y, 0f));
            transform.rotation = rotation;
            CounterFlips++;

            if (CounterFlips > _specialFlipAttack)
            {
                // Debug.Log("Kill!");
                foreach (GameObject enemy in _enemies)
                {
                    if (enemy == null)
                    {
                        continue;
                    }
                    else
                    {
                        if (enemy.GetComponent<Renderer>().isVisible)
                        {
                            Destroy(enemy);
                        }
                    }

                    Debug.Log(enemy.GetComponent<Renderer>().isVisible);
                }
            }

            // Debug.Log(CounterFlips);
        }
    }
}
