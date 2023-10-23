using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DartManager : Singleton<DartManager>
{
    [SerializeField] private TemplateDart templateDart;

    private GameObject[] _darts = new GameObject[5]; // 다트 덱 구성
    private int[] _dartType = new int[5];
    private int _allDartsCnt;
    private int _curDartIdx = 0;
    private bool[] _isUsedIdx = new bool[20];
    private bool[] _isSelected = new bool[5];
    private void SetDart() // 게임 시작 시 고정 다트 1개와 랜덤 다트 4개를 선택
    {
        _allDartsCnt = templateDart.darts.Length;
        _darts[0] = templateDart.darts[0].dartPrefab;
        _dartType[0] = 0;
        for (int i = 1; i < 5; i++)
        {
            int curDartIdx;
            while (true)
            {
                curDartIdx = Random.Range(1, _allDartsCnt);
                if (!_isUsedIdx[curDartIdx]) break;
            }
            _isUsedIdx[curDartIdx] = true;
            _dartType[i] = curDartIdx;
            _darts[i] = templateDart.darts[curDartIdx].dartPrefab;
        }
        // TODO : 카드 버튼에 정보 갱신
    }
 
    private void SelectDart(int dartIdx) // 스테이지 시작 시 다트 선택 화면에서 다트 선택 및 해제
    {
        if (_isSelected[dartIdx])
        {
            // TODO : 카드가 내려가는 애니메이션
            _isSelected[dartIdx] = false;
        }
        else
        {
            // TODO : 카드가 올라가는 애니메이션
            _isSelected[dartIdx] = true;
        }
        
    }
    private void RerollSelectedDart() // 랜덤 다트 4개 중 선택한 다트 다시 뽑기 가능
    {
        for (int i = 1; i < 5; i++)
        {
            if (!_isSelected[i]) continue;
            int prevDartIdx = _dartType[i];
            int curDartIdx = 0;
            
            // TODO : 카드 리롤 애니메이션
            
            while (true)
            {
                curDartIdx = Random.Range(1, _allDartsCnt);
                if (!_isUsedIdx[curDartIdx]) break;
            }
            _isUsedIdx[prevDartIdx] = false;
            _isUsedIdx[curDartIdx] = true;
            _dartType[i] = curDartIdx;
            _darts[i] = templateDart.darts[curDartIdx].dartPrefab;
            
            // TODO : 카드 버튼에 정보 갱신
        }
    }
    private void ChangeDart(int dartIdx) // 다트 버튼을 선택하여 바꾸기
    {
        _curDartIdx = dartIdx;
        // TODO : 다트 선택 애니메이션

    }
    private void SpawnDart(int dartType, Vector3 spawnPosition)
    {
        Vector3 position = spawnPosition + Vector3.back;
        GameObject clone = Instantiate(templateDart.darts[dartType].dartPrefab, position, Quaternion.identity);
        clone.GetComponent<Dart>().SetUp(dartType, spawnPosition);

    }
}
