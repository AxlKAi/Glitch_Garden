using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (menuName = "LevelIcon")]
public class LevelIcon : ScriptableObject
{
    [SerializeField] Sprite _icon;
    public Sprite Icon => _icon;

    [TextArea(1, 15)]
    [SerializeField] string _title;
    public string Title => _title;

    private int _levelCompletion;
    public int LevelCompletion => _levelCompletion;

    [SerializeField]
    private int _levelIndex;
    public int LevelIndex => _levelIndex;

    public void LoadLevelCompletionFromPrefs()
    {
        _levelCompletion = PlayerPrefsController.GetLevelCompletition(_levelIndex);
    }

    public int GetLevelCompletion()
    {
        return _levelCompletion;
    }
}
