using UnityEngine;

public class LevelsMapView : MonoBehaviour
{
    [SerializeField] private GameObject _scroolViewContent;
    [SerializeField] private LevelIconButton _levelIconViewPrefab;
    
    private LevelsMapController _levelsMapController;
    public LevelsMapController Controller { get { return _levelsMapController; } }

    private void Awake()
    {
        _levelsMapController = GetComponent<LevelsMapController>();
    }

    public LevelIconButton CreateIconScroolView(LevelIcon levelIcon)
    {
        var button = Instantiate(
                        _levelIconViewPrefab, 
                        new Vector3(0, 0, 0), 
                        Quaternion.identity, 
                        _scroolViewContent.transform);

        button.InitializeButton(levelIcon);
        button.GetButton.onClick.AddListener(() => _levelsMapController.StartLevel(levelIcon));

        return button;
    }

    private void SlideLeft()
    {
        Debug.Log("left");
    }
}

