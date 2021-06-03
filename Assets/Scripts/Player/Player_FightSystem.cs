using System;
using UnityEngine;

public class Player_FightSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;

    [Header("Attack")]
    [SerializeField] private float _startTimeBtwAttack;
    private float _timeBtwAttack;
    [SerializeField] private float _attackRange;
    [Space(height: 5f)]

    [SerializeField] private int _damage;
    [Space(height: 5f)]

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemy;
    [Space(height: 5f)]

    [Header("Shot")]
    [SerializeField] private float _startTimeBtwShot;
    private float _timeBtwShot;
    [Space(height: 5f)]

    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulled;
    [Space(height: 5f)]

    [Header("Health")]
    public int Health;

    private bool IsAlive => Health > 0;
    [Space(height: 5f)]

    [Header("Audio")]
    [SerializeField] private AudioSource _attackSource;
    [SerializeField] private AudioSource _shotSource;
    [Space(height: 5f)]

    [SerializeField] private AudioSource _takeDamageSource;
    [SerializeField] private AudioSource _dieSource;

    private Player_Control _player_Control;
    private Animator _animr;



    private void Start()
    {
        _player_Control = GetComponent<Player_Control>();
        _animr = GetComponent<Animator>();
    }



    private void Update()
    {
        if (IsAlive)
        {

            //==Attack==//
            if (_timeBtwAttack <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _animr.SetTrigger("Attack");

                    _timeBtwAttack = _startTimeBtwAttack;
                }
            }
            else if (_timeBtwAttack > 0)
            {
                _timeBtwAttack -= Time.deltaTime;
            }

            //==Shoting==//
            if (_timeBtwShot <= 0)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    _animr.SetTrigger("Shot");

                    _timeBtwShot = _startTimeBtwShot;
                }
            }
            else if (_timeBtwShot > 0)
            {
                _timeBtwShot -= Time.deltaTime;
            }
        }
    }



    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health > 0)
        {
            _animr.SetTrigger("TakeDamage");

            _takeDamageSource.Play();
        }

        if (Health <= 0)
        {
            StartCoroutine(_player_Control.Player_Die());

            _dieSource.Play();

            foreach (GameObject enemies in _enemies)
            {
                enemies.GetComponent<Enemy_Control>().PlayerLive_Off();
            }
        }

        FindObjectOfType<HealthBar>().HealthBar_Update();
    }



    public void Attacking()
    {
        _attackSource.Play();

        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy_FightSystem>().TakeDamage(_damage);
        }
    }



    private void Shoting()
    {
        _shotSource.Play();

        Instantiate(_bulled, _firePoint.position, _firePoint.transform.rotation);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//