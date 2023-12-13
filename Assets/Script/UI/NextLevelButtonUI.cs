﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelButtonUI : MonoBehaviour
{
    LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnMouseDown()
    {
        levelManager.LoadNextScene();
    }
}
