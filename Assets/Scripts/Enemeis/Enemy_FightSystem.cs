using UnityEngine;
using System.Collections;

public class Enemy_FightSystem : MonoBehaviour
{
    [Header("Control")]
    [SerializeField] private float _time;
    [Space(height: 5f)]

    [Header("Health")]
    [SerializeField] private int _health;
    [Space(height: 5f)]

    [Header("Audio")]
    [SerializeField] private AudioSource _dieSource;

    private Enemy_Control _enemy_Control;
    private Animator _animr;



    private void Start()
    {
        _enemy_Control = GetComponent<Enemy_Control>();
        _animr = GetComponent<Animator>();
    }



    public void TakeDamage(int damage)
    {
        _health -= damage;

        if(_health > 0)
        {
            StartCoroutine(_enemy_Control.CoroutineTakeDamage());
        }

        if (_health <= 0)
        {
            Die();
            _enemy_Control.Die = true;
        }
    }



    private void Die()
    {
        float timeDestroy = 5;

        _animr.SetTrigger("Die");

        _dieSource.Play();

        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;

        Destroy(gameObject, timeDestroy);
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//