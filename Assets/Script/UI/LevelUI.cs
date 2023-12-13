using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    TextMeshProUGUI levelTMPref;
    float timeToShowLevelInfo = 3.5f;
    float timeRemain;
    int secondsToStartGame;
    [SerializeField] private TMPro.TextMeshProUGUI timeUI;
    GameState gameState;
    private int waveNum = 1;

    [SerializeField] private bool _startAfterTutorial = false;
    

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        timeRemain = timeToShowLevelInfo;
        levelTMPref = GetComponent<TextMeshProUGUI>();
        levelTMPref.enabled = true;
        levelTMPref.gameObject.GetComponentInChildren<UnityEngine.UI.Image>().enabled = true;
        if (!_startAfterTutorial)
            SetActiveTimeUI();
    }

    private void SetActiveTimeUI()
    {
        timeUI.transform.gameObject.SetActive(true);
    }

    public void StartAfterTutorial()
    {
        _startAfterTutorial = false;
        SetActiveTimeUI();
    }

    private void ShowInfo()
    {
        if (!_startAfterTutorial)
        {
            timeRemain -= Time.deltaTime;
            secondsToStartGame = (int)timeRemain;
            timeUI.text = "0" + secondsToStartGame.ToString();
        }
    }

    private void FixedUpdate()
    {
        ShowInfo();

        if (timeRemain <= 0)
        {
            HideLevelInfo();
            gameState.StartWave(waveNum);
        }
    }

    public void SetLevelIntroText(string text)
    {
        levelTMPref.text = text;
    }

    public void HideLevelInfo()
    {
        gameObject.SetActive(false);
    }

    public void ShowWave(int wave_num)
    {
        waveNum = wave_num;
        timeRemain = timeToShowLevelInfo;
        levelTMPref.text = "Wave " + wave_num.ToString();
        gameObject.SetActive(true);
    }
}
