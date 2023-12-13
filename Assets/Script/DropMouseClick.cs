using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMouseClick : MonoBehaviour
{
    private DefenderMenu[] defenderButtons;
    private SpellBarButton[] spellButtons;
    private DefenderBuildZone defenderBuildZone;
    private GameState gameState;

    private void Start()
    {
        FindAllDefenderButtons();
        FindAllSpellButtons();
        defenderBuildZone = FindObjectOfType<DefenderBuildZone>();
        gameState = GameState._instance; 
    }

    public void DropClick()
    {

    }

    private void FindAllDefenderButtons()
    {
        defenderButtons = Resources.FindObjectsOfTypeAll(typeof(DefenderMenu)) as DefenderMenu[];
    }

    private void FindAllSpellButtons()
    {
        spellButtons = Resources.FindObjectsOfTypeAll(typeof(SpellBarButton)) as SpellBarButton[];
    }

    public void TurnOffDefenderButtons()
    {
        foreach (var button in defenderButtons)
        {
            button.IsDisabled = true;
        }
    }

    public void TurnOnDefenderButtons()
    {
        foreach (var button in defenderButtons)
        {
            button.IsDisabled = false;
        }
    }

    public void TurnOffSpellButtons()
    {
        foreach (var button in spellButtons)
        {
            button.IsDisabled = true;
        }
    }

    public void TurnOnSpellButtons()
    {
        foreach (var button in spellButtons)
        {
            button.IsDisabled = false;
        }
    }

    public void TurnOffBattlefield()
    {
        defenderBuildZone.SetGameOnPause();
    }

    public void TurnOnBattlefield()
    {
        defenderBuildZone.SetGameNormalSpeed();
    }

    public void TurnOffAllElements()
    { 
        TurnOffDefenderButtons();
        TurnOffSpellButtons();
        TurnOffBattlefield();
    }

    public void TurnOnAllElements()
    {
        if (gameState.IsModalWindowExist())
            return;

        TurnOnDefenderButtons();
        TurnOnSpellButtons();
        TurnOnBattlefield();
    }
}
