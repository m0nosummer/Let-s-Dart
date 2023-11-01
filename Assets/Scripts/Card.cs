using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private DartManager dartManager;
    [SerializeField] private float moveAmount = 10f;
    [SerializeField] private float moveTime = 0.1f;

    private RectTransform _rectTransform;
    private Vector3 _startPos;
    private Vector3 _startScale;
    private readonly bool[] _isSelected = new bool[4];

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startPos = _rectTransform.position;
        _startScale = _rectTransform.localScale;
    }

    public void SelectCard(int cardIdx)
    {
        if (_isSelected[cardIdx]) return; // 이미 선택 중인 카드는 선택 불가
        
        _isSelected[cardIdx] = true;
        _rectTransform.DOLocalMoveY(moveAmount, moveTime).SetRelative();
    }

    public void DeselectCard(int cardIdx)
    {
        _isSelected[cardIdx] = false;
        _rectTransform.DOLocalMoveY(-1 * moveAmount, moveTime).SetRelative();
    }
}
