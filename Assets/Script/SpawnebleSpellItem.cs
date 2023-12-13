using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "SpawnebleSpellItem")]
public class SpawnebleSpellItem : ScriptableObject
{
    public int spell_id;
    public float delay;
    public Vector3 position;

    public SpawnebleSpellItem(int id, float delay, Vector3 position)
    {
        spell_id = id;
        this.delay = delay;
        this.position = position;
    }
}
