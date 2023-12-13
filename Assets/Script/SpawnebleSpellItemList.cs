using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpawnebleSpellItemsList")]
public class SpawnebleSpellItemList : ScriptableObject
{
    [SerializeField]
    public List<SpawnebleSpellItem> spawnebleSpellItemList;
}
