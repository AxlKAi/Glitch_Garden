using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuState : MonoBehaviour
{
    //реалзовать 3 состояния главного меню
    //  1-активны основные кнопки
    //  2-активно окно карты уровней
    //  3-окно опций
    // Start is called before the first frame update

    public enum State
    {
        MainButtons, LevelsMap, Options
    }

    [SerializeField] private Button[] _mainButtons;
    [SerializeField] private LevelsMapView _levelsMapWindow;
    [SerializeField] private OptionWindow _optionWindow;
    [SerializeField] private float _windowAppearenceDelay = 1.4f;

    private Vector3 _levelsMapWindowScale;
    private Vector3 _levelsMapWindowPosition;
    private Vector3 _optionWindowScale;
    private Vector3 _optionWindowPosition;

    public void SetState(State newState)
    {
        switch (newState)
        {
            case State.MainButtons:
                ShowMainButtons();
                break;

            case State.LevelsMap:
                ShowWindow(_levelsMapWindow.transform, _levelsMapWindowPosition, _levelsMapWindowScale, DisableMainButtons);
                break;

            case State.Options:
                ShowWindow(_optionWindow.transform, _optionWindowPosition, _optionWindowScale, DisableMainButtons);
                break;
        }
    }  

    private void Start()
    {
        _levelsMapWindowScale = _levelsMapWindow.transform.localScale;
        _levelsMapWindow.transform.localScale = new Vector3(0, 0, 0);
        _levelsMapWindowPosition = _levelsMapWindow.transform.position;
        _levelsMapWindow.transform.position = transform.position;

        _optionWindowScale = _optionWindow.transform.localScale;
        _optionWindow.transform.localScale = new Vector3(0, 0, 0);
        _optionWindowPosition = _optionWindow.transform.position;
        _optionWindow.transform.position = transform.position;
    }
    private void ShowMainButtons()
    {
        if (_levelsMapWindow.isActiveAndEnabled)
        {
            HideWindow(_levelsMapWindow.transform, EnableMainButtons);
        }

        if (_optionWindow.isActiveAndEnabled)
        {
            HideWindow(_optionWindow.transform, EnableMainButtons);
        }
    }

    private void DisableMainButtons()
    {
        foreach (Button button in _mainButtons)
        {
            button.interactable = false;
        }
    }

    private void EnableMainButtons()
    {
        _levelsMapWindow.gameObject.SetActive(false);
        _optionWindow.gameObject.SetActive(false);

        foreach (Button button in _mainButtons)
        {
            button.interactable = true;
        }
    }

    private void HideWindow(Transform windowTransform, TweenCallback callback)
    {
        Sequence s = DOTween.Sequence();
        s.Append(windowTransform.DOMove(transform.position, _windowAppearenceDelay).OnComplete(callback));
        s.Join(windowTransform.DOScale(new Vector3(0, 0, 0), _windowAppearenceDelay));
    }

    private void ShowWindow(Transform windowTransform, Vector3 windowPosition, Vector3 windowScale, TweenCallback callback)
    {
        windowTransform.gameObject.SetActive(true);

        Sequence s = DOTween.Sequence();
        s.Append(windowTransform.DOMove( windowPosition, _windowAppearenceDelay).OnComplete(callback));
        s.Join(windowTransform.DOScale( windowScale, _windowAppearenceDelay));
    }
}
