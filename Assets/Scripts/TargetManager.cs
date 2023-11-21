using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetManager : Singleton<TargetManager>
{
    [SerializeField] private GameObject playScreenPanel;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private GameObject[] textTargetHps;
    
    
    private GameObject[] _targets = new GameObject[8];
    private Target[] _targetComponents = new Target[8]; // target : 8개
    private int _stageLevel = 1;
    private bool _isGameOver;

    public Target[] TargetComponents
    {
        get => _targetComponents;
    }
    public int StageLevel
    {
        get => _stageLevel;
        set
        {   _stageLevel = value;
            OnStageLevelChanged?.Invoke(value);
        }
    }
    public bool IsGameOver
    {
        get => _isGameOver;
        set
        {   _isGameOver = value;
            OnGameOver?.Invoke(value);
        }
    }
    public event Action<int> OnStageLevelChanged; // _stageLevel 변수 감지
    public event Action<bool> OnGameOver; // _isGameOver 변수 감지

    public void SpawnTargets(float screenWidth, float playPanelRatio) // playPanelRatio : h / w
    {
        float targetHalfSize = screenWidth / 16;
        float panelHeight = screenWidth * playPanelRatio;

        for (int i = 0; i < 8; i++)
        {
            Vector3 offsetPos = new Vector3(targetHalfSize * 2 * i - screenWidth / 2 + targetHalfSize,
                panelHeight / 2 - targetHalfSize, 0);
            Vector3 spawnPos = playScreenPanel.transform.position + offsetPos + Vector3.back;
            
            GameObject clone = Instantiate(targetPrefab, spawnPos, Quaternion.identity);
            textTargetHps[i].transform.position = spawnPos;
            clone.transform.localScale = new Vector3(targetHalfSize * 2, targetHalfSize * 2, 0);
            
            _targetComponents[i] = clone.GetComponent<Target>();
            _targetComponents[i].TargetIdx = i;
        }
    }
    public void SetTargetHp(int curStageLevel) // 합 = StageLevel이 되도록 8개의 칸에 HP 배분
    {
        _stageLevel = curStageLevel;
        
        List<int> targetIdxList = new List<int> {0, 1, 2, 3, 4, 5, 6, 7};
        
        int[] tmpHp = new int[8];
        int totalHpLeft = _stageLevel;
        int curTargetCnt = 1;
        
        ShuffleList(targetIdxList);
        
        for (int i = 0; i < 8; i++)
        {
            int curIdx = targetIdxList[i];
            int maxHpRange = (totalHpLeft > 0) ? totalHpLeft / 4 + 2 : 1;
            
            if (i == 7) tmpHp[curIdx] = totalHpLeft;
            else        tmpHp[curIdx] = Random.Range(0, maxHpRange);
            totalHpLeft -= tmpHp[curIdx];
        }

        for (int i = 0; i < tmpHp.Length; i++)
        {
            _targetComponents[i].TargetHp = tmpHp[i];
            textTargetHps[i].GetComponent<TextMeshProUGUI>().text = tmpHp[i].ToString();
            textTargetHps[i].GetComponent<TextMeshProUGUI>().fontSize = Screen.width / 16;
            textTargetHps[i].SetActive(true);
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    } // Knuth Shuffle
    public void DamageTarget(int curTargetIdx, int dartAttack, int dartRange)
    {
        int range = (dartRange - 1) / 2;
        for (int i = curTargetIdx - range; i <= curTargetIdx + range; i++)
        {
            if (i < 0 || i > 7) continue;
            if (!_targetComponents[i].TargetLoseHp(dartAttack)) // 타겟 HP가 0보다 더 떨어짐
            {
                IsGameOver = true;
            }
        }

        for (int i = 0; i < 8; i++)
        {
            if (_targetComponents[i].TargetHp > 0) return;
        }

        StageLevel += 1;
    }
}
