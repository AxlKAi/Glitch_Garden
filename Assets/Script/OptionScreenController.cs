using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionScreenController : MonoBehaviour
{     
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider difficultySlider;

    // Start is called before the first frame update
    void Start()
    {
        GetVolumeSliderParametrs();
        GetDifficultySliderParams();
    }

    private void GetDifficultySliderParams()
    {
        difficultySlider.maxValue = PlayerPrefsController.MAX_DIFFICULTY;
        difficultySlider.minValue = PlayerPrefsController.MIN_DIFFICULTY;
        difficultySlider.value = PlayerPrefsController.GetDifficulty();
    }

    private void GetVolumeSliderParametrs()
    {
        volumeSlider.maxValue = PlayerPrefsController.MAX_VOLUME;
        volumeSlider.minValue = PlayerPrefsController.MIN_VOLUME;
        volumeSlider.value = PlayerPrefsController.GetMasterVolume();
    }

    public void SaveVolume(float volume)
    {
        PlayerPrefsController.SetMasterVolume(volume);
    }
    public void SaveDifficulty(float difficulty)
    {
        PlayerPrefsController.SetDifficulty(difficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
