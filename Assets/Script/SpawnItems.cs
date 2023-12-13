using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Spawn Unit")]
public class SpawnItems : ScriptableObject
{
    [SerializeField] public Attacker attackerPrefab;
    [SerializeField] public int count;

    [Tooltip("Delay in seconds. Period, after witch mob will spawn")]
    [SerializeField] public float delay;
}
