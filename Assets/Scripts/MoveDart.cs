using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDart : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private DartManager dartManager;
    
    private Vector3 _offset;
    private Vector3 _initPos;
    private bool _isDragging = false;
    private void Update()
    {
        if (_isDragging)
        {
            // 현재 터치 위치를 화면 좌표로 변환
            Vector3 touchPosition = 
                mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            
            // 물체를 터치 위치에 이동
            dartManager.curDart.transform.position = touchPosition + _offset;
        }
    }

    private void OnMouseDown()
    {
        // 터치 시작 위치와 물체 중심 간의 오프셋 계산
        _offset = transform.position - 
                 mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        _isDragging = true;
    }

    private void OnMouseUp()
    {
        dartManager.curDart.GetComponent<Dart>().ShootDart(); // 터치 영역을 벗어나면 다트 발사
        _isDragging = false;
    }

    private void OnMouseExit()
    {
        dartManager.curDart.GetComponent<Dart>().ShootDart(); // 터치 영역을 벗어나면 다트 발사
        _isDragging = false;
    }
}
