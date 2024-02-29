using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeed : MonoBehaviour
{
    const int MaxGameSpeed = 8;

    [SerializeField]
    private NormalSpeedButtonUI _normalSpeedButtonUI;
    [SerializeField]
    private DoubleSpeedButtonUI _doubleSpeedButtonUI;

    private Color _activeButton = new Color(.6f, .6f, .6f);
    private Color _regularButton = new Color(.2f,.2f,.2f);
    private Color _activeSpeedIndicator = new Color(.75f, .59f, .27f);
    private float _regularSpeedIndicatorColorMultiplicator = 1.3f;

    private TextMeshProUGUI _speedIndicator;
    private string _speedSuffix = "x";

    public void SetNormalGameSpeed()
    {
        Time.timeScale = 1;
        SetButtonsStateToDefault();
    }

    public void SetDoubleGameSpeed()
    {
        SetButtonsStateToDefault();
        _normalSpeedButtonUI.gameObject.GetComponent<Image>().color = _activeButton;

        if ((int)Time.timeScale < MaxGameSpeed)
            Time.timeScale = Time.timeScale * 2;

        _speedIndicator.text = (int)Time.timeScale + _speedSuffix;
        _speedIndicator.color = _activeSpeedIndicator;
    }

    public void SetZeroGameSpeed()
    {
        Time.timeScale = 0;
    }

    private void Start()
    {
        _normalSpeedButtonUI = FindObjectOfType<NormalSpeedButtonUI>();
        _doubleSpeedButtonUI = FindObjectOfType<DoubleSpeedButtonUI>();
        _speedIndicator = _doubleSpeedButtonUI.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        SetButtonsStateToDefault();
        SetNormalGameSpeed();
    }

    private void SetButtonsStateToDefault()
    {
        _normalSpeedButtonUI.gameObject.GetComponent<Image>().color = _regularButton;
        _doubleSpeedButtonUI.gameObject.GetComponent<Image>().color = _activeButton;
        _speedIndicator.color = _activeSpeedIndicator/_regularSpeedIndicatorColorMultiplicator;
        _speedIndicator.text = "1" + _speedSuffix;
    }
}
