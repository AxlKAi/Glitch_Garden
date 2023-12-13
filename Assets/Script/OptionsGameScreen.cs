using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OptionsGameScreen : MonoBehaviour
{
    GameState gameState;
    [SerializeField] private UnityEvent _activateOptionWindow;
    [SerializeField] public UnityEvent _deactivateOptionWindow;
    [SerializeField] private GameObject _optionWindowPrefab;

    private DropMouseClick dropMouseClick;

    private GameObject optionWindow;

    public event UnityAction ActivateOptionWindow
    {
        add => _activateOptionWindow.AddListener(value);
        remove => _activateOptionWindow.RemoveListener(value);
    }

    public event UnityAction DeactivateOptionWindow
    {
        add => _deactivateOptionWindow.AddListener(value);
        remove => _deactivateOptionWindow.RemoveListener(value);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.FindObjectOfType<GameState>();

        ActivateOptionWindow +=SetGameSpeedZero;
        DeactivateOptionWindow += DestroyOptionWindow;
        DeactivateOptionWindow += SetGameSpeedNormal;
    }

    public void SetGameSpeedZero()
    {
        gameState.SetZeroGameSpeed();
    }

    public void SetGameSpeedNormal()
    {
        gameState.SetNormalGameSpeed();
    }

    public void ActivateOptionWindowInvoke()
    {
        _activateOptionWindow?.Invoke();
    }

    public void DeactivateOptionWindowInvoke()
    {
        _deactivateOptionWindow?.Invoke();
    }

    public void CreateOptionWindow()
    {
        if(_optionWindowPrefab!=null)
        {
            if (FindOtherOptionWindow())
                return;

            gameState.SpellModeOff();
            gameState.BuildModeOff();

            optionWindow = Instantiate(_optionWindowPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            optionWindow.transform.SetParent(transform);

            dropMouseClick = optionWindow.GetComponentInChildren<DropMouseClick>();
            ActivateOptionWindow += gameState.RegisterModalWindow;
            ActivateOptionWindow += dropMouseClick.TurnOffAllElements;
            DeactivateOptionWindow += gameState.UnregisterModalWindow;
            DeactivateOptionWindow += dropMouseClick.TurnOnAllElements;
        }
    }

    private bool FindOtherOptionWindow()
    {
        var otherOptionWindow = FindObjectOfType<OptionGameAnimatorConnector>();
        if (otherOptionWindow != null)
            return true;
        else
            return false;
    }

    private void DestroyOptionWindow()
    {
        Destroy(optionWindow, 1f);
    }
}
