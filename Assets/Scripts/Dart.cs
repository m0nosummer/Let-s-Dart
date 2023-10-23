using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DartType { NormalDart = 0, Dart1, Dart2, Dart3, }
public class Dart : MonoBehaviour
{
    [SerializeField] private TargetManager targetManager; // 다트 충돌 시 타겟 데미지 연산
    [SerializeField] private TemplateDart templateDart; // 다트 종류 설정
    [SerializeField] private float interval; // 좌우 Point 사이 간격
    [SerializeField] private float drag; // 좌우 운동 가속도 조절
    
    private Vector3 _startPoint; // start와 end 사이의 거리는 (1 * interval)
    private Vector3 _endPoint;
    private float _moveSpeed; // 좌우 운동 속도
    private int _dartType;
    private bool _isMoving; // 다트 발사 여부 판단

    public int DartType { get; set; } // Dart Manager에서 다트 타입 설정
    public Vector3 moveDir; // 좌우 방향

    private void Awake()
    {
        _isMoving = true;
        _startPoint = transform.position;
        _endPoint = _startPoint + (Vector3.right * interval);
        _moveSpeed = templateDart.darts[_dartType].moveSpeed; // dataType 초기화
        moveDir = Vector3.right;
    }

    private void Update()
    {
        float distStart = Vector3.Distance(transform.position, _startPoint);
        float distEnd = Vector3.Distance(transform.position, _endPoint);
        float minDist = Mathf.Min(distStart, distEnd);
        
        _moveSpeed = templateDart.darts[_dartType].moveSpeed * minDist * drag; // 속도 갱신
        if (_isMoving) transform.position += Time.deltaTime * _moveSpeed * moveDir; // 다트 발사 중이 아닐 때 좌우 왕복
        
        if (distStart <= 0.02f)
        {
            transform.position = _startPoint;
            ChangeDir();
        }
        else if (distEnd <= 0.02f)
        {
            transform.position = _endPoint;
            ChangeDir();
        }
    }
    private void ChangeDir() // 다트 좌우 방향 변경
    {
        if (moveDir == Vector3.left) moveDir = Vector3.right;
        else if (moveDir == Vector3.right) moveDir = Vector3.left;
    }

    public void ShootDart() // 다트 발사 
    {
        transform.position += Time.deltaTime * _moveSpeed * Vector3.up;
        _isMoving = false;
    }
    private void OnTriggerEnter2D(Collider2D collision) // 다트 충돌 시 타겟에 데미지 연산 및 다트 오브젝트 삭제
    {
        if (!collision.CompareTag("Target")) return;
        targetManager.DamageTarget(templateDart.darts[_dartType].damage, templateDart.darts[_dartType].range);
        Destroy(gameObject); // TODO : 다트 소멸 이펙트 추가
    }
}