using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : Singleton<TargetManager>
{
    [SerializeField] private Target[] targets; // target : 8개

    private int _stageLevel;

    private void SetTargetHP() // 합 = StageLevel이 되도록 8개의 칸에 HP 배분
    {
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
                targets[i].TargetHP = tmpHP[i];
            }
        }
    }

    public void DamageTarget(int dartAttack, int dartRange)
    {
        
    }
}
