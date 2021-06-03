using UnityEngine;
using System.Collections;

public class Enemy_Control : MonoBehaviour
{
    [Header("Control / Контроль")]
    public float Speed = 2f;
    [SerializeField] private float _runSpeed = 7;

    [SerializeField] private float _pauseBtwAttacks = 2f;

    private bool _playerLive = true;

    [Tooltip("Где будет стоять персонаж")]
    [SerializeField] private Vector3 _staticPosition;
    [Space(height: 5f)]

    [Tooltip("Жизни")]
    public int MaxHealth = 100;
    private int _currentHealth;

    [SerializeField] private float _timeDamageAnim;

    [HideInInspector] public bool Static;
    [HideInInspector]public bool Die;
    [Space(height: 5f)]

    [Header("Attack / Атаки")]
    [SerializeField] private float _attackRange;
    [SerializeField] private float _startTimeBtwAttack;
    private float _timeBtwAttack;
    [Space(height: 5f)]

    [SerializeField] private int _attackDamage;

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _playerLayer;
    [Space(height: 5f)]

    [Header("Settings / Настойки")]
    [SerializeField] private bool _patrolWalk;
    [Space(height: 5f)]

    [SerializeField] private bool _attackShot;
    [Space(height: 5f)]

    private bool SYSTEM_PATROL_B;
    private bool SYSTEM_TAKEDAMAGE_B;
    private bool SYSTEM_ANGRY_B;
    private bool SYSTEM_ATTACK_B;
    private bool SYSTEM_SEARCH_B;
    [Space(height: 5f)]

    [Header("Distance / Дистанции")]
    [Tooltip("Дистанция перед персонажем")]
    [SerializeField] [Range(1.1f, 20f)] private float _distanceRetreat = 10f;
    [SerializeField] [Range(1.1f, 20f)] private float _targetRetreat = 10f;

    [SerializeField] private Transform _rayPoint;
    [Space(height: 5f)]

    [Header("Positions / Позиции")]
    [Tooltip("_staticPosition (по x) + перемещение")]
    [SerializeField] private float _leftPosition;
    [Tooltip("_staticPosition (по x) + -перемещение")]
    [SerializeField] private float _rightPosition;
    [Space(height: 5f)]

    [SerializeField] private bool _leftBorder = false;
    [SerializeField] private bool _rightBorder = false;

    [Header("Attacks / Атаки")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _projectile;
    [Space(height: 5f)]

    [Header("Audio")]
    //[SerializeField] private AudioSource _movementSource;
    [SerializeField] private AudioSource _runSource;
    [SerializeField] private AudioSource _attackSource;
    [SerializeField] private AudioSource _shotSource;
    [SerializeField] private AudioSource _takeDamageSource;
    [Space(height: 5f)]

    [Header("Etc / Другое")]
    [SerializeField] private bool _facingRight = true;
    private Animator _animr;
    private Transform _player;

    //=====================//
    //==Additional states==//
    //=====================//
    [Tooltip("Подтвержает, что только что был игрок")]
    private bool _1;
    [Tooltip("Подтвержает, что только что рабол SYSTEM_ATTACK")]
    private bool _2;
    [Tooltip("Подтвержает готовность атаковать")]
    private bool _3 = true;
    [Tooltip("Подтвержает, что только что персонаж патрулировал")]
    private bool _4 = true;



    private void Awake()
    {
        SYSTEM_BOOLS();

        _staticPosition = transform.position;
    }



    private void Start()
    {
        _animr = GetComponent<Animator>();

        _currentHealth = MaxHealth;

        _player = FindObjectOfType<Player_Control>().transform;
    }



    private void Update()
    {
        if (Die == false)
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        
            if (SYSTEM_PATROL_B == true) SYSTEM_PATROL();
            if (SYSTEM_ANGRY_B == true) SYSTEM_ANGRY();
            if (SYSTEM_ATTACK_B == true) SYSTEM_ATTACK();
            if (SYSTEM_SEARCH_B == true) SYSTEM_SEARCH();

            if (Vector2.Distance(transform.position, _player.position) > _distanceRetreat)
            {
                SYSTEM_PATROL_B = true;
                SYSTEM_ANGRY_B = false;
                SYSTEM_ATTACK_B = false;
                SYSTEM_SEARCH_B = false;

                _1 = true;
                _4 = false;
            }
            else if (Vector2.Distance(transform.position, _player.position) < _distanceRetreat &&
                    Vector2.Distance(transform.position, _player.position) > _targetRetreat
                    && _1 == true)
            {
                SYSTEM_PATROL_B = false;
                SYSTEM_ANGRY_B = true;
                SYSTEM_ATTACK_B = false;
                SYSTEM_SEARCH_B = false;
            }
            else if (Vector2.Distance(transform.position, _player.position) < _targetRetreat)
            {
                SYSTEM_PATROL_B = false;
                SYSTEM_ANGRY_B = false;
                SYSTEM_ATTACK_B = true;
                SYSTEM_SEARCH_B = false;

                _1 = false;
                _4 = true;
            }
            else if (Vector2.Distance(transform.position, _player.position) > _targetRetreat && _4 == true)
            {
                SYSTEM_PATROL_B = false;
                SYSTEM_ANGRY_B = false;
                SYSTEM_ATTACK_B = false;
                SYSTEM_SEARCH_B = true;
            }
        }

        if(Die == true)
        {
            //_movementSource.Stop();
            _runSource.Stop();
            _attackSource.Stop();
            _shotSource.Stop();
            _takeDamageSource.Stop();
        }
    }



    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth > 0) StartCoroutine(CoroutineTakeDamage());
    }



    public void PlayerLive_Off()
    {
        _playerLive = false;
    }



    private void SYSTEM_PATROL()
    {
        if (_patrolWalk == true) Patrol_Walk();
        else if (_patrolWalk == false) Patrol_Static();

        if (transform.position.x >= _rightPosition)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);

            _rightBorder = true;
            _leftBorder = false;
            _facingRight = false;
        }
        else if (transform.position.x <= _leftPosition)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);

            _rightBorder = false;
            _leftBorder = true;
            _facingRight = true;
        }
    }



    private void SYSTEM_ANGRY()
    {
        _animr.SetBool("Movement", true);

        Speed = _runSpeed;

        //_movementSource.Stop();
        _runSource.Play();
    }



    private void SYSTEM_ATTACK()
    {
        if(Die == false && _playerLive == true)
        {
            _1 = false;

            _animr.SetBool("Movement", false);
            //_movementSource.Stop();
            _runSource.Stop();

            SYSTEM_PATROL_B = false;

            Speed = 0;

            if (_attackShot == true && _3 == true)
            {
                _2 = true;
                StartCoroutine(CoroutineAttack_Shot());
            }
            else if (_attackShot == false && _3 == true)
            {
                _2 = true;
                Attack();
            }
        }
    }



    private void SYSTEM_SEARCH()
    {
        _animr.SetBool("Movement", true);

        Speed = _runSpeed;

        //_movementSource.Stop();
        _runSource.Play();
    }



    private void Patrol_Walk()
    {
        _animr.SetBool("Movement", true);

        //_movementSource.Play();
    }


    
    private void Patrol_Static()
    {
        transform.position = _staticPosition;
    }



    private void Attack()
    {
        if (_timeBtwAttack <= 0)
        {
            _animr.SetTrigger("Attack");

            _attackSource.Play();

            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _playerLayer);

            foreach (Collider2D player in hitPlayer)
            {
                player.GetComponent<Player_FightSystem>().TakeDamage(_attackDamage);
            }

            _timeBtwAttack = _startTimeBtwAttack;
        }
        else
        {
            _timeBtwAttack -= Time.deltaTime;
        }
    }



    public void Attack_Shot()
    {
        if (SYSTEM_TAKEDAMAGE_B == false)
        {
            _shotSource.Play();

            Instantiate(_projectile, _firePoint.position, _firePoint.rotation);
        }
    }



    public IEnumerator CoroutineTakeDamage()
    {
        SYSTEM_TAKEDAMAGE_B = true;
        SYSTEM_PATROL_B = false;
        Static = true;
        _patrolWalk = true;

        _animr.SetTrigger("TakeDamage");

        _takeDamageSource.Play();
        //_movementSource.Stop();
        _runSource.Stop();

        Speed = 0;

        yield return new WaitForSeconds(_timeDamageAnim);

        Static = false;

        SYSTEM_TAKEDAMAGE_B = false;
        SYSTEM_PATROL_B = true;

        Speed = 5;

        yield break;
    }



    public IEnumerator CoroutineAttack_Shot()
    {
        float attackCooldown = 2.5f;

        if (_2 == true)
        {
            _3 = false;

            _animr.SetTrigger("Shot");

            yield return new WaitForSeconds(attackCooldown);

            _3 = true;
        }
    }



    private void SYSTEM_BOOLS()
    {
        SYSTEM_PATROL_B = true;

        SYSTEM_TAKEDAMAGE_B = false;
        SYSTEM_ANGRY_B = false;
        SYSTEM_ATTACK_B = false;
        SYSTEM_SEARCH_B = false;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);

        if(_facingRight == true)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_rayPoint.position, _rayPoint.position + _rayPoint.localScale.x * Vector3.right * _distanceRetreat);
        }
        else if (_facingRight == false)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_rayPoint.position, _rayPoint.position + _rayPoint.localScale.x * Vector3.left * _distanceRetreat);
        }
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//