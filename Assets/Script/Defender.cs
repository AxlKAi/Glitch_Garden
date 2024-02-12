using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] int starCost = 100;
    GameState gameState;
    Spawner enemySpawnerInMyLine = null;
    Animator animator;
    bool isRangeAttacking = false;
    bool isCloseAttacking = false;

    [SerializeField] int health = 100;
    int maxHealth;
    [SerializeField] ParticleSystem hitParticlePref;
    [SerializeField] float particleTTLsec = 1f;
    [SerializeField] bool canRangeAttack = true;
    [SerializeField] bool canCloseFight = false;
    DefenderCloseCombat closeCombatComponent;
    HealthBar healthBar;
    [SerializeField] ParticleSystem closeCombatVFX;
    Transform fightVFX;

    public delegate void OnDestroyDefender();
    public event OnDestroyDefender OnDestroyDefenderEvent;

    [SerializeField] private ParticleSystem _buildVFX;

    Color defaultSpriteCollor;
    [SerializeField] private Color _hitColor = Color.red;
    [SerializeField] private SpriteRenderer _hitSprite;
    [SerializeField] private float _animatorSpawnDelay = .05f;

    private AudioManager _audioManager;
    [SerializeField] private string _buildSFX = "Cactus_build";
    [SerializeField] private string[] _closeAttackSFX;


    //   [SerializeField] private float animationSpeedRando!!!!!!

    // Start is called before the first frame update
    void Awake()
    {
        SetOrderLayer();
        gameState = FindObjectOfType<GameState>();
        animator = GetComponent<Animator>();
        SetLineSpawner();

        if (canCloseFight)
        {
            closeCombatComponent = GetComponentInChildren<DefenderCloseCombat>();
            fightVFX = transform.Find("FightVFX");
        }

        healthBar = GetComponentInChildren<HealthBar>();
        maxHealth = health;

        _audioManager = AudioManager.Instance;
    }

    void Start()
    {
        defaultSpriteCollor = _hitSprite.color;
        float delay_tmp = Random.Range(-_animatorSpawnDelay, _animatorSpawnDelay);
        animator.SetFloat("runMultiplier", 1f + delay_tmp);
        _audioManager.PlaySFX(_buildSFX);
    }

    // Update is called once per frame
    void Update()
    {
        //TODO make update every .5 sec
        if (canCloseFight)
        {
            CheckEnemyNearBy();
        }
        if (canRangeAttack)
        {
            CheckRangeAttackState();
        }
    }

    public Sprite GetBodySprite()
    {
        Sprite resultSprite;
        Transform body = transform.Find("Body");

        if (body)
        {
            resultSprite = body.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            resultSprite = GetComponentInChildren<SpriteRenderer>().sprite;
        }
        return resultSprite;
    }

    bool CheckEnemyNearBy()
    {
        bool newCombatState = closeCombatComponent.isEnemyNearBy();
        if (newCombatState)
        {
            StartCloseFight();
        }
        else
        {
            StopCloseFight();
        }
        return newCombatState;
    }

    public void DealDamage()
    {
        PlayCloseCombatSFX();
        closeCombatComponent.DealDamage();        
    }

    private void PlayCloseCombatSFX()
    {
        string nameSFX;        

        if(_closeAttackSFX.Length > 0)
        {
            int index = Random.Range(0, _closeAttackSFX.Length - 1 );
            nameSFX= _closeAttackSFX[index];
        }
        else
        {
            nameSFX = _closeAttackSFX[0];
        }
        
        _audioManager.PlaySFX(nameSFX);
    }

    void ShowFightVFX()
    {
        ParticleSystem vfx = Instantiate(closeCombatVFX, fightVFX.position, fightVFX.rotation);
        vfx.gameObject.transform.parent = gameObject.transform;
        Destroy(vfx.gameObject, 1f);        
    }

    private void CheckRangeAttackState()
    {
        bool testRangeAtackState = AttackersInLine();
        if (testRangeAtackState != isRangeAttacking)
        {
            if (testRangeAtackState)
            {
                animator.SetBool("isAttacking", true);
                isRangeAttacking = true;
            }
            else
            {
                StopRangeAttack();
            }
        }
    }

    private void StartCloseFight()
    {
        animator.SetBool("isCloseCombat", true);
        isCloseAttacking = true;
        isRangeAttacking = false;
    }

    private void StopCloseFight()
    {
        animator.SetBool("isCloseCombat", false);
        isCloseAttacking = false;
    }

    private bool AttackersInLine()
    {
        if (enemySpawnerInMyLine == null)
        {
            return false;
        } 
        else
        {
            return (enemySpawnerInMyLine.transform.childCount > 0);
        }        
    }

    void SetLineSpawner()
    {
        Spawner[] allSpawners;
        allSpawners = FindObjectsOfType<Spawner>();

        foreach(Spawner enemySpawner in allSpawners)
        {
            bool isClosEnoughSpownerInMyLine = Mathf.Abs(enemySpawner.transform.position.y - transform.position.y) <= Mathf.Epsilon;
            if (isClosEnoughSpownerInMyLine)
            {
                enemySpawnerInMyLine = enemySpawner;
            }
        }
    }

    public bool isLineSpawnerSet(int spawnerY)
    {
        bool isLineSpawner=false;

        Spawner[] allSpawners;
        allSpawners = FindObjectsOfType<Spawner>();

        foreach (Spawner enemySpawner in allSpawners)
        {
            bool isClosEnoughSpownerInMyLine = Mathf.Abs(enemySpawner.transform.position.y - spawnerY) <= Mathf.Epsilon;
            if (isClosEnoughSpownerInMyLine)
            {
                isLineSpawner = true;
            }
        }

        return isLineSpawner; 
    }

    void SetOrderLayer()
    {
        SpriteRenderer[] sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
        
        foreach(SpriteRenderer sprite in sprites)
        {
            sprite.sortingOrder = 5;
        }
    }

    public int GetDefenderCost()
    {
        return starCost;
    }

    public void Hit(int damage)
    {
        health -= damage;
        HitParticle();
        if (health<=0)
        {
            Die();
        }
        float healthBarParam = (float)health / (float)maxHealth;
        healthBar.SetSize(healthBarParam);
    }

    void HitParticle()
    {
        if(hitParticlePref)
        {
            ParticleSystem pHit = Instantiate(hitParticlePref, transform.position, Quaternion.identity);
        }
        StartCoroutine(AppendHitColor());    
    }

    private IEnumerator AppendHitColor()
    {
        _hitSprite.color = Color.Lerp(defaultSpriteCollor, _hitColor, .7f);
        yield return new WaitForSeconds(.2f);
        _hitSprite.color = defaultSpriteCollor;
    }

    void Die()
    {
        OnDestroyDefenderEvent?.Invoke();
        Destroy(gameObject);
    }

    public void Healing(int addHealth)
    {
        health += addHealth;
        if(health > maxHealth)
        {
           health = maxHealth;
        }

        if (health == maxHealth)
        {
            healthBar.HideBar();
        } else
        {
            float healthBarParam = (float)health / (float)maxHealth;
            healthBar.SetSize(healthBarParam);
        }
    }

    public void StopRangeAttack()
    {
        animator.SetBool("isAttacking", false);
        isRangeAttacking = false;
    }

    public void ShowBuildVFX()
    {
        if(_buildVFX)
        {
            Instantiate(_buildVFX, transform.position, Quaternion.identity);
        }
    }
}
