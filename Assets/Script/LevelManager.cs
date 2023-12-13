using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public const int LevelsCount = 12;
    public const int FirstLevelIndexOffset = 2;

    [SerializeField] int delayBeforeLoadScene = 3;
    int sceneIndex;
    
    LevelUI levelUI;

    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 0)
        {
            LoadMainMenuWithDelay();
        }

        if (sceneIndex > FirstLevelIndexOffset)
        {
            EnableLevelUI();
        }
    }

    private void EnableLevelUI()
    {
        levelUI = FindObjectOfType<LevelUI>();
        string levelInfo = "Level " + (sceneIndex - FirstLevelIndexOffset).ToString();
        levelUI.SetLevelIntroText(levelInfo);
    }

    public void LoadMainMenuWithDelay()
    {
        StartCoroutine(LoadStartScreenCorout());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    IEnumerator LoadStartScreenCorout()
    {
        yield return new WaitForSeconds(delayBeforeLoadScene);
        LoadMainMenu();        
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadOptionScreen()
    {
        SceneManager.LoadScene("OptionScreen");
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public static void LoadScene(int index)
    {
        index = (int)Mathf.Clamp(index + FirstLevelIndexOffset, FirstLevelIndexOffset + 1, LevelsCount + FirstLevelIndexOffset);
        SceneManager.LoadScene(index);
    }

    public int CurrentLevelIndex()
    {
        return (int)Mathf.Clamp(sceneIndex - FirstLevelIndexOffset, 1, LevelsCount);
    }
}
