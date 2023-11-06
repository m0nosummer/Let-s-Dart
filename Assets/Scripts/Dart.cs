using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum DartType { NormalDart = 0, Dart1, Dart2, Dart3, }
public class Dart : MonoBehaviour
{
    [SerializeField] private TemplateDart templateDart; // 다트 종류 설정
    [SerializeField] private float drag; // 좌우 운동 가속도 조절
    [SerializeField] private float moveSpeed;
    
    private float _moveSpeed;
    private int _dartType;
    private int _dartDamage;
    private int _dartRange;
    private Sprite _dartImage;

    public bool isCollide;
    public int DartType { get; set; }
    public Vector3 moveDir; // 좌우 방향

    public void Awake() // Game Start 버튼으로 호출됨
    {
        _dartType = 1;
        _moveSpeed = moveSpeed;
        moveDir = Vector3.right;
        _dartImage = GetComponent<SpriteRenderer>().sprite; 
    }
    public void Setup(int dartType) // 다트타입, 데미지, 범위, 다트이미지 초기화
    {
        _dartType = dartType;
        _dartDamage = templateDart.darts[_dartType].damage;
        _dartRange = templateDart.darts[_dartType].range;
        _dartImage = templateDart.darts[_dartType].cardImageSprite;
    }
    private void Update()
    {
        
    }
    public void ShootDart() // 다트 발사 
    {
        transform.position += Time.deltaTime * _moveSpeed * Vector3.up;
    }
    public void OnTriggerEnter2D(Collider2D collision) // 다트 충돌 시 타겟에 데미지 연산 및 다트 오브젝트 삭제
    {
        isCollide = true;
    }

    public void OnDie()
    {
        Destroy(gameObject);
    }
}