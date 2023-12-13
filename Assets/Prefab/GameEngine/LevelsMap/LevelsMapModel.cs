using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelsMapModel")]
public class LevelsMapModel : ScriptableObject
{
    [SerializeField] private LevelIcon[] _levelsIcon;
    public LevelIcon[] LevelsIcon => _levelsIcon;

    private int _levelsCount;
    public int LevelsCount => _levelsCount;

    public void LoadLevelsFromPrefs()
    {
        _levelsCount = _levelsIcon.Length;
        foreach( LevelIcon levelIcon in _levelsIcon )
        {
            levelIcon.LoadLevelCompletionFromPrefs();
        }
    }

    public int GetLevelCoplition(int levelIndex)
    {
        return _levelsIcon[levelIndex].GetLevelCompletion();
    }
}
