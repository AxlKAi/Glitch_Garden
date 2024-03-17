using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Attacker attacker;
    [SerializeField] float speed = 1f;
    [SerializeField] int damage = 20;

    [Tooltip("Time to live in seconds")]
    [SerializeField] float TTLsec = 20f;
    [SerializeField] private ParticleSystem _hitVFX;
    [SerializeField] private float _hitVFX_delay = 2f;
    [SerializeField] private GameObject _pickableVFXRootGO;
    [SerializeField] private string _mainSFX = "crab_dark_magic";

    // Start is called before the first frame update
    void Start()
    {
        attacker = GetComponentInParent<Attacker>();
        DestroyTTLexpired();

        GameObject go = GameObject.Find("LevelVFX");
        if (go)
        {
            _pickableVFXRootGO = go;
        }

        AudioManager.Instance.PlaySFX(_mainSFX);
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > Mathf.Epsilon)
        {
            float x = transform.position.x - Time.deltaTime * speed;
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Defender attackingDefenderRange = collision.GetComponent<Defender>();
        if (attackingDefenderRange)
        {
            attackingDefenderRange.Hit(damage);
            // DestroyProjectileImmidiatly();
            if(_hitVFX)
            {
                StartCoroutine(ShowHitVFX(attackingDefenderRange.transform.position));
            }
        }
    }

    private void DestroyTTLexpired()
    {
        StartCoroutine(DestroyProjectile());
    }

    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(TTLsec);
        Destroy(gameObject);
    }

    void DestroyProjectileImmidiatly()
    {
        Destroy(gameObject);
    }

    private IEnumerator ShowHitVFX(Vector3 pos)
    {
        ParticleSystem partcle = Instantiate(_hitVFX, pos, Quaternion.identity);
        yield return new WaitForSeconds(_hitVFX_delay);
    }
}
