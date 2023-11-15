using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private int _targetIdx;
    private int _targetHp;

    public int TargetHp
    {
        get => _targetHp;
        set => _targetHp = Mathf.Max(value, 0);
    }
    public int TargetIdx
    {
        get => _targetIdx;
        set => _targetIdx = value;
    }
    
    public bool TargetLoseHp(int dartAttack)
    {
        _targetHp -= dartAttack;
        if (_targetHp < 0)
        {
            return false;
        }
        else if (_targetHp == 0)
        {
            
        }

        return true;
    }
    
}
