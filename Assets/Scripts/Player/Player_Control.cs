using UnityEngine;
using System.Collections;

public class Player_Control : MonoBehaviour
{
    [Header("Control")]
    public float Speed;
    public float JumpForce;
    private float _directionMovement; //направление перемещения
    [Space(height: 5f)]

    [SerializeField] private bool _facingRight;
    private bool _isJump;
    private bool _isWall;
    private bool _isGround;
    private bool _inAir;
    private bool _die;
    [Space(height: 5f)]

    [Header("Ground")]
    [SerializeField] private float _checkRadius = 0.2f;

    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Transform _groundCheck;

    [Header("Die")]
    [SerializeField] private GameObject _dieWindow;
    [SerializeField] private GameObject _endGameWindow;

    [SerializeField] private string _teleportation;
    [Space(height: 5f)]

    [HideInInspector] public bool Static = false;
    [Space(height: 5f)]

    [Header("Audio")]
    //[SerializeField] private AudioSource _movementSource;
    [SerializeField] private AudioSource _jumpSource;

    private Rigidbody2D _rigidbody2D;
    private Animator _animr;
    private Player_FightSystem _player_FightSystem;

    private const float _speedMultiplier = 50f;
    private const float _jumpMultiplier = 100f;



    private void Start()
    {
        Time.timeScale = 1f;
        _dieWindow.SetActive(false);
        _endGameWindow.SetActive(false);

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animr = GetComponent<Animator>();
        _player_FightSystem = GetComponent<Player_FightSystem>();
    }



    private void Update()
    {
        Movement();
    }



    private void FixedUpdate()
    {
        _isGround = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _whatIsGround);

        _rigidbody2D.velocity = new Vector2(_directionMovement * Speed * _speedMultiplier * Time.fixedDeltaTime, _rigidbody2D.velocity.y);

        if (_isJump == true)
        {
            _rigidbody2D.AddForce(new Vector2(_rigidbody2D.velocity.x, JumpForce * _jumpMultiplier));
            _isGround = false;
            _isJump = false;
        }
    }



    private void Movement()
    {
        if(_isGround == false)
        {
            _inAir = true;
        }
        else if (_isGround == true)
        {
            _inAir = false;
        }

        //==Walls==//
        if (_isWall == true || (Static == true && _die == false))
        {
            _animr.SetBool("isWall", true);
        }
        else if (_isWall == false)
        {
            _animr.SetBool("isWall", false);
        }

        //==Left & Right==//
        if (Static == false)
        {
            _directionMovement = Input.GetAxis("Horizontal");

            _rigidbody2D.velocity = new Vector2(_directionMovement * Speed, _rigidbody2D.velocity.y);
        
            if (Input.GetButton("Horizontal"))
            {
                _animr.SetBool("isRun", true);
            }
            else if (Input.GetButtonUp("Horizontal"))
            {              
                _animr.SetBool("isRun", false);
            }

            if (_facingRight == false && _directionMovement > 0)
            {
                Flip();
            }
            else if (_facingRight == true && _directionMovement < 0)
            {
                Flip();
            }

            Player_MovementAudioEffects();
        }

        //==Jump & inAir==//
        if (Input.GetButtonDown("Jump") && _isGround == true)
        {
            if(Static == false)
            {
                _isJump = true;
                _inAir = true;

                _jumpSource.Play();
            }
        }

        if (_inAir == true)
        {
            _animr.SetBool("isJump", true);
        }
        else if (_inAir == false)
        {
            _animr.SetBool("isJump", false);
        }
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        const string tagFinish = "Finish";

        if (col.gameObject.TryGetComponent(out Die die))
        {
            Die();

            _player_FightSystem.TakeDamage(100);
        }

        if (col.gameObject.TryGetComponent(out Traps traps))
        {
            _player_FightSystem.TakeDamage(40);
        }

        if (col.CompareTag(tagFinish))
        {
            EndMission();
        }
    }



    private void OnTriggerStay2D(Collider2D col)
    {
        
    }



    private void OnTriggerExit2D(Collider2D col)
    {

    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Wall wall))
        {
            _isWall = true;
        }
    }



    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Wall wall))
        {
            _isWall = false;
        }
    }



    private void Flip()
    {
        if (Static == false)
        {
            _facingRight = !_facingRight;
            transform.Rotate(0, 180, 0);
        }
    }



    private void Die()
    {
        FindObjectOfType<GameManager_Pause>().ConditionPause_On();

        JumpForce = 0;

        _dieWindow.SetActive(true);
    }



    private void EndMission()
    {
        FindObjectOfType<GameManager_Pause>().ConditionPause_On();

        //SceneManager.LoadScene(_teleportation);
        _endGameWindow.SetActive(true);
    }



    private void Player_MovementAudioEffects()
    {
        if (Input.GetButtonDown("Horizontal") && 
            (_isGround == true && Static == false && _die == false))
        {
            //_movementSource.Play();
        }
        else if (Input.GetButtonUp("Horizontal") ||
            (_isGround == false || Static == true || _die == true))
        {
            //_movementSource.Stop();
        }
    }



    public IEnumerator Player_Die()
    {
        float timeAnimDie = 2f;

        _die = true;
        Static = true;

        FindObjectOfType<Enemy_Control>().PlayerLive_Off();

        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        _animr.SetTrigger("Die");

        JumpForce = 0;

        yield return new WaitForSeconds(timeAnimDie);

        _dieWindow.SetActive(true);

        FindObjectOfType<GameManager_Pause>().ConditionPause_On();

        yield break;
    }



    public void PlayerFreeze_On()
    {
        Static = true;

        _animr.SetBool("isRun", false);

        FindObjectOfType<GameManager_Pause>().ConditionPause_On();

        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }



    public void PlayerFreeze_Off()
    {
        Static = false;

        FindObjectOfType<GameManager_Pause>().ConditionPause_Off();

        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(_groundCheck.position, _checkRadius);
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//