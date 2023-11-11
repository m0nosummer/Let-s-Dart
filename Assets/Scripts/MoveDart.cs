using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveDart : MonoBehaviour
{
    
    private Dart _dart;
    private Rigidbody2D _rb;
    private DartManager _dartManager;
    private Camera _mainCamera;
    private bool _canMove = true;
    private void Start()
    {
        _dart = GetComponent<Dart>();
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _dartManager = GameObject.FindWithTag("DartManager").GetComponent<DartManager>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = _mainCamera.ScreenToWorldPoint(touch.position);
            
            GameObject clickedObj = EventSystem.current.currentSelectedGameObject;
            
            if (!clickedObj.CompareTag("PlayScreen")) return;
            if (!_canMove) return;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _rb.MovePosition(new Vector2(touchPos.x, transform.position.y));
                    break;
                
                case TouchPhase.Moved:
                    _rb.MovePosition(new Vector2(touchPos.x, transform.position.y));    
                    break;
                
                case TouchPhase.Ended:
                    _dartManager.TouchEnd();
                    _canMove = false;
                    break;
            }
        }
    }
}
