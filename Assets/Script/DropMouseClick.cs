using UnityEngine;
using UnityEngine.UI;

public class DropMouseClick : MonoBehaviour
{
    private DefenderMenu[] defenderButtons;
    private SpellBarButton[] spellButtons;
    private DefenderBuildZone defenderBuildZone;
    private GameState gameState;
    private NormalSpeedButtonUI _normalSpeedButtonUI;
    private DoubleSpeedButtonUI _doubleSpeedButtonUI;
    private OptionButtonUI _optionButtonUI;


    private void Start()
    {
        FindAllDefenderButtons();
        FindAllSpellButtons();
        defenderBuildZone = FindObjectOfType<DefenderBuildZone>();
        gameState = GameState._instance;
        _normalSpeedButtonUI = FindObjectOfType<NormalSpeedButtonUI>();
        _doubleSpeedButtonUI = FindObjectOfType<DoubleSpeedButtonUI>();
        _optionButtonUI = FindObjectOfType<OptionButtonUI>();
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
        TurnOffGameSpeedButtons();
    }

    public void TurnOnAllElements()
    {
        if (gameState.IsModalWindowExist())
            return;

        TurnOnDefenderButtons();
        TurnOnSpellButtons();
        TurnOnBattlefield();
        TurnOnGameSpeedButtons();
    }

    private void TurnOffGameSpeedButtons()
    {
        _doubleSpeedButtonUI.GetComponent<Button>().interactable = false;
        _normalSpeedButtonUI.GetComponent<Button>().interactable = false;
        _optionButtonUI.GetComponent<Button>().interactable = false;
    }
    private void TurnOnGameSpeedButtons()
    {
        _doubleSpeedButtonUI.GetComponent<Button>().interactable = true;
        _normalSpeedButtonUI.GetComponent<Button>().interactable = true;
        _optionButtonUI.GetComponent<Button>().interactable = true;
    }
}
