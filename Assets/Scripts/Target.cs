using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private int _targetHP;

    public int TargetHP
    {
        get => _targetHP;
        set => _targetHP = Mathf.Max(value, 0);
    }
    
    public bool OnDartCollision(int dartAttack)
    {
        _targetHP -= dartAttack;
        if (_targetHP < 0)
        {
            return false;
        }
        else if (_targetHP == 0)
        {
            
        }

        return true;
    }
    
}
