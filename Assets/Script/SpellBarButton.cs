using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBarButton : MonoBehaviour
{
    Spell spell;
    [SerializeField] int spell_id;
    [SerializeField] private int inventoryNum;
    private Inventory inventory;

    GameState gameState;

    private bool isDisabled = false;
    public bool IsDisabled
    {
        get
        {
            return isDisabled;
        }

        set
        {
            isDisabled = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.FindObjectOfType<GameState>();
        inventory = GameObject.FindObjectOfType<Inventory>();

        spell = gameState.GetSpell(spell_id);
        GetComponent<SpriteRenderer>().sprite = spell.icon;
    }

    private void OnMouseDown()
    {
        if (!isDisabled)
        {
            gameState.SpellModeOn(spell);
            inventory.SetSelectedItem(this);
        }
    }

    public void SetSpellId(int spell_id)
    {
        if(spell != null)
        {
            spell = gameState.GetSpell(spell_id);
        }
        this.spell_id = spell_id;
    }

    public int GetItemIndex()
    {
        return inventoryNum;
    }
}
