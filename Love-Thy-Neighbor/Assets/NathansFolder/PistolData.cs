using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PistolData", menuName = "SOs/PistolData")]
public class PistolData : ScriptableObject
{
    [field: SerializeField, Range(0, 100)] public float Damage { get; private set; }
    [field: SerializeField, Range(0, 5)] public float AttackSpeed { get; private set; }
    [field: SerializeField, Range(0, 1)] public float PerShotRecoil { get; private set; }
    [field: SerializeField, Range(0, 1)] public float AccuracyRecoverySpeed { get; private set; }
    [field: SerializeField, Range (0, 1)] public float MaxInaccuracy { get; private set; }
    [field: SerializeField, Range(0, 360)] public float PerShotKick { get; private set; }
    [field: SerializeField, Range(0, 50)] public float MaxKick { get; private set; }
    [field:SerializeField] public float ReloadSpeed { get; private set; }
    [field:SerializeField] public float KickRecoverySpeed { get; private set; }
    [field:SerializeField] public int MaxAmmo { get; private set; }
    [field:SerializeField] public int ShotgunPellets { get; private set; }
    [field:SerializeField] public float ShotgunSpread { get; private set; }
    [field:SerializeField] public bool ShotGunMode { get; private set; }
    [field:SerializeField] public bool FullAutoMode { get; private set; }
}
