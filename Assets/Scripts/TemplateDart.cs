using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class TemplateDart : ScriptableObject
{
    public GameObject dartPrefab;
    public Dart[] darts;
    
    [System.Serializable]
    public struct Dart
    {
        public Sprite cardImageSprite;
        public Sprite dartImageSprite;
        public int damage;
        public int range;

    }
}
