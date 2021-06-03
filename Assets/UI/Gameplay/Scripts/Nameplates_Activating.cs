using UnityEngine;

public class Nameplates_Activating : MonoBehaviour
{
    [SerializeField] private Animator _window;
    [Space(height: 5f)]

    [SerializeField] private AudioSource _activatinsSource;
    [Space(height: 5f)]

    [Tooltip ("Object and trigger")]
    [SerializeField] private GameObject[] _objects;

    private bool _player;
    private bool _active;



    private void Update()
    {
        if (_player == true)
        {
            _window.GetComponent<Animator>().SetBool("Active", true);

            _active = true;
        }
        else if (_player == false)
        {
            _window.GetComponent<Animator>().SetBool("Active", false);
        }

        Nameplates_Activatind_AudioEffect();
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        string playerTag = "Player";

        if (col.CompareTag(playerTag))
        {
            _player = true;
        }
    }



    private void OnTriggerExit2D(Collider2D col)
    {
        string playerTag = "Player";

        if (col.CompareTag(playerTag))
        {
            _player = false;

            foreach(GameObject objects in _objects)
            {
                Destroy(objects, 0.2f);
            }
        }
    }



    private void Nameplates_Activatind_AudioEffect()
    {
        if(_active == true) _activatinsSource.Play();
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//