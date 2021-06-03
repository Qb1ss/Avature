using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    [Header ("Border")]
    [SerializeField] private float _dumping = 25f;

    [Tooltip("Левая граница")]
    public float LeftLimit;
    [Tooltip("Правая граница")]
    public float RightLimit;
    [Tooltip("Нижняя граница")]
    public float BottomLimit;
    [Tooltip("Верхняя граница")]
    public float UpperLimit;
    [Space(height: 5f)]

    [Tooltip("Умеет ли камера делать вращение")]
    [SerializeField] private bool _rotateOn = true;
    private bool _isLeft;
    private bool _rotate = false;

    private int _lastX;

    private Vector2 _offset;
    private Transform _player;



    private void Start()
    {
        _offset = new Vector2(Mathf.Abs(_offset.x), _offset.y);
        FindPlayer(_isLeft);

        _rotate = false;
    }



    private void Update()
    {
        CameraBorder();
        Rotating();
    }



    private void CameraBorder()
    {
        if (_player)
        {
            transform.position = new Vector3(_player.position.x - _offset.x, _player.position.y - _offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(_player.position.x + _offset.x, _player.position.y + _offset.y, transform.position.z);
        }

        transform.position = new Vector3
        (
        Mathf.Clamp(transform.position.x, LeftLimit, RightLimit),
        Mathf.Clamp(transform.position.y, UpperLimit, BottomLimit),
        transform.position.z
        );
    }



    public void FindPlayer(bool playerIsLeft)
    {
        _player = GameObject.FindObjectOfType<Player_Control>().transform;
        _lastX = Mathf.RoundToInt(_player.position.x);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(LeftLimit, UpperLimit), new Vector2(RightLimit, UpperLimit));
        Gizmos.DrawLine(new Vector2(LeftLimit, BottomLimit), new Vector2(RightLimit, BottomLimit));
        Gizmos.DrawLine(new Vector2(LeftLimit, UpperLimit), new Vector2(LeftLimit, BottomLimit));
        Gizmos.DrawLine(new Vector2(RightLimit, UpperLimit), new Vector2(RightLimit, BottomLimit));
    }



    private void Rotating()
    {
        if (_rotateOn == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _rotate = true;
            }
            else
            {
                _rotate = false;
            }

            if (_rotate == true)
            {
                Rotated();
            }
        }
    }



    private void Rotated()
    {
        transform.Rotate(0, 0, 180f);
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//