using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PistolData", menuName = "SOs/PistolData")]
public class PistolData : ScriptableObject
{
    [field: SerializeField, Range(0,100)] public float damage;
    [field: SerializeField, Range(0, 5)] public float attackSpeed;
    [field: SerializeField, Range(0, 1)] public float perShotRecoil;
    [field: SerializeField, Range(0, 1)] public float accuracyRecoverySpeed;
    [field: SerializeField, Range (0, 1)] public float maxInaccuracy;
}
