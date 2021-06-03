using System.Collections;
using UnityEngine;

public class CutScene_DEMO_1 : MonoBehaviour
{
    [Header("Parametrs")]
    [SerializeField] private float _time;
    [SerializeField] private GameObject _trigger;
    [Space(height: 5f)]

    [Header("Cut-Scene")]
    [SerializeField] private float _timeAnim;

    [SerializeField] private GameObject _camera_1;
    [SerializeField] private GameObject _camera_2;
    [SerializeField] private GameObject _wall;

    [SerializeField] private Animator _cameraAnimr;



    private void Start()
    {
        StartCoroutine(Coroutine());
    }



    private IEnumerator Coroutine()
    {
        FindObjectOfType<Player_Control>().PlayerFreeze_On();

        _cameraAnimr.enabled = true;
        _cameraAnimr.GetComponent<Animator>().SetTrigger("Trigger");

        yield return new WaitForSeconds(_timeAnim);

        _camera_2.SetActive(true);
        _camera_1.SetActive(false);
        _wall.SetActive(true);

        yield return new WaitForSeconds(_time);

        Destroy(_trigger);
        Destroy(gameObject);

        FindObjectOfType<Player_Control>().PlayerFreeze_Off();

        yield break;
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//