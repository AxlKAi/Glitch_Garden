using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DropMouseClick))]
public class FirstWindow : MonoBehaviour
{
    [SerializeField] private Animator _tutorialWindowAnimator;
    [SerializeField] private TutorialController _tutorialControler;
    [SerializeField] private DropMouseClick _dropMouseClick;
    private GameState gameState;
    [SerializeField] private UnityEvent _windowAppearence;
    [SerializeField] private UnityEvent _windowClose;

    public event UnityAction WindowAppearence
    {
        add => _windowAppearence.AddListener(value);
        remove => _windowAppearence.RemoveListener(value);
    }

    public event UnityAction WindowClose
    {
        add => _windowClose.AddListener(value);
        remove => _windowClose.RemoveListener(value);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        if (_dropMouseClick == null)
            TryGetComponent<DropMouseClick>(out _dropMouseClick);

        gameState.SpellModeOff();
        gameState.BuildModeOff();
        WindowAppearence += _dropMouseClick.TurnOffAllElements;
        WindowAppearence += gameState.SpellModeOff;
        WindowAppearence += gameState.BuildModeOff;
        WindowAppearence += gameState.RegisterModalWindow;
        WindowClose += gameState.UnregisterModalWindow;
        WindowClose += _dropMouseClick.TurnOnAllElements;
        WindowClose += DestroyWindow;
    }

    public void CloseAnimation()
    {
        AudioManager.Instance.PlaySFX("UI_Click");
        _tutorialWindowAnimator.SetBool("CloseWindow", true);
    }

    public void OpenWindow()
    {
        _windowAppearence?.Invoke();
    }

    public void CloseWindow()
    {
        _windowClose?.Invoke();
    }

    public void DestroyWindow()
    {
        gameState.TutorialController.GlobalUIwindowDeactivateEvent();
        Destroy(gameObject, 1f);
    }
}
