using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    [Header("Teleportation")]
    [SerializeField] private string _gameScene = "GameScene";
    [SerializeField] private string _openGameScene = "OpenGameScene";
    [Space (height: 5f)]

    [Header ("Windows")]
    [SerializeField] private GameObject _windowControl;
    [SerializeField] private GameObject _windowInfo;
    [Space(height: 5f)]

    [Header("Audio")]
    [SerializeField] private AudioSource _audioEffect;

    private bool _windowActice;



    private void Start()
    {
        Time.timeScale = 1f;
    }



    private void Update()
    {
        if(_windowActice == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _windowControl.SetActive(false);
                _windowInfo.SetActive(false);

                _audioEffect.Play();

                _windowActice = false;
            }
        }
    }



    public void Button_Start()
    {
        _audioEffect.Play();

        SceneManager.LoadScene(_gameScene);
    }



    public void Button_OpenGame()
    {
        _audioEffect.Play();

        SceneManager.LoadScene(_openGameScene);
    }



    public void Button_Control()
    {
        _audioEffect.Play();

        _windowControl.SetActive(true);
        _windowActice = true;
    }



    public void Button_Info()
    {
        _audioEffect.Play();

        _windowInfo.SetActive(true);
        _windowActice = true;
    }



    public void Button_Exit()
    {
        _audioEffect.Play();

        Application.Quit();
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//