using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    Sprite sprite;
    Defender defenderPref;
    GameState gameState;
    private bool isBuildMode;
    private bool isSpellMode;
    Spell spell;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.FindObjectOfType<GameState>();
        gameState.BuildModeOnEvent += BuildModeOn;
        isBuildMode = gameState.GetBuildMode();
        isSpellMode = gameState.GetSpellMode();
        gameState.BuildModeOffEvent += HideBuildMouseCursor;
        gameState.SpellModeOnEvent += SetSpellModeOn;
        gameState.SpellModeOffEvent += SetSpellModeOff;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuildMode || isSpellMode)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
        }
    }

    public void BuildModeOn(Defender def)
    {
        this.defenderPref = def;
        isBuildMode = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = defenderPref.GetBodySprite();
    }

    public void HideBuildMouseCursor()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        isBuildMode = false;
    }

    public void SetSpellModeOn(Spell spell)
    {
        this.isSpellMode = true;
        this.spell = spell;
        this.sprite = spell.mouseCursor;
        gameObject.GetComponent<SpriteRenderer>().sprite = this.sprite;
    }

    public void SetSpellModeOff()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        this.isSpellMode = false;
        this.spell = null;
        sprite = null;
    }
}
