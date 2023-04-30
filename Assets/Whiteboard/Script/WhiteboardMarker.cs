using OVR.OpenVR;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build;
using UnityEngine;

public class WhiteboardMarker : MonoBehaviour
{
    [SerializeField] private Transform _tip;

    //Make public later to modulate this value
    public int _penSize = 5;

    public AudioSource _collisionSound, _squeekingSound;
    private bool _hasPlayed, _hasPlayedSqueek;
    private Renderer _renderer;
    private Color[] _colors;
    private float _tipHeight;

    private RaycastHit _touch;
    private Whiteboard _whiteboard;

    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;

    private Quaternion _lastTouchRot, firstRot;

  
   

    void Start()
    {
        _renderer = _tip.GetComponent<Renderer>();
        _tipHeight = _tip.localScale.y;
        
        _collisionSound = GetComponent<AudioSource>();
    }

   
    void Update()
    {
        Draw();
       

    }

    private void Draw()
    {
        
        if (Physics.Raycast(_tip.position, transform.up, out _touch, _tipHeight))
        {

            if (_touch.transform.CompareTag("Whiteboard"))
            {
                if (!_hasPlayed)
                {
                    _collisionSound.Play();
                    _hasPlayed = true;
                }


                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }
                if (!_hasPlayedSqueek)
                {
                    _squeekingSound.Play();
                    _hasPlayedSqueek = true;
                }
                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);
                checkPenSize();
                Debug.Log("PenSize : " + _penSize);

                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2));

                

                //Out of bounds check
                if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x) return;

                if (_touchedLastFrame)
                {
                    _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, _colors);
                 


                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colors);

                    }

                    transform.rotation = _lastTouchRot;

                    _whiteboard.texture.Apply();
                }

                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
               
                return;

            }
        }

        _whiteboard = null;
        _touchedLastFrame = false;

        _squeekingSound.Stop();
        _hasPlayed = false;
        _hasPlayedSqueek = false;
    }

    public void checkPenSize()
    {
        _colors = Enumerable.Repeat(_renderer.material.color, _penSize * _penSize).ToArray();
    }

 

}
