using UnityEngine;

public class GameManager_ColorFull : MonoBehaviour
{
    [Header("Control")]
    [SerializeField] private int _currentColor = 0;

    [SerializeField] private bool[] _colors = { true, false, false, false, false, false, false, false, false };
    [Space(height: 5f)]

    [SerializeField] private GameObject _selectorColor;
    [Space(height: 5f)]

    [SerializeField] private Animator[] _animrCameras;
    [Space(height: 5f)]

    [SerializeField] private Camera[] _backgroundsCamera;
    [Space(height: 5f)]

    [Header("Audio")]
    [SerializeField] private AudioSource _newColorEffect;
    [Space(height: 5f)]

    [Header("SpriteRenderer")]
    [SerializeField] private SpriteRenderer[] _spriteRendererRed;
    [SerializeField] private SpriteRenderer[] _spriteRendererPink;
    [SerializeField] private SpriteRenderer[] _spriteRendererViolet;
    [SerializeField] private SpriteRenderer[] _spriteRendererBlue;
    [SerializeField] private SpriteRenderer[] _spriteRendererGrey;
    [SerializeField] private SpriteRenderer[] _spriteRendererGreen;
    [SerializeField] private SpriteRenderer[] _spriteRendererYellow;
    [SerializeField] private SpriteRenderer[] _spriteRendererOrange;
    [Space(height: 5f)]

    [Header("Colliders")]
    [SerializeField] private Collider2D[] _collidersRed;
    [SerializeField] private Collider2D[] _collidersPink;
    [SerializeField] private Collider2D[] _collidersViolet;
    [SerializeField] private Collider2D[] _collidersBlue;
    [SerializeField] private Collider2D[] _collidersGrey;
    [SerializeField] private Collider2D[] _collidersGreen;
    [SerializeField] private Collider2D[] _collidersYellow;
    [SerializeField] private Collider2D[] _collidersOrange;

    private bool _active = true;



    private void Start()
    {
        _active = true;
    }



    private void Update()
    {
        if (_active == true)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                SelectorColor();
                ConditionDraw_On();
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _selectorColor.SetActive(false);
                Time.timeScale = 1f;

                _newColorEffect.Play();

                ConditionDraw_Off();

                foreach (Animator animrs in _animrCameras)
                {
                    animrs.GetComponent<Animator>().SetBool("Zoom", false);
                }
            }
        }
    }



    private void SelectorColor()
    {
        foreach (Animator animrs in _animrCameras)
        {
            animrs.GetComponent<Animator>().SetBool("Zoom", true);
        }

        _selectorColor.SetActive(true);
        Time.timeScale = 0.5f;
    }



    private void ColorSelecting()
    {
        for (int i = 0; i < _colors.Length; i++)
        {
            _colors[i] = false;
        }

        _colors[_currentColor] = true;

        ColorSelected();
    }



    private void ColorSelected()
    {
        if (_colors[0])
        {
            foreach (Camera camera in _backgroundsCamera)
            {
                camera.GetComponent<Camera>().backgroundColor = new Color(0.1603774f, 0.1603774f, 0.1603774f);
            }

            Colors_Reset();
        }

        //==Red==//
        else if (_colors[1])
        {
            Debug.Log(_currentColor);

            foreach (Camera camera in _backgroundsCamera)
            {
                camera.GetComponent<Camera>().backgroundColor = Color.red;
            }

            Colors_Reset();

            foreach (Collider2D collidersRed in _collidersRed)
            {
                collidersRed.isTrigger = true;
            }

            foreach (SpriteRenderer spriteRendererRed in _spriteRendererRed)
            {
                spriteRendererRed.enabled = false;
            }
        }

        //==Pink==//
        else if (_colors[2])
        {
            Debug.Log(_currentColor);

            foreach (Camera camera in _backgroundsCamera)
            {
                camera.GetComponent<Camera>().backgroundColor = new Color(0.972549f, 0.09411765f, 0.5803922f);
            }

            Colors_Reset();

            foreach (Collider2D collidersPink in _collidersPink)
            {
                collidersPink.isTrigger = true;
            }

            foreach (SpriteRenderer spriteRendererPink in _spriteRendererPink)
            {
                spriteRendererPink.enabled = false;
            }
        }

        //==Violet==//
        else if (_colors[3])
        {
            Debug.Log(_currentColor);

            foreach (Camera camera in _backgroundsCamera)
            {
                camera.GetComponent<Camera>().backgroundColor = new Color(0.6078432f, 0.1490196f, 0.572549f);
            }

            Colors_Reset();

            foreach (Collider2D collidersViolet in _collidersViolet)
            {
                collidersViolet.isTrigger = true;
            }

            foreach (SpriteRenderer spriteRendererViolet in _spriteRendererViolet)
            {
                spriteRendererViolet.enabled = false;
            }
        }

        //==Blue==//
        else if (_colors[4])
        {
            Debug.Log(_currentColor);

            foreach (Camera camera in _backgroundsCamera)
            {
                camera.GetComponent<Camera>().backgroundColor = Color.blue;
            }

            Colors_Reset();

            foreach (Collider2D collidersBlue in _collidersBlue)
            {
                collidersBlue.isTrigger = true;
            }

            foreach (SpriteRenderer spriteRendererBlue in _spriteRendererBlue)
            {
                spriteRendererBlue.enabled = false;
            }
        }

        //==Grey==//
        else if (_colors[5])
        {
            Debug.Log(_currentColor);
            
            foreach (Camera camera in _backgroundsCamera)
            {
                camera.GetComponent<Camera>().backgroundColor = new Color(0.5019608f, 0.5019608f, 0.5019608f);
            }

            Colors_Reset();

            foreach (Collider2D collidersGrey in _collidersGrey)
            {
                collidersGrey.isTrigger = true;
            }

            foreach (SpriteRenderer spriteRendererGrey in _spriteRendererGrey)
            {
                spriteRendererGrey.enabled = false;
            }
        }

        //==Green==//
        else if (_colors[6])
        {
            foreach (Camera camera in _backgroundsCamera)
            {
                camera.GetComponent<Camera>().backgroundColor = new Color(0f, 0.5019608f, 0f);
            }

            Colors_Reset();

            foreach (Collider2D collidersGreen in _collidersGreen)
            {
                collidersGreen.isTrigger = true;
            }

            foreach (SpriteRenderer spriteRendererGreen in _spriteRendererGreen)
            {
                spriteRendererGreen.enabled = false;
            }
        }

        //==Yellow==//
        else if (_colors[7])
        {
            foreach (Camera camera in _backgroundsCamera)
            {
                camera.GetComponent<Camera>().backgroundColor = new Color(1f, 1f, 0f);
            }

            Colors_Reset();

            foreach (Collider2D collidersYellow in _collidersYellow)
            {
                collidersYellow.isTrigger = true;
            }

            foreach (SpriteRenderer spriteRendererYellow in _spriteRendererYellow)
            {
                spriteRendererYellow.enabled = false;
            }
        }

        //==Orange==//
        else if (_colors[8])
        {
            foreach (Camera camera in _backgroundsCamera)
            {
                camera.GetComponent<Camera>().backgroundColor = new Color(1f, 0.6470588f, 0f);
            }

            Colors_Reset();

            foreach (Collider2D collidersOrange in _collidersOrange)
            {
                collidersOrange.isTrigger = true;
            }

            foreach (SpriteRenderer spriteRendererOrange in _spriteRendererOrange)
            {
                spriteRendererOrange.enabled = false;
            }
        }
    }



    private void Colors_Reset()
    {
        //==Red==//
        foreach (Collider2D collidersRed in _collidersRed)
        {
            collidersRed.isTrigger = false;
        }
        foreach (SpriteRenderer spriteRendererRed in _spriteRendererRed)
        {
            spriteRendererRed.enabled = true;
        }

        //==Pink==//
        foreach (Collider2D collidersPink in _collidersPink)
        {
            collidersPink.isTrigger = false;
        }
        foreach (SpriteRenderer spriteRendererPink in _spriteRendererPink)
        {
            spriteRendererPink.enabled = true;
        }

        //==Violet==//
        foreach (Collider2D collidersViolet in _collidersViolet)
        {
            collidersViolet.isTrigger = false;
        }
        foreach (SpriteRenderer spriteRendererViolet in _spriteRendererViolet)
        {
            spriteRendererViolet.enabled = true;
        }

        //==Blue==//
        foreach (Collider2D collidersBlue in _collidersBlue)
        {
            collidersBlue.isTrigger = false;
        }
        foreach (SpriteRenderer spriteRendererBlue in _spriteRendererBlue)
        {
            spriteRendererBlue.enabled = true;
        }

        //==Grey==//
        foreach (Collider2D collidersGrey in _collidersGrey)
        {
            collidersGrey.isTrigger = false;
        }
        foreach (SpriteRenderer spriteRendererGrey in _spriteRendererGrey)
        {
            spriteRendererGrey.enabled = true;
        }

        //==Green==//
        foreach (Collider2D collidersGreen in _collidersGreen)
        {
            collidersGreen.isTrigger = false;
        }
        foreach (SpriteRenderer spriteRendererGreen in _spriteRendererGreen)
        {
            spriteRendererGreen.enabled = true;
        }

        //==Yellow==//
        foreach (Collider2D collidersYellow in _collidersYellow)
        {
            collidersYellow.isTrigger = false;
        }
        foreach (SpriteRenderer spriteRendererYellow in _spriteRendererYellow)
        {
            spriteRendererYellow.enabled = true;
        }

        //==Orange==//
        foreach (Collider2D collidersOrange in _collidersOrange)
        {
            collidersOrange.isTrigger = false;
        }
        foreach (SpriteRenderer spriteRendererOrange in _spriteRendererOrange)
        {
            spriteRendererOrange.enabled = true;
        }
    }



    public void SelectingColor(int index)
    {
        if (_active == true)
        {
            _currentColor = index;

            ColorSelecting();
        }
    }



    public void ActiveOff()
    {
        _active = false;
    }



    public void ActiveOn()
    {
        _active = true;
    }



    private void ConditionDraw_On()
    {
        FindObjectOfType<Creator>().ActiveOff();
    }



    private void ConditionDraw_Off()
    {
        FindObjectOfType<Creator>().ActiveOn();
    }
}
//By Bortsov "@Qb1ss" Gleb🏂//