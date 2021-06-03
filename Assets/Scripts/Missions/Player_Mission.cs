using UnityEngine;

public class Player_Mission : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private int _needObjects;
    [SerializeField] private int _objects;
    [Space(height: 5f)]

    [Header("Triggers & Walls")]
    [SerializeField] private GameObject _trigger;
    [SerializeField] private GameObject _wall;
    [Space(height: 5f)]

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;



    private void Update()
    {
        FindingObject();
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent(out NeedObject needObject))
        {
            _objects++;

            _audioSource.Play();
        }
    }



    private void FindingObject()
    {
        if (_objects >= _needObjects)
        {
            _trigger.SetActive(true);
            _wall.SetActive(false);
        }
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//