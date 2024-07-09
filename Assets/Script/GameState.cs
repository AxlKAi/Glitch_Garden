using System.Collections;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState _instance;

    [SerializeField] private LayerMask inputLayerMask;
    [SerializeField] private TutorialController _tutorialController;
    [SerializeField] private bool _easyDifficulty = false;

    public TutorialController TutorialController => _tutorialController;

    public const float difficulty_min_level = 0f;
    public const float difficulty_max_level = 2f;
    private static float _difficulty_level = 0;
    public const float difficulty_default = 1;
    public static float Difficulty_level { get { return _difficulty_level; } set { }  }

    //TODO singltone!!!!

    public StarsUI starsUI;
    public LifesUI lifesUI;
    
    [SerializeField] int startStarsAmmount = 1000;
    int starsCurrentCount;
    public int TotalStarsExtracted { get; private set; } = 0;
    
    [Tooltip ("How mutch monster can reach safeguard zone before game over")]
    [SerializeField] int lifesInLevel = 15;
    
    GameOverCanvas gameOverCanvas;
    int maxMobSpawnCount = 0;
    public int KilledMobSpawnCount { get; private set; } = 0;

    EnemyCountSlider enemyCountSlider;
    bool isGameOver = false;
    public static LevelManager levelManager = null;

    private bool isInBuildMode = false;
    public delegate void SetBuildModeOn(Defender def);
    public event SetBuildModeOn BuildModeOnEvent;
    public delegate void SetBuildModeOff();
    public event SetBuildModeOff BuildModeOffEvent;

    private bool isInSpellMode = false;

    public const string DEFENDER_PARENT_GAME_OBJ = "Defenders";
    public const string PROJECTILE_PARRENT_GAME_OBJ = "Projectiles";
    private GameObject defendersRoot;

    [SerializeField] public Spell[] allSpells;
    public delegate void SetSpellModeOn(Spell spell);
    public event SetSpellModeOn SpellModeOnEvent;
    public delegate void SetSpellModeOff();
    public event SetSpellModeOff SpellModeOffEvent;
    private Spell currentSpell;
    private SpellAction spellAction;

    private Inventory inventory;
    [SerializeField] private Spell_item spell_itemPref;
    [SerializeField] private StarItem starsItemPref;
    [SerializeField] private int pickableLayerNum = 12;

    private Spawner[] allSpawners;
    private int allSpawnerCount = 0;
    private int emptySpawnerCount = 0;
    private LevelUI levelUI;
    public delegate void StartWaveDelegate(int num);
    public event StartWaveDelegate StartWaveEvent;
    private bool isAllSpawnersEmpty = false;
    private bool endOfWave = false;
    private int waveNum = 0;
    private int waveMaxNum = 0;

    private int modalWindowCount = 0; // if some modal window is open right now, than > 0

    [SerializeField]
    private GameSpeed _gameSpeed;

    public GameSpeed GameSpeed { get { return _gameSpeed; } }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;

        starsCurrentCount = startStarsAmmount;
        EnableStarsUI();
        EnableLifesUI();
        gameOverCanvas = FindObjectOfType<GameOverCanvas>();
        EnableMobSpawnCountSlider();
        levelManager = FindObjectOfType<LevelManager>();
        InitializProjectilesRoot();
        Camera.main.eventMask = inputLayerMask;

        spellAction = GameObject.FindObjectOfType<SpellAction>();

        inventory = FindObjectOfType<Inventory>();

        pickableLayerNum = LayerMask.GetMask("PickableItems");

        // TODO delete
        levelUI = FindObjectOfType<LevelUI>();

        InitializeDefendersRootGameObj();

        LoadDifficultyLevel();
    }

    private void LoadDifficultyLevel()
    {
        _difficulty_level = PlayerPrefsController.GetDifficulty();
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    private void EnableStarsUI()
    {
        starsUI = FindObjectOfType<StarsUI>();
        if (!starsUI)
        {
            Debug.LogError("Please, add StarsUI obj to sceene");
        }
        starsUI.DisplayStars(starsCurrentCount);
    }

    private void EnableLifesUI()
    {
        lifesUI = FindObjectOfType<LifesUI>();

        if (!lifesUI)
        {
            Debug.LogError("Please, add LifesUI obj to sceene");
        }

        lifesUI.DisplayLifes(lifesInLevel);
    }

    private void FixedUpdate()
    {
        checkNewWaveStart();
    }

    public void AddStars(int starsPoint)
    {
        if(isGameOver) { return; } 

        starsCurrentCount += starsPoint;
        TotalStarsExtracted += starsPoint;
        starsUI.DisplayStars(starsCurrentCount);
    }

    public void SpendStars(int starsPoint)
    {
        if(starsCurrentCount >= starsPoint)
        {
            starsCurrentCount -= starsPoint;
        }

        starsUI.DisplayStars(starsCurrentCount);
    }

    public bool IsEnoughStars(int starsCost)
    {
        return (starsCurrentCount >= starsCost);
    }

    public void DecLife()
    {
        if (isGameOver) { return; }

        lifesInLevel -= 1;
        lifesUI.DisplayLifes(lifesInLevel);
        lifesUI.SpawnDecreaseEffect();

        if (lifesInLevel <=0 )
        {
            GameOver();
        }        
    }

    void GameOver() 
    {
        BuildModeOff();
        SpellModeOff();
        isGameOver = true;
        gameOverCanvas.TurnOn();
    }

    void EnableMobSpawnCountSlider()
    {
        enemyCountSlider = FindObjectOfType<EnemyCountSlider>();
        CountEnemies();
        enemyCountSlider.SetMaxValue(maxMobSpawnCount);
    }

    void CountEnemies()
    {
        if (allSpawners == null) { SetAllSpawners(); }
        foreach (Spawner spawner in allSpawners)
        {
            allSpawnerCount++;
            maxMobSpawnCount += spawner.GetRemainMobSpawnCount();
        }
    }

    private void SetAllSpawners()
    {
        allSpawners = FindObjectsOfType<Spawner>();
    }

    public void AddKilledEnemy()
    {   
        KilledMobSpawnCount += 1;
        enemyCountSlider.SetValue(KilledMobSpawnCount);
    }

    private void ShowWinScreen()
    {
        gameOverCanvas.ShowWinScreen(KilledMobSpawnCount, TotalStarsExtracted);
        PlayerPrefsController.SaveLevelCompletition(levelManager.CurrentLevelIndex(), (int)_difficulty_level + 1);
    }

    public void TestWinMethod()
    {
        Time.timeScale = 1f;
        ShowWinScreen();
    }

    void InitializProjectilesRoot()
    {
        GameObject projectilesRoot = GameObject.Find(PROJECTILE_PARRENT_GAME_OBJ);
        if (!projectilesRoot)
        {
            projectilesRoot = new GameObject(PROJECTILE_PARRENT_GAME_OBJ);
        }
    }

    public bool GetBuildMode()
    {
        return isInBuildMode;
    }

    public void BuildModeOn(Defender def)
    {
        isInBuildMode = true;
        isInSpellMode = false;
        BuildModeOnEvent?.Invoke(def);
    }

    public void BuildModeOff()
    {
        isInBuildMode = false;
        isInSpellMode = false;
        BuildModeOffEvent?.Invoke();
    }

    public bool GetSpellMode()
    {
        return isInSpellMode;
    }

    public void SpellModeOn(Spell spell)
    {
        currentSpell = spell;
        isInSpellMode = true;
        isInBuildMode = false;
        SpellModeOnEvent?.Invoke(spell);
    }

    public void SpellModeOff()
    {
        isInSpellMode = false;
        isInBuildMode = false;
        currentSpell = null;
        SpellModeOffEvent?.Invoke();
    }

    public Spell GetCurrentSpell()
    {
        return currentSpell;
    }

    public SpellAction GetSpellAction()
    {
        return spellAction;
    }

    public Spell[] GetAllSpells()
    {
        return allSpells;
    }

    public Spell GetSpell(int spell_id)
    {
        Spell resultSpell = null;
        foreach(Spell s in allSpells)
        {
            if(s.id == spell_id)
            {
                resultSpell = s;
            }
        }
        return resultSpell;
    }

    public StarItem GetStarItemPref()
    {
        return starsItemPref;
    }

    public int GetPickableLayerNum()
    {
        return pickableLayerNum;
    }

    public LevelUI GetLevelUI() 
    {
        return levelUI;
    }

    public int GetAllSpawnersCount()
    {
        Debug.Log("spawnerCount "+allSpawnerCount);
        return allSpawnerCount;
    }

    public void StartWave(int wave_num)
    {
        waveNum = wave_num;
        StartWaveEvent?.Invoke(wave_num);
    }

    public int GetWaveNum()
    {
        return waveNum;
    }

    public void SetEndOfWave(bool status)
    {
        endOfWave = status;
    }

    public bool GetEndOfWave()
    {
        return endOfWave;
    }

    public void checkNewWaveStart()
    {
        if (endOfWave && emptySpawnerCount==allSpawnerCount)
        {
            if(EnemyAtMapCount() == 0)
            {
                waveNum++;
                emptySpawnerCount = 0;
                endOfWave = false;
                if (waveNum <= waveMaxNum)
                {
                    levelUI.ShowWave(waveNum);
                }
                else
                {
                    if(!isGameOver) ShowWinScreen();
                }
            }
        }
    }

    public void IncEmptySpawner()
    {
        emptySpawnerCount++;
    }

    public void SetWameMaxNum(int waveMax)
    {
        waveMaxNum = waveMax;
    }

    private int EnemyAtMapCount()
    {
        Attacker[] attakers;
        attakers = GameObject.FindObjectsOfType<Attacker>();
        int count = attakers.Length;
        return count;
    }

    void InitializeDefendersRootGameObj()
    {
        defendersRoot = GameObject.Find(GameState.DEFENDER_PARENT_GAME_OBJ);
        if (!defendersRoot)
        {
            defendersRoot = new GameObject(GameState.DEFENDER_PARENT_GAME_OBJ);
        }
    }

    public GameObject GetDefendersRoot()
    {
        return defendersRoot;
    }

    public bool IsModalWindowExist()
    {
        return (modalWindowCount>0);
    }

    public void RegisterModalWindow()
    {
        modalWindowCount++;
    }

    public void UnregisterModalWindow()
    {
        modalWindowCount--;
    }    
}
