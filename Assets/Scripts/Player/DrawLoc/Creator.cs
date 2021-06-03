using UnityEngine;
using UnityEngine.UI;

public class Creator : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _linePrefab;
    [Space(height: 5f)]

    [Tooltip("Количество линий, которые можно нарисовать")]
    [SerializeField] private int _maxPoints;
    [Tooltip("Максимальное количество объектов, разрешенных одновременно")]
    [SerializeField] private int _maxObjects;
    [Space(height: 5f)]

    [SerializeField] private bool _infiniteLine;
    [Space(height: 5f)]

    [Header("UI")]
    [SerializeField] private Text _showPoints;
    [Space(height: 5f)]

    [Header("Destroy")]
    [SerializeField] private float _timeToDestroy;

    [HideInInspector] public bool IsAddingLine;
    private bool _active = true;

    private Maker[] _activeLine = new Maker[1000];
    private GameObject[] _lineGO = new GameObject[1000];

    private GameManager_DrawLoc _manager;

    private int _count = -1;
    private int _objectCount = 0;

    private int _objectIndex = -1;
    private float _time;

    private float _timeGO;


    private void Start()
    {
        _manager = FindObjectOfType<GameManager_DrawLoc>();

        _maxObjects += 1;
    }



    private void Update()
    {
        if(_active == true)
        {
            Drawing();
        }
    }



    private void Drawing()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _count++;

            if (_maxPoints > 0)
            {
                _lineGO[_count] = Instantiate(_linePrefab);
                _activeLine[_count] = _lineGO[_count].GetComponent<Maker>();
                _objectCount++;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            _objectIndex++;
            Destroy(_lineGO[_objectIndex]);
            _time = 0;
            _objectCount--;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _activeLine[_count] = null;
        }

        if (_count > -1 && _activeLine[_count] != null)
        {
            Vector2 mousePos;

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!_infiniteLine && _maxPoints > 0)
            {
                _activeLine[_count].UpdateLine(mousePos);
                _maxPoints--;
            }
            else if (_infiniteLine)
            {
                _activeLine[_count].UpdateLine(mousePos);
            }
            else return;
        }

        DestroyOverTime();

        ShowStatus();

        if (IsAddingLine)
        {
            IsAddingLine = false;
        }
    }



    private void DestroyOverTime()
    {
        if (_count > _objectIndex)
        {
            _time += Time.deltaTime;

            if (_time > _timeToDestroy || _objectCount >= _maxObjects)
            {
                _objectIndex++;
                Destroy(_lineGO[_objectIndex]);
                _time = 0;
                _objectCount--;
            }
        }
    }



    public void DestroyAll()
    {
        if (_count > 0)
        {
            for (int i = _objectIndex - 1; i <= _count; i++)
            {
                Destroy(_lineGO[i]);
            }
        }
    }



    private void ShowStatus()
    {
        if (_maxPoints <= 10) _showPoints.color = Color.red;
        else if (_maxPoints > 10) _showPoints.color = Color.white;

        if (_infiniteLine == false)
        {
            _showPoints.text = _maxPoints.ToString();
        }
        else if (_infiniteLine == true)
        {
            _showPoints.text = "Infinite";
        }
    }



    public void Chargering(int energy) 
    {
        _maxPoints = energy;
    }



    public void AddLine()
    {
        int max = _maxPoints;
    }



    public void ActiveOff()
    {
        _active = false;
    }



    public void ActiveOn()
    {
        _active = true;
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//