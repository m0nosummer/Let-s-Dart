using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private DartManager dartManager;
    [SerializeField] private Vector3 targetScale;
    [SerializeField] private float moveTime = 0.1f;

    private Vector3 _startPos;
    private Vector3 _startScale;
    private bool[] _isSelected = new bool[5];

    private void Start()
    {
        _startPos = transform.position;
        _startScale = transform.localScale;
    }

    public void SelectCard(int cardIdx)
    {
        if (_isSelected[cardIdx])
        {
            _isSelected[cardIdx] = false;
            transform.DOScale(targetScale, moveTime);
        }
        else
        {
            _isSelected[cardIdx] = true;
            transform.DOScale(_startScale, moveTime);
        }
    }
    
}
