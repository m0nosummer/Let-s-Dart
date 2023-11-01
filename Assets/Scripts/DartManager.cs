using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DartManager : Singleton<DartManager>
{
    [SerializeField] private InGameUI inGameUI;
    [SerializeField] private TemplateDart templateDart;
    [SerializeField] private GameObject[] startCards = new GameObject[4];

    private const int AllDartsCnt = 20;

    private GameObject _curDart; 
    private readonly int[] _dartType = new int[4];
    private int _curDartIdx = 0;
    private readonly bool[] _isUsedType = new bool[20];
    private readonly bool[] _isSelected = new bool[4];
    public void SetDart() // 게임 시작 시 고정 다트 1개와 랜덤 다트 4개를 선택
    {
        _isUsedType[0] = true;
        _dartType[0] = 0;
        startCards[0].GetComponent<Image>().sprite = templateDart.darts[0].sprite;
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
            startCards[i].GetComponent<Image>().sprite = templateDart.darts[i].sprite;
        }
        // TODO : 카드 버튼에 정보 갱신
    }
    public void RerollSelectedDart() // 랜덤 다트 4개 중 선택한 다트 다시 뽑기 가능
    {
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
            startCards[i].GetComponent<Image>().sprite = templateDart.darts[i].sprite;

            // TODO : 카드 버튼에 정보 갱신
        }
    }
    public void SpawnDart(int dartType, Vector3 spawnPosition)
    {
        Vector3 position = spawnPosition + Vector3.back;
        _curDart = Instantiate(templateDart.dartPrefab, position, Quaternion.identity);
        StartCoroutine(nameof(ChangeDart));
    }
    public IEnumerator ChangeDart(int dartIdx) // 다트 버튼을 선택하여 바꾸기
    {
        inGameUI.inGameCardUI[_curDartIdx].GetComponent<Card>().DeselectCard(_curDartIdx);
        _curDartIdx = dartIdx;
        inGameUI.inGameCardUI[_curDartIdx].GetComponent<Card>().SelectCard(_curDartIdx);
        _curDart.GetComponent<Dart>().Setup(_curDartIdx);
        yield return null;
    }

    public void DartDestroy()
    {
        StopCoroutine(nameof(ChangeDart));
    }
}
