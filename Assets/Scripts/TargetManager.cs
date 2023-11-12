using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : Singleton<TargetManager>
{
    [SerializeField] private GameObject playScreenPanel;
    [SerializeField] private GameObject targetPrefab;
    
    
    private GameObject[] _targets = new GameObject[8];
    private Target[] _targetComponents = new Target[8]; // target : 8개
    private int _stageLevel;

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
            
            clone.transform.localScale = new Vector3(targetHalfSize * 2, targetHalfSize * 2, 0);
            _targetComponents[i] = clone.GetComponent<Target>();
        }
    }
    public void SetTargetHP(int curStageLevel) // 합 = StageLevel이 되도록 8개의 칸에 HP 배분
    {
        _stageLevel = curStageLevel;
        int[] tmpHP = new int[8];
        while (true)
        {
            int sum = 0;
            for (int i = 0; i < tmpHP.Length; i++)
            {
                tmpHP[i] = Random.Range(0, _stageLevel * 2);
                sum += tmpHP[i];
            }

            if (sum != _stageLevel) continue;
            
            for (int i = 0; i < tmpHP.Length; i++)
            {
                _targetComponents[i].TargetHP = tmpHP[i];
            }
        }
    }

    public void DamageTarget(int dartAttack, int dartRange)
    {
        
    }
}
