using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spell
{
    public int id;  // id = 0 means NULL
    public string name;
    public Sprite icon;
    public Sprite mouseCursor;
    public ParticleSystem particle;
    public int spellPower;
    public float spellRange;
    public float spellTime;

    public Spell(Spell d)
    {
        this.icon = d.icon;
        this.name = d.name;
        this.id = d.id;
        this.mouseCursor = d.mouseCursor;
    }
}
