using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DartManager : Singleton<DartManager>
{
    [SerializeField] private InGameUI inGameUI;
    [SerializeField] private TemplateDart templateDart;
    [SerializeField] private TargetManager targetManager;

    private const int AllDartsCnt = 16;

    private Card[] _inGameCardComponent = new Card[4];
    private List<GameObject> _dartList;
    private int[] _dartType = new int[4];
    private int _curDartIdx = 0;
    private bool[] _isUsedType = new bool[16];
    private bool[] _isSelected = new bool[4];
    private bool _isRerolled;

    public List<GameObject> DartList => _dartList;
    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            _inGameCardComponent[i] = inGameUI.inGameCards[i].GetComponent<Card>();
        }
    }

    public void SetStartCards() // 게임 시작 시 고정 다트 1개와 랜덤 다트 4개를 선택
    {
        _isUsedType[0] = true;
        _dartType[0] = 0;
        inGameUI.startCards[0].GetComponent<Image>().sprite = templateDart.darts[0].sprite;
        for (int i = 1; i < 4; i++)
        {
            int curDartType;
            while (true)
            {
                curDartType = Random.Range(1, AllDartsCnt);
                if (!_isUsedType[curDartType]) break;
            }
            _isUsedType[curDartType] = true;
            _dartType[i] = curDartType;
            inGameUI.startCards[i].GetComponent<Image>().sprite = templateDart.darts[curDartType].sprite;
        }
    }
    public void SetInGameCards()
    {
        for (int i = 0; i < 4; i++)
        {
            inGameUI.inGameCards[i].GetComponent<Image>().sprite = templateDart.darts[_dartType[i]].sprite;

        }
    }
    public void SelectCardToReroll(int selectIdx)
    {
        RectTransform rectTransform = inGameUI.startCards[selectIdx].GetComponent<RectTransform>();
        if (selectIdx == 0 || _isRerolled == true) return;
        if (_isSelected[selectIdx])
        {
            _isSelected[selectIdx] = false;
            rectTransform.DOLocalMoveY(-50f, 0.1f).SetRelative();
        }
        else
        {
            _isSelected[selectIdx] = true;
            rectTransform.DOLocalMoveY(50f, 0.1f).SetRelative();
        }
    }
    public void RerollSelectedDart() // 랜덤 다트 4개 중 선택한 다트 다시 뽑기 가능
    {
        if (_isRerolled) return;
        _isRerolled = true;
        for (int i = 1; i < 4; i++)
        {
            if (!_isSelected[i]) continue;
            int prevDartType = _dartType[i];
            int curDartType = 0;
            
            // TODO : 카드 리롤 애니메이션
            
            while (true)
            {
                curDartType = Random.Range(1, AllDartsCnt);
                if (!_isUsedType[curDartType]) break;
            }
            _isUsedType[prevDartType] = false;
            _isUsedType[curDartType] = true;
            _dartType[i] = curDartType;
            inGameUI.startCards[i].GetComponent<Image>().sprite = templateDart.darts[curDartType].sprite;

            // TODO : 카드 버튼에 정보 갱신
        }
    }
    public void SpawnDart(int dartType, Vector3 spawnPosition)
    {
        Vector3 position = spawnPosition + Vector3.back;
        GameObject curDart = Instantiate(templateDart.dartPrefab, position, Quaternion.identity);
        _dartList.Add(curDart);
        StartCoroutine(nameof(OnDartCollision));
    }

    private IEnumerator OnDartCollision()
    {
        if (_dartList[0].GetComponent<Dart>().OnTriggerEnter2D())
        {
            
        }
        _dartList.Clear();
    }
    public void OnClickChangeDart(int dartIdx)
    {
        StartCoroutine(ChangeDart(dartIdx));
    }
    public IEnumerator ChangeDart(int dartIdx) // 다트 버튼을 선택하여 바꾸기
    {
        
        if (_inGameCardComponent[dartIdx]._isSelected) yield break; // 동일 카드 선택 불가
        
        _inGameCardComponent[_curDartIdx].DeselectCard();
        _curDartIdx = dartIdx;
        _inGameCardComponent[_curDartIdx].SelectCard();
        _dartList[0].GetComponent<Dart>().Setup(_curDartIdx);
        yield return null;
    }

    public void DartDestroy()
    {
        StopCoroutine(nameof(ChangeDart));
    }
}
