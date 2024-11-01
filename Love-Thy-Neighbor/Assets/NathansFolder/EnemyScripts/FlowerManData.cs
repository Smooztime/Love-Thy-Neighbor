using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FlowerManData", menuName = "SOs/FlowerManData")]
public class FlowerManData : ScriptableObject
{
    [field: SerializeField, Range(0, 1000)] public float health;
    [field: SerializeField, Range(0, 100)] public float speed;
    [field: SerializeField, Range(0, 1000)] public float damage;
}

