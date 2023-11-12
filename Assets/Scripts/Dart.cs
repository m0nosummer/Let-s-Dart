using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum DartType { NormalDart = 0, Dart1, Dart2, Dart3, }
public class Dart : MonoBehaviour
{
    [SerializeField] private TemplateDart templateDart; // 다트 종류 설정
    [SerializeField] private float moveSpeed = 5f;
    
    private int _dartType;
    private int _dartDamage;
    private int _dartRange;
    private bool _isCollide;
    private bool _isMoving;

    public bool IsCollide
    {
        get => _isCollide;
        set
        {   _isCollide = value;
             OnDartVariableChanged?.Invoke(value);
         }
    }
    public bool IsMoving
    {
        get => _isMoving;
        set => _isMoving = value;
    }
    public int DartType
    {
        get => _dartType;
        set => _dartType = value;
    }
    public int DartDamage
    {
        get => _dartDamage;
        set => _dartDamage = value;
    }
    public int DartRange
    {
        get => _dartRange;
        set => _dartRange = value;
    }
    
    public event Action<bool> OnDartVariableChanged; // _isCollide 변수 감지
    
    [Tooltip("다트 타입, 데미지, 범위, 다트이미지 초기화")]
    public void Setup(int dartType)
    {
        _dartType = dartType;
        _dartDamage = templateDart.darts[_dartType].damage;
        _dartRange = templateDart.darts[_dartType].range;
        GetComponent<SpriteRenderer>().sprite = templateDart.darts[_dartType].dartImageSprite;
    }
    public void ShootDart()
    {
        IsMoving = true;
        StartCoroutine(nameof(MoveUp));
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        StopCoroutine(nameof(MoveUp));
        if (!collision.CompareTag("Target")) return;
        IsCollide = true;
    }

    private IEnumerator MoveUp()
    {
        while (true)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            yield return null;
        }
    }

    public void OnDie()
    {
        // TODO : 다트 소멸 애니메이션 추가
        Destroy(gameObject);
    }
}