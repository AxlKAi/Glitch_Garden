using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SpawnebleStarItemList")]
public class StarSpawnItemList : ScriptableObject
{
    [SerializeField]
    public List<StarSpawnItem> starSpawnItemList;
}
