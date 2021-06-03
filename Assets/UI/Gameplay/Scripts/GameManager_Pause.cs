using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Pause : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _pausePanel;
    [Space(height: 5f)]

    [Header("Buttons")]
    [SerializeField] private string _thisScene;
    [Space(height: 5f)]

    [Header("Audio")]
    [SerializeField] private AudioSource _tapEffect;

    private bool _pause = false;



    private void Start()
    {
        _pausePanel.SetActive(false);
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }



    private void Pause()
    {
        _pause = !_pause;

        if (_pause == true)
        {
            Time.timeScale = 0;

            _pausePanel.SetActive(true);

            ConditionPause_On();
            ConditionPausePlayer_On();
        }
        else if (_pause == false)
        {
            Time.timeScale = 1;

            _pausePanel.SetActive(false);

            ConditionPause_Off();
            ConditionPausePlayer_Off();
        }
    }



    public void Button_Continue()
    {
        _tapEffect.Play();

        Pause();
    }



    public void Button_Restart()
    {
        _tapEffect.Play();

        SceneManager.LoadScene(_thisScene);
    }



    public void Button_Mute()
    {
        //отключение звука
    }



    public void Button_Exit()
    {
        _tapEffect.Play();

        string menuScene = "MenuScene";
        SceneManager.LoadScene(menuScene);
    }



    public void ConditionPause_On()
    {
        FindObjectOfType<Creator>().ActiveOff();
        FindObjectOfType<GameManager_Color>().ActiveOff();
    }



    public void ConditionPausePlayer_On()
    {
        FindObjectOfType<Player_Control>().PlayerFreeze_On();
    }



    public void ConditionPause_Off()
    {
        FindObjectOfType<Creator>().ActiveOn();
        FindObjectOfType<GameManager_Color>().ActiveOn();
    }



    public void ConditionPausePlayer_Off()
    {
        FindObjectOfType<Player_Control>().PlayerFreeze_Off();
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//