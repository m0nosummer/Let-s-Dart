using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TemplateDart : ScriptableObject
{
    public Dart[] darts;
    
    [System.Serializable]
    public struct Dart
    {
        public GameObject dartPrefab;
        public Sprite sprite; // 다음 다트 표시 버튼에 사용
        public int damage;
        public int range;

    }
}
