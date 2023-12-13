using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string MASTER_VOLUME_KEY = "master volume";
    const string DIFFICULTY_KEY = "difficulty";
    const string IS_TUTORIAL_ACTIVE_KEY = "tutorial_active";

    public const float MAX_VOLUME = 1f;
    public const float MIN_VOLUME = 0f;
    public const float DEFAULT_VOLUME = 0.5f;

    public const float MIN_DIFFICULTY = GameState.difficulty_min_level;
    public const float MAX_DIFFICULTY = GameState.difficulty_max_level;
    public const float DEFAULT_DIFFICULTY = GameState.difficulty_default;

    public const int TUTORIAL_LEVELS_COUNT = 3;

    public const bool DEFAULT_IS_TUTORIAL_ACTIVE = true;

    const string LEVEL_NAME_PREFIX = "level_";

    private void Start()
    {
        FirstGameStartInitializeSettings();   
    }

    private void FirstGameStartInitializeSettings()
    {
        if (!PlayerPrefs.HasKey(MASTER_VOLUME_KEY) || !PlayerPrefs.HasKey(DIFFICULTY_KEY) || !PlayerPrefs.HasKey(IS_TUTORIAL_ACTIVE_KEY))
        {
            SetMasterVolume(DEFAULT_VOLUME);
            SetDifficulty(DEFAULT_DIFFICULTY);
            SetIsTutorialActive(DEFAULT_IS_TUTORIAL_ACTIVE);

            for(int i=1; i<=LevelManager.LevelsCount; i++)
            {
                SaveLevelCompletition(i, 0);
            }
        }
    }

    public static void SetMasterVolume(float volume)
    {
        if ( (volume <= MAX_VOLUME) && (volume >= MIN_VOLUME) )
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Volume is greater or lower than mast be.");
        }
    }

    public static float GetMasterVolume()
    {
        return (PlayerPrefs.GetFloat(MASTER_VOLUME_KEY));
    }

    public static void SetDifficulty(float difficulty)
    {
        if ((difficulty <= MAX_DIFFICULTY) && (difficulty >= MIN_DIFFICULTY))
        {
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
        }
        else
        {
            Debug.LogError("Volume is greater or lower than mast be.");
        }
    }

    public static float GetDifficulty()
    {
        return (PlayerPrefs.GetFloat(DIFFICULTY_KEY));
    }

    public static void SetIsTutorialActive(bool state)
    {
        if (state)
            PlayerPrefs.SetInt(IS_TUTORIAL_ACTIVE_KEY, 1);
        else
            PlayerPrefs.SetInt(IS_TUTORIAL_ACTIVE_KEY, 0);
    }

    public static bool GetIsTutorialActive()
    {
        return (PlayerPrefs.GetInt(IS_TUTORIAL_ACTIVE_KEY) >= 1);
    }

    public static void SaveLevelCompletition(int levelIndex, int difficulty)
    {
        if(levelIndex < GetLevelCompletition(levelIndex))
            PlayerPrefs.SetFloat(LEVEL_NAME_PREFIX+levelIndex, difficulty);
    }

    public static int GetLevelCompletition(int levelIndex)
    {
        string levelName = LEVEL_NAME_PREFIX + levelIndex;
        int levelComplitionIndex = 0;

        if (PlayerPrefs.HasKey(levelName))
        {
            levelComplitionIndex = (int)PlayerPrefs.GetFloat(LEVEL_NAME_PREFIX + levelIndex);
            Debug.Log("The key " + levelName + " exists, it equals " + levelComplitionIndex);
        }
        else
        {
            // TODO delete this random level complition index
            /*
            levelComplitionIndex = (int)Random.Range(0f, 3.99f);
            Debug.Log("Generate random level complition index");

            SaveLevelCompletition(levelIndex, levelComplitionIndex);
            Debug.Log("The key " + levelIndex + " does not exist. I create one = " + levelComplitionIndex);
            */
        }            

        return levelComplitionIndex;
    }
}

