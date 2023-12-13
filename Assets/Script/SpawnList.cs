using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config List")]
public class SpawnList : ScriptableObject
{
    [SerializeField]
    public SpawnItems[] spawnList;
}
