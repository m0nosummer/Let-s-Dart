using System;
using System.Collections;
using System.Collections.Generic;
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
    private int _stageLevel;
    private bool _isGameOver;

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
        int[] tmpHp = new int[8];
        while (true)
        {
            int sum = 0;
            for (int i = 0; i < tmpHp.Length; i++)
            {
                tmpHp[i] = Random.Range(0, _stageLevel * 2);
                sum += tmpHp[i];
            }

            if (sum == _stageLevel) break;
        }

        for (int i = 0; i < tmpHp.Length; i++)
        {
            _targetComponents[i].TargetHp = tmpHp[i];
            textTargetHps[i].GetComponent<TextMeshProUGUI>().text = tmpHp[i].ToString();
            textTargetHps[i].GetComponent<TextMeshProUGUI>().fontSize = Screen.width / 16;
            textTargetHps[i].SetActive(true);
        }
    }

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
    }
}
