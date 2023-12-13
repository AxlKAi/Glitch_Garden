using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyPiggy : MonoBehaviour
{
    ParticleSystem[] particles;
    int particleIndex = 0;
    Attacker attacker;
    GameObject fireBlastRangeZone;
    [SerializeField] EnemyProjectile rangeBurnPrefab;

    // Start is called before the first frame update
    void Start()
    {
        attacker = GetComponent<Attacker>();
        fireBlastRangeZone = transform.Find("FireBlastRangeZone").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowFlameAttack()
    {
        particles = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
        FireBurnRangeProjectile();

    }

    void FireBurnRangeProjectile()
    {
        EnemyProjectile projectile = Instantiate(rangeBurnPrefab, fireBlastRangeZone.transform.position, Quaternion.identity);
        // projectile.transform.parent = attacker.
    }

    void StopAllVFX()
    {
        particles = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particle in particles)
        {
            particle.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Defender attackingDefender = collision.GetComponent<Defender>();
        if (attackingDefender)
        {
            attacker.Attack(attackingDefender);
        }
        else
        {

        }
    }
}
