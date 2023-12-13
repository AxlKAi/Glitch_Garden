using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonUI : MonoBehaviour
{
    private GameState _gameState;

    private void Awake()
    {
        _gameState = GameState._instance;
    }

    public void WinLevel()
    {
        _gameState.TestWinMethod();
    }
}
