using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpawnebleStarItem")]
public class StarSpawnItem : ScriptableObject
{
    public Vector3 position;
    public int amount;
    public float delay;

}
