using UnityEngine;

public class Bulled_Player : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float _speed;
    [Space(height: 5f)]

    [SerializeField] private int _damage;
    


    
    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    

    private void Update()
    {
        transform.Translate(Vector2.left * -_speed * Time.deltaTime);
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent(out Enemy_Control enemy))
        {
            enemy.GetComponent<Enemy_FightSystem>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//