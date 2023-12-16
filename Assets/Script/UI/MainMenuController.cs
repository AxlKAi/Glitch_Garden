using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private MainMenuState _mainMenuState;

    void Start()
    {
        if (TryGetComponent<MainMenuState>(out MainMenuState state))
        {
            _mainMenuState = state;
        }         
    }

    public void ShowLevelsMap()
    {
        _mainMenuState.SetState(MainMenuState.State.LevelsMap);
    }

    public void ShowMainButtons()
    {
        _mainMenuState.SetState(MainMenuState.State.MainButtons);
    }

    public void ShowOptionWindow()
    {
        _mainMenuState.SetState(MainMenuState.State.Options);
    }
}
