using UnityEngine;

public class Trigger_CutScene : MonoBehaviour
{
    [SerializeField] private GameObject _cutScene;
    [SerializeField] private GameObject _player;



    public void CutScene()
    {
        FindObjectOfType<Player_Control>().PlayerFreeze_On();

        _player.GetComponent<Player_Control>().Static = true;

        _cutScene.SetActive(true);
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//