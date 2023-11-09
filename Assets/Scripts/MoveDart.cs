using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveDart : MonoBehaviour
{
    [SerializeField] private DartManager dartManager;
    [SerializeField] private InGameUI inGameUI;
    
    private Dart _dart;
    private Camera _mainCamera;
    private Rigidbody2D _rb;

    private void Start()
    {
        _dart = GetComponent<Dart>();
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = _mainCamera.ScreenToWorldPoint(touch.position);
            
            GameObject clickedObj = EventSystem.current.currentSelectedGameObject;
            
            if (!clickedObj.CompareTag("PlayScreen")) return;
            
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _rb.MovePosition(new Vector2(touchPos.x, transform.position.y));
                    break;
                
                case TouchPhase.Moved:
                    _rb.MovePosition(new Vector2(touchPos.x, transform.position.y));    
                    break;
                
                case TouchPhase.Ended:
                    _dart.ShootDart();
                    dartManager.SpawnDart(dartManager.DartTypes[2],
                        inGameUI.playPanel.transform.position +
                        Vector3.down * (inGameUI.ScreenWidth * inGameUI.PlayPanelRatio / 2));
                    break;
            }
        }
    }
}
