using UnityEngine;

public class Player_ColliderColor : MonoBehaviour
{
    private const string _constColorObjectTag = "ColorObject";



    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag(_constColorObjectTag))
        {
            FindObjectOfType<GameManager_Color>().ActiveOff();
        }
    }



    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag(_constColorObjectTag))
        {
            FindObjectOfType<GameManager_Color>().ActiveOn();
        }
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//