using UnityEngine;

public class Charger : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource _chargeEffect;
    [Space(height: 5f)]

    [Header("Control")]
    [SerializeField] private int _energy;
    private bool _active = true;
    private bool _player = false;

    private Animator _animr;



    private void Start()
    {
        _animr = GetComponent<Animator>();
    }



    private void Update()
    {
        if(_player == true && _active == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _chargeEffect.Play();

                _animr.SetTrigger("Trigger");

                _active = false;

                FindObjectOfType<Player_Control>().PlayerFreeze_On();
                FindObjectOfType<Creator>().Chargering(_energy);
            }
        }
    }



    public void EndAnimation()
    {
        FindObjectOfType<Player_Control>().PlayerFreeze_Off();
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        string tagPlayer = "Player";

        if (col.CompareTag(tagPlayer))
        {
            _player = true;
        }
    }



    private void OnTriggerExit2D(Collider2D col)
    {
        string tagPlayer = "Player";

        if (col.CompareTag(tagPlayer))
        {
            _player = false;
        }
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//