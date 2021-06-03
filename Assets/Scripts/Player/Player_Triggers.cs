using UnityEngine;

public class Player_Triggers : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out Trigger_CutScene trigger_CutScene))
        {
            trigger_CutScene.CutScene();
        }
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//
