using UnityEngine;
using UnityEngine.SceneManagement;

public class Window_EndGame : MonoBehaviour
{
    private const string _constMenuScene = "MenuScene";



    private void Start()
    {
        FindObjectOfType<Player_Control>().PlayerFreeze_On();
    }



    private void EndGame()
    {
        //отключение всех звуков
        Time.timeScale = 0;
    }



    public void Button_Menu()
    {
        SceneManager.LoadScene(_constMenuScene);
    }



    public void Button_Exit()
    {
        Application.Quit();
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//