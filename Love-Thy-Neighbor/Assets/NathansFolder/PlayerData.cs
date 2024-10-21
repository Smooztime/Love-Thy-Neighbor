using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "SOs/PlayerData")]
public class PlayerData : ScriptableObject
{
    [field: SerializeField, Range(0,100)] public float moveSpeed;
    [field: SerializeField, Range(0,50)] public float lookSens;
    [field: SerializeField, Range(0,1000)] public int maxHealth;

}
