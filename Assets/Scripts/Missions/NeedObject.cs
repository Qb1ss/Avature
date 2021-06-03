using UnityEngine;

public class NeedObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Player_Control player))
        {
            Destroy(gameObject);
        }
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//