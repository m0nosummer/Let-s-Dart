using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DartManager : Singleton<DartManager>
{
    [SerializeField] private InGameUI inGameUI;
    [SerializeField] private TemplateDart templateDart;
    [SerializeField] private TargetManager targetManager;

    public List<GameObject> curDartList;

    private Card[] _inGameCardComponent = new Card[4];
    private List<Dart> _curDartComponentList;
    private int[] _dartTypes = new int[4];
    private int _allDartsCnt = 16;
    private int _curDartIdx;
    private bool[] _isUsedType = new bool[16];
    private bool[] _isSelected = new bool[4];
    private bool _isRerolled;

    public int[] DartTypes
    {
        get
        {
            return _dartTypes;
        }
    }
    private void Awake()
    {
        curDartList = new List<GameObject>();
        _curDartComponentList = new List<Dart>();
        for (int i = 0; i < 4; i++)
        {
            _inGameCardComponent[i] = inGameUI.inGameCards[i].GetComponent<Card>();
        }
    }
    public void SetStartCards() // 게임 시작 시 고정 다트 1개와 랜덤 다트 4개를 선택
    {
        _isUsedType[0] = true;
        _dartTypes[0] = 0;
        inGameUI.startCards[0].GetComponent<Image>().sprite = templateDart.darts[0].cardImageSprite;
        for (int i = 1; i < 4; i++)
        {
            int curDartType;
            while (true)
            {
                curDartType = Random.Range(1, _allDartsCnt);
                if (!_isUsedType[curDartType]) break;
            }
            _isUsedType[curDartType] = true;
            _dartTypes[i] = curDartType;
            inGameUI.startCards[i].GetComponent<Image>().sprite = templateDart.darts[curDartType].cardImageSprite;
        }
    }
    public void SetInGameCards() // 선택한 카드 배치
    {
        for (int i = 0; i < 4; i++)
        {
            inGameUI.inGameCards[i].GetComponent<Image>().sprite = templateDart.darts[_dartTypes[i]].cardImageSprite;
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
            int prevDartType = _dartTypes[i];
            int curDartType = 0;
            
            // TODO : 카드 리롤 애니메이션
            
            while (true)
            {
                curDartType = Random.Range(1, _allDartsCnt);
                if (!_isUsedType[curDartType]) break;
            }
            _isUsedType[prevDartType] = false;
            _isUsedType[curDartType] = true;
            _dartTypes[i] = curDartType;
            inGameUI.startCards[i].GetComponent<Image>().sprite = templateDart.darts[curDartType].cardImageSprite;

            // TODO : 카드 버튼에 정보 갱신
        }
    }
    public void SpawnDart(int dartType, Vector3 spawnPosition) // spawnPosition + dart 크기 * 2 에서 spawn
    {
        Vector3 position = spawnPosition + (Vector3.forward * 80);
        GameObject clone = Instantiate(templateDart.dartPrefab, position, Quaternion.identity);
        curDartList.Add(clone);
        _curDartIdx = 0;
        _curDartComponentList.Add(clone.GetComponent<Dart>());
        _curDartComponentList.Last().Setup(_curDartIdx);
        _curDartComponentList.Last().OnDartVariableChanged += HandleDartVariableChanged;
        StartCoroutine(nameof(OnDartCollision));
    }

    private void HandleDartVariableChanged(bool newValue)
    {
        if (newValue)
        {
            Debug.Log("true man");
        }
    }
    private IEnumerator OnDartCollision() // 다트 충돌 시 데미지 연산, 다트 삭제
    {
        Debug.Log(curDartList.Last().GetComponent<Dart>().IsCollide.ToString());
        if (curDartList.Last().GetComponent<Dart>().IsCollide)
        {
            Debug.Log("Collide" + curDartList.Last().name);
            // targetManager.DamageTarget(_curDartComponentList.Last().DartDamage, _curDartComponentList.Last().DartRange);
            curDartList.Last().GetComponent<Dart>().IsCollide = false;
            DartDestroy();
        }
        yield return null;
    }
    public void ChangeDart(int dartIdx) // 다트 버튼을 선택하여 바꾸기
    {
        if (_inGameCardComponent[dartIdx]._isSelected) return; // 동일 카드 선택 불가
        
        _inGameCardComponent[_curDartIdx].DeselectCard();
        _curDartIdx = dartIdx;
        _inGameCardComponent[_curDartIdx].SelectCard();
        _curDartComponentList.Last().Setup(_curDartIdx);
    }
    public void TouchEnd()
    {
        _curDartComponentList.Last().ShootDart();
        SpawnDart(DartTypes[_curDartIdx], inGameUI.playPanel.transform.position +
                                Vector3.down * (inGameUI.ScreenWidth * inGameUI.PlayPanelRatio / 2));
    }
    public void DartDestroy()
    {
        StopCoroutine(nameof(OnDartCollision));
        _curDartComponentList.Last().OnDie();
        curDartList.RemoveAt(curDartList.Count - 1);
        _curDartComponentList.RemoveAt(curDartList.Count - 1);
    }
}
