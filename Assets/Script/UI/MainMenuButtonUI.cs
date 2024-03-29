﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonUI : MonoBehaviour
{

    GameState _gameState;
    LevelManager _levelManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameState = FindObjectOfType<GameState>();
        _levelManager = FindObjectOfType<LevelManager>();
    }

    public void LoadMainMenu()
    {
        AudioManager.Instance.PlaySFX("UI_Click");
        _gameState.GameSpeed.SetNormalGameSpeed();
        _levelManager.LoadMainMenu();
    }
}
