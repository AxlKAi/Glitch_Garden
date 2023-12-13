using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelsMapController : MonoBehaviour
{
    [SerializeField] private LevelsMapModel _levelsMapModel;
    [SerializeField] private UnityEvent _slideLeft;
    [SerializeField] private UnityEvent _slideRight;

    private LevelsMapView _levelsMapView;

    public event UnityAction SlideLeft
    {
        add => _slideLeft.AddListener(value);
        remove => _slideLeft.RemoveListener(value);
    }

    public event UnityAction SlideRight
    {
        add => _slideRight.AddListener(value);
        remove => _slideRight.RemoveListener(value);
    }

    private void Awake()
    {
        _levelsMapView = GetComponent<LevelsMapView>();
    }
    void Start()
    {
        LoadLevelsData();
    }

    private void LoadLevelsData()
    {
        bool isLevelButtonInteractable = true;        

        _levelsMapModel.LoadLevelsFromPrefs();
        foreach (var icon in _levelsMapModel.LevelsIcon)
        {
            var button = _levelsMapView.CreateIconScroolView(icon);

            if(isLevelButtonInteractable == false)
                button.Deactivate();

            if (icon.LevelCompletion == 0 && icon.LevelIndex > PlayerPrefsController.TUTORIAL_LEVELS_COUNT)
                isLevelButtonInteractable = false;
        }        
    }

    public void StartLevel(LevelIcon level)
    {
        LevelManager.LoadScene(level.LevelIndex); 
    }

}
