using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameOverCanvas : MonoBehaviour
{
    GameState gameState;

    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private RestartButtonUI restartButtonUI;
    [SerializeField] private MainMenuButtonUI mainMenuButtonUI;
    [SerializeField] private NextLevelButtonUI nextLevelButtonUI;
    [SerializeField] private TextMeshProUGUI starsTotalUI;
    [SerializeField] private TextMeshProUGUI killsTotalUI;

    [SerializeField] private float _windowAppearenceDelay = .7f;

    private Transform _windowTransform;
    private Vector3 _windowScale;
    private Vector3 _windowPosition;

    public void TurnOn()
    {
        _windowTransform.gameObject.SetActive(true);
        starsTotalUI.text = gameState.TotalStarsExtracted.ToString();
        killsTotalUI.text = gameState.KilledMobSpawnCount.ToString();

        gameState.SpellModeOff();
        gameState.BuildModeOff();

        var dropMouseClick = transform.GetComponentInChildren<DropMouseClick>();
        dropMouseClick.TurnOffAllElements();
        gameState.RegisterModalWindow();
        gameState.SetNormalGameSpeed();

        Sequence s = DOTween.Sequence();
        s.Append(_windowTransform.DOMove(_windowPosition, _windowAppearenceDelay));
        s.Join(_windowTransform.DOScale(_windowScale, _windowAppearenceDelay));

    }

    public void ShowWinScreen(int killedMobs, int totalStarsExtracted)
    {
        gameState = GameState._instance;
        TurnOn();
        restartButtonUI.gameObject.SetActive(false);
        mainMenuButtonUI.gameObject.SetActive(false);
        nextLevelButtonUI.gameObject.SetActive(true);
        textUI.text = "You are WIN!";
    }

    private void Start()
    {
        _windowTransform = transform.GetChild(0).gameObject.transform;

        _windowScale = _windowTransform.localScale;
        _windowTransform.localScale = new Vector3(0, 0, 0);

        _windowPosition = _windowTransform.position;
        _windowTransform.position = new Vector3(0, 0, 0);
    }
}
