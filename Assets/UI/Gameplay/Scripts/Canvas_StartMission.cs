using UnityEngine;

public class Canvas_StartMission : MonoBehaviour
{
    public void StartAnim()
    {
        FindObjectOfType<Player_Control>().PlayerFreeze_On();
    }



    public void EndAnim()
    {
        FindObjectOfType<Player_Control>().PlayerFreeze_Off();

        gameObject.SetActive(false);
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//