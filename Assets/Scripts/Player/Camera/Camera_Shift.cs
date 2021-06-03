using UnityEngine;

public class Camera_Shift : MonoBehaviour
{
    [Header("Side / Стороны")]
    [SerializeField] private bool _left;
    [SerializeField] private bool _right;
    [SerializeField] private bool _bottom;
    [SerializeField] private bool _upper;
    [Space(height: 5f)]

    [Header("Position / Позиции")]
    public float LeftNeedPosition;
    public float RightNeedPosition;
    public float BottomNeedPosition;
    public float UpperNeedPosition;
    [Space(height: 5f)]

    [Header("Settings / Настройки")]
    [SerializeField] private float _stepPosition = 0.01f;
    [Tooltip("Изначальное значение меньше, чем стане")]
    [SerializeField] private bool _nigativeStep;
    [Tooltip("Триггер, при котором сробатывает смена камеры")]
    [SerializeField] private GameObject _trigger;
    [Space(height: 5f)]

    [Header("Size / Размер")]
    [Tooltip("Изменяется ли размер камеры")]
    [SerializeField] private bool _shiftSizeZoom;
    public float NeedSize;
    [SerializeField] private float _stepSize = 10f;
    [Tooltip("Изначальное значение меньше, чем станет")]
    [SerializeField] private bool _sizeZoomIn;

    private bool _lastPositionLeft = true;
    private bool _lastPositionRight = true;
    private bool _lastPositionBottom = true;
    private bool _lastPositionUpper = true;
    private bool _lastSize = true;
    private bool _reworkPosition;
    private bool _reworkSize;

    private bool _startTransformPosition;
    [Space(height: 5f)]

    [SerializeField] private Collider2D _collider2D;
    private Camera_Control _player_Camera;
    private Camera _camera;



    private void Start()
    {
        _player_Camera = GetComponent<Camera_Control>();
        _camera = GetComponent<Camera>();

        _collider2D.isTrigger = true;

        if (_nigativeStep == true)
        {
            _stepPosition = -_stepPosition;
        }

        _stepSize = _stepSize / 1000;
    }



    private void Update()
    {
        Shifting();
    }



    private void Shifting()
    {
        if (_lastPositionLeft == true)
        {
            if (_startTransformPosition == true)
            {
                _reworkPosition = true;

                if (_left == true)
                {
                    if (_nigativeStep == true)
                    {
                        _player_Camera.LeftLimit += _stepPosition;

                        if (_player_Camera.LeftLimit <= LeftNeedPosition)
                        {
                            _lastPositionLeft = false;
                        }
                    }
                    else if (_nigativeStep == false)
                    {
                        _player_Camera.RightLimit += _stepPosition;

                        if (_player_Camera.RightLimit >= RightNeedPosition)
                        {
                            _lastPositionLeft = false;
                        }
                    }
                }
            }
        }

        if (_lastPositionRight == true)
        {
            if (_startTransformPosition == true)
            {
                _reworkPosition = true;

                if (_right == true)
                {
                    if (_nigativeStep == true)
                    {
                        _player_Camera.RightLimit += _stepPosition;

                        if (_player_Camera.RightLimit <= RightNeedPosition)
                        {
                            _lastPositionRight = false;
                        }
                    }
                    else if (_nigativeStep == false)
                    {
                        _player_Camera.RightLimit += _stepPosition;

                        if (_player_Camera.RightLimit >= RightNeedPosition)
                        {
                            _lastPositionRight = false;
                        }
                    }
                }
            }
        }

        if (_lastPositionBottom == true)
        {
            if (_startTransformPosition == true)
            {
                _reworkPosition = true;

                if (_bottom == true)
                {
                    if (_nigativeStep == true)
                    {
                        _player_Camera.BottomLimit += _stepPosition;

                        if (_player_Camera.BottomLimit <= BottomNeedPosition)
                        {
                            _lastPositionBottom = false;
                        }
                    }
                    else if (_nigativeStep == false)
                    {
                        _player_Camera.BottomLimit += _stepPosition;

                        if (_player_Camera.BottomLimit >= BottomNeedPosition)
                        {
                            _lastPositionBottom = false;
                        }
                    }
                }
            }
        }

        if (_lastPositionUpper == true)
        {
            if (_startTransformPosition == true)
            {
                _reworkPosition = true;

                if (_upper == true)
                {
                    if (_nigativeStep == true)
                    {
                        _player_Camera.UpperLimit += _stepPosition;

                        if (_player_Camera.UpperLimit <= UpperNeedPosition)
                        {
                            _lastPositionUpper = false;
                        }
                    }
                    else if (_nigativeStep == false)
                    {
                        _player_Camera.UpperLimit += _stepPosition;

                        if (_player_Camera.UpperLimit >= UpperNeedPosition)
                        {
                            _lastPositionUpper = false;
                        }
                    }
                }
            }
        }

        if (_reworkPosition == true &&
            _lastPositionLeft == false &&
            _lastPositionRight == false &&
            _lastPositionBottom == false &&
            _lastPositionUpper == false)
        {
            if (_left == true) _player_Camera.LeftLimit = LeftNeedPosition;
            if (_right == true) _player_Camera.RightLimit = RightNeedPosition;
            if (_bottom == true) _player_Camera.BottomLimit = BottomNeedPosition;
            if (_upper == true) _player_Camera.UpperLimit = UpperNeedPosition;
        }

        if (_lastSize == true)
        {
            if (_shiftSizeZoom == true)
            {
                if (_startTransformPosition == true)
                {
                    _reworkSize = true;

                    if (_sizeZoomIn == false)
                    {
                        _camera.orthographicSize += _stepSize * 2;

                        if (_camera.orthographicSize >= NeedSize)
                        {
                            _lastSize = false;
                        }
                    }
                    else if (_sizeZoomIn == true)
                    {
                        _camera.orthographicSize -= _stepSize * 2;

                        if (_camera.orthographicSize <= NeedSize)
                        {
                            _lastSize = false;
                        }
                    }
                }
            }
        }

        if (_lastSize == false && _reworkSize == true)
        {
            _camera.orthographicSize = NeedSize;
        }
    }



    private void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.gameObject == _trigger)
        {
            _startTransformPosition = true;
            _collider2D.enabled = false;

            if (_left) _lastPositionLeft = true; else _lastPositionLeft = false;
            if (_right) _lastPositionRight = true; else _lastPositionRight = false;
            if (_bottom) _lastPositionBottom = true; else _lastPositionBottom = false;
            if (_upper) _lastPositionUpper = true; else _lastPositionUpper = false;
        }
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//