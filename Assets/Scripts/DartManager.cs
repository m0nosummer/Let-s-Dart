using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DartManager : Singleton<DartManager>
{
    [SerializeField] private TemplateDart templateDart;

    private GameObject[] _darts = new GameObject[5]; // 다트 덱 구성
    private int _dartType;
    private int _allDartsCnt;
    private int _curDartIdx = 0;
    private bool[] _isSelectedDart;
    private void SelectDart() // 게임 시작 시 고정 다트 1개와 랜덤 다트 4개를 선택
    {
        _allDartsCnt = templateDart.darts.Length;
        _darts[0] = templateDart.darts[0].dartPrefab;
        for (int i = 1; i < 5; i++)
        {
            int randomIdx;
            while (true)
            {
                randomIdx = Random.Range(1, _allDartsCnt);
                if (!_isSelectedDart[randomIdx]) break;
            }
            _darts[i] = templateDart.darts[randomIdx].dartPrefab;
            _isSelectedDart[randomIdx] = true;
        }
        //TODO : 카드에 다트 정보 갱신
    }
    private void RerollSelectedDart() // 랜덤 다트 4개 중 선택한 다트 다시 뽑기 가능
    {
        
    }
    private void SpawnDart(Vector3 spawnPosition, int dartType)
    {
        Vector3 position = spawnPosition + Vector3.back;
        
        GameObject clone = Instantiate(templateDart.darts[_dartType].dartPrefab, position, Quaternion.identity);
        clone.GetComponent<Dart>().DartType = dartType;

    }

    private void ChangeDart(int dartIdx) // 다트 버튼을 선택하여 바꾸기
    {
        _curDartIdx = dartIdx;
        

    }
}
