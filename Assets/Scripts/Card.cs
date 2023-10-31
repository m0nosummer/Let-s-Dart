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
    private bool _canSelectOtherCard;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startPos = _rectTransform.position;
        _startScale = _rectTransform.localScale;
    }

    public void SelectCard(int cardIdx)
    {
        if (_isSelected[cardIdx]) // 카드 선택 해제
        {
            _isSelected[cardIdx] = false;
            _rectTransform.DOLocalMoveY(-1 * moveAmount, moveTime).SetRelative();
        }
        else
        {
            for (int i = 0; i < 4; i++) // 다른 카드가 이미 선택되었을 경우 중복 선택 불가능
            {
                if (_isSelected[i] == true) return;
            }
            _isSelected[cardIdx] = true;
            _rectTransform.DOLocalMoveY(moveAmount, moveTime).SetRelative();
        }
    }
    
}
