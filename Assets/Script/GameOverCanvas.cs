using UnityEngine;
using TMPro;

public class GameOverCanvas : MonoBehaviour
{
    GameState gameState;

    [SerializeField]
    TextMeshProUGUI textUI;

    [SerializeField]
    RestartButtonUI restartButtonUI;

    [SerializeField]
    MainMenuButtonUI mainMenuButtonUI;

    [SerializeField]
    NextLevelButtonUI nextLevelButtonUI;

    [SerializeField]
    TextMeshProUGUI starsTotalUI;

    [SerializeField]
    TextMeshProUGUI killsTotalUI;

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
    }
}
