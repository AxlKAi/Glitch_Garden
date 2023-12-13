using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionWindow : MonoBehaviour
{
    [SerializeField] private OptionSwithContent _difficultySwitch;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private OptionSwithContent _isTutorialActiveSwitch;
    [SerializeField] private Text _restartMessageText;
    [SerializeField] private Text _restartMessageTextShadow;
    private int _initialyDifficulty;

    // Start is called before the first frame update
    void Start()
    {
        InitializeDifficultySwitch();
        InitializeVolumeSlider();
        InitializeTutorialSwitch();
        InitializeMessageText();
    }

    private void InitializeMessageText()
    {
        if (_restartMessageText == null || _restartMessageTextShadow == null)
            Debug.LogWarning("text field missing");
        HideRestartMessage();
    }

    private void HideRestartMessage()
    {
        _restartMessageText.text = "";
        _restartMessageTextShadow.text = "";
    }

    private void ShowRestartMessage()
    {
        _restartMessageText.text = "Уровень будет перезапущен";
        _restartMessageTextShadow.text = "Уровень будет перезапущен";
    }

    private void InitializeTutorialSwitch()
    {
        if (_isTutorialActiveSwitch != null)
        { 
            if (PlayerPrefsController.GetIsTutorialActive())
                _isTutorialActiveSwitch.SetActivePosition(0);
            else
                _isTutorialActiveSwitch.SetActivePosition(1);
        }            
        else
            Debug.LogError("Cant find difficultySwitch");
    }

    private void InitializeVolumeSlider()
    {
        if (_volumeSlider != null)
        {
            _volumeSlider.value = PlayerPrefsController.GetMasterVolume();
            _volumeSlider.onValueChanged.AddListener(delegate { ChangeVolumeSettings(); });
        }
        else
            Debug.LogWarning("volume slider is missing");
    }

    private void InitializeDifficultySwitch()
    {
        if (_difficultySwitch != null)
        {
            _initialyDifficulty = (int)PlayerPrefsController.GetDifficulty();
            _difficultySwitch.SetActivePosition(_initialyDifficulty);
        }            
        else
            Debug.LogError("Cant find difficultySwitch");
    }

    public void ChangeVolumeSettings()
    {
        PlayerPrefsController.SetMasterVolume(_volumeSlider.value);
    }

    public void ChangeDifficultySettings()
    {
        var position = _difficultySwitch.CurrentMenuPosition;
        PlayerPrefsController.SetDifficulty(position);
        if (position != _initialyDifficulty)
            ShowRestartMessage();
        else
            HideRestartMessage();
    }

    public void ChangeTutorialSettings()
    {
        if (_isTutorialActiveSwitch.CurrentMenuPosition == 0)
            PlayerPrefsController.SetIsTutorialActive(true);
        else
            PlayerPrefsController.SetIsTutorialActive(false);
    }

    public void RestartButtonClick()
    {
           GameState.levelManager.RestartLevel();
    }

    public void CloseButtonClick()
    {
        if (_difficultySwitch.CurrentMenuPosition != _initialyDifficulty)
            RestartButtonClick();
    }
}
