using UnityEngine;
using TMPro;

public class GameOverCanvas : MonoBehaviour
{
    GameState gameState;

    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private RestartButtonUI restartButtonUI;
    [SerializeField] private MainMenuButtonUI mainMenuButtonUI;
    [SerializeField] private NextLevelButtonUI nextLevelButtonUI;
    [SerializeField] private TextMeshProUGUI starsTotalUI;
    [SerializeField] private TextMeshProUGUI killsTotalUI;

    

    public void TurnOn()
    {       
        transform.GetChild(0).gameObject.SetActive(true);
        starsTotalUI.text = gameState.TotalStarsExtracted.ToString();
        killsTotalUI.text = gameState.KilledMobSpawnCount.ToString();
    }

    public void ShowWinScreen(int killedMobs, int totalStarsExtracted)
    {
        gameState = GameState._instance;
        TurnOn();
        restartButtonUI.gameObject.SetActive(false);
        mainMenuButtonUI.gameObject.SetActive(false);
        nextLevelButtonUI.gameObject.SetActive(true);
        textUI.text = "You are WIN!";

        gameState.SpellModeOff();
        gameState.BuildModeOff();

        var dropMouseClick = transform.GetComponentInChildren<DropMouseClick>();
        dropMouseClick.TurnOffAllElements();
        gameState.RegisterModalWindow();
        gameState.SetNormalGameSpeed();
    }
}
