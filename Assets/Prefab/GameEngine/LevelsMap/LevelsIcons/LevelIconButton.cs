using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIconButton : MonoBehaviour
{
    [SerializeField] private Image _icon;
    public Image Icon => _icon;
    
    [SerializeField] private Text _title;    
    public Text Title => _title;

    private Button _button;
    public Button GetButton => _button;
    
    private LevelIcon _levelIcon;

    [SerializeField] [Range(.0f,1f)] private float HightlightedAlphaColor = 1f;
    [SerializeField] private Image _easyStar;
    public Image EasyStar => _easyStar;

    [SerializeField] private Image _normalStar;
    public Image NormalStart => _normalStar;

    [SerializeField] private Image _hardStar;
    public Image HardStar => _hardStar;

    [SerializeField] private Color _deactivateColor = Color.grey;

    public void InitializeButton(LevelIcon levelIcon)
    {
        _levelIcon = levelIcon;
        _button = GetComponent<Button>();
        _icon.sprite = levelIcon.Icon;
        _title.text = levelIcon.Title;
        ShowDifficultyStars();
    }

    public void Deactivate()
    {
        _button.interactable = false;
        _icon.color = _deactivateColor;
        _title.color = _deactivateColor;
    }

    private void ShowDifficultyStars()
    {
        switch (_levelIcon.LevelCompletion)
        {
            case 3: HightlightStar(_hardStar); goto case 2;
            case 2: HightlightStar(_normalStar); goto case 1;
            case 1: HightlightStar(_easyStar); break;
            case 0: break;
        }
    }

    private void HightlightStar(Image image)
    {
        var color = image.color;
        color.a = HightlightedAlphaColor;
        image.color = color;
    }
}
