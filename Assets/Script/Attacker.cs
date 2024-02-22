using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] [Range(.1f, 5f)] float speed = 1f;

    [Tooltip("Health of attacker in the begining")]
    [SerializeField] int health = 100;
    int maxHealth;
    [SerializeField] GameObject deathVFX;
    [SerializeField] ParticleSystem hitVFX;
    [SerializeField] int starsAward = 100;
    [SerializeField] int damage = 60;
    [SerializeField] EnemyProjectile enemyProjectile;

    GameState gameState;
    Animator animator;
    Defender attackingDefender;
    GameObject projectilesRoot;
    HealthBar healthBar;
    Transform hitVFXtransform;

    bool isInSlow = false;
    Coroutine RemoveSlowPeriodCorout;
    float slowTime;
    float normalSpeed;

    bool isInFreeze = false;
    Coroutine RemoveFreezeEffectCorout;
    AnimatorStateInfo animatorState;
    int stateNameHash;

    Color defaultSpriteCollor;
    Color currentSpriteCollor;
    [SerializeField] private Color _hitColor = Color.red;
    [SerializeField] private Color _freezeColor = Color.blue;

    [SerializeField] private string _attackSFX = "crocodile_attack";
    [SerializeField] private string _idleSFX = "crocodile_idle";
    [SerializeField] private string _deathSFX = "crocodile_death";
    private AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        animator = GetComponent<Animator>();
        projectilesRoot = GameObject.Find(GameState.PROJECTILE_PARRENT_GAME_OBJ);
        healthBar = GetComponentInChildren<HealthBar>();
        maxHealth = health;
        hitVFXtransform = transform.Find("HitVFXpoint");
        normalSpeed = this.speed;

        SpriteRenderer spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        defaultSpriteCollor = spriteRenderer.color;
        currentSpriteCollor = defaultSpriteCollor;
        _audioManager = AudioManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left*speed*Time.deltaTime);
    }

    public void SetMovementSpeed(float speed)
    {
        if(isInFreeze)
        {
            this.speed = 0;
        }
        else if (isInSlow)
        {
            this.speed = speed / 2;
        }
        else
        {
            this.speed = speed;
        }        
    }

    public void PlayIdleSFX()
    {
        bool isAttack = false;

        if (animator != null)
            isAttack = animator.GetBool("isAttacking");

        if (!isAttack && animator != null)
            _audioManager.PlaySFX(_idleSFX);
    }

    private void PlayAttackSFX()
    {
        _audioManager.PlaySFX(_attackSFX);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            Hit(bullet.GetDamege());
            bullet.DestroyBullet();
        }
    }

    public void Hit(int damage)
    {
        health -= damage;
        StartCoroutine(ShowHitAnimation());
        if (health <= 0)
        {
            gameState.AddStars(starsAward);
            Die();
        }
        float healthBarParam = (float)health / (float)maxHealth;
        healthBar.SetSize(healthBarParam);
    }

    IEnumerator ShowHitAnimation()
    {
        if (hitVFX)
        {
            Instantiate(hitVFX, hitVFXtransform.position, hitVFXtransform.rotation);
        }

        SpriteRenderer spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = Color.Lerp(defaultSpriteCollor, _hitColor, .7f);
        yield return new WaitForSeconds(.2f);
        spriteRenderer.color = currentSpriteCollor;

    }

    void Die()
    {
        StopAllCoroutines();
        gameState.AddKilledEnemy();
        if (deathVFX)
        {
            Vector3 g_position = gameObject.transform.position;
            BoxCollider2D box = GetComponent<BoxCollider2D>();

            Vector3 box_center = new Vector3(g_position.x + box.offset.x, g_position.y + box.offset.y, 0) ;
            Instantiate(deathVFX, transform.position, transform.rotation);
            
        }
        Destroy(gameObject);
    }

    public void Attack(Defender attackingDefender)
    {
        this.attackingDefender = attackingDefender;
        animator.SetBool("isAttacking", true);
    }

    public void RangeAttack()
    {
        animator.SetBool("isRangeAttacking", true);
    }

    void Fire()
    {
        EnemyProjectile bullet = Instantiate(enemyProjectile, transform.position, Quaternion.identity);
        // TODO assign bullet to Defender who shoot. Problem whis cactus, he dont have BODY object.
        //bullet.transform.parent = gameObject.transform;

        bullet.transform.parent = projectilesRoot.transform;
    }

    public void DealDamage()
    {
        if (!attackingDefender)
        {
            animator.SetBool("isAttacking", false);
        }
        else
        {
            attackingDefender.Hit(damage);
            PlayAttackSFX();
        }        
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");
    }

    public void Run()
    {
        animator.SetBool("isRunning", true);
    }

    public void StopRun()
    {
        animator.SetBool("isRunning", false);
    }

    public void ReachSafeguardZone()
    {
        Die();
    }

    public void StopRangeAttack()
    {
        animator.SetBool("isRangeAttacking", false);
    }

    public void ApplySlowMagic(float period)
    {
        if(!isInSlow)
        {
            isInSlow = true;
            speed *= .5f;
            SpriteRenderer spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.color = Color.green;
            RemoveSlowPeriodCorout = StartCoroutine(RemoveSlowEffect(period));
        }
    }

    IEnumerator RemoveSlowEffect(float period)
    {
        slowTime = period;
        yield return new WaitForSeconds(period);
        speed = normalSpeed;
        isInSlow = false;
        SpriteRenderer spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = Color.white;
    }

    public bool isInSlowMagic()
    {
        return isInSlow;
    }

    public void ResetRemoveSlowEffectPeriod()
    {
        StopCoroutine(RemoveSlowPeriodCorout);
        RemoveSlowPeriodCorout = StartCoroutine(RemoveSlowEffect(slowTime)); 
    }

    public void SetFreezeEffect()
    {
        if(!isInFreeze)
        {
            isInFreeze = true;
            speed = 0;
            animatorState = animator.GetCurrentAnimatorStateInfo(0);
            stateNameHash = animatorState.fullPathHash;
            animator.enabled = false;
            RemoveFreezeEffectCorout = StartCoroutine(RemoveFreezeEffect(gameState.GetSpell(4).spellTime));
        }
        else
        {
            StopCoroutine(RemoveFreezeEffectCorout);
            RemoveFreezeEffectCorout = StartCoroutine(RemoveFreezeEffect(gameState.GetSpell(4).spellTime));
        }
    }

    public IEnumerator RemoveFreezeEffect(float time)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = _freezeColor;
        currentSpriteCollor = _freezeColor;

        yield return new WaitForSeconds(time);
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = defaultSpriteCollor;
        currentSpriteCollor = defaultSpriteCollor;
        animator.enabled = true;
        animator.Play(stateNameHash);
        isInFreeze = false;
    }
}
