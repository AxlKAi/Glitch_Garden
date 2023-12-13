using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMagicCollider : MonoBehaviour
{
    GameState gameState;
    Spell slowSpell;
    private Coroutine DestroySlowAreaCoroutine;

    private void Start()
    {
        gameState = GameObject.FindObjectOfType<GameState>();
        slowSpell = gameState.GetSpell(3);
        DestroySlowAreaCoroutine = StartCoroutine(DestroySlowArea(slowSpell.spellTime));
    }

    IEnumerator DestroySlowArea(float TTL)
    {
        yield return new WaitForSeconds(TTL);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attacker attacker = collision.GetComponent<Attacker>();
        if (attacker)
        {
            if(attacker.isInSlowMagic()==false)
            {
                attacker.ApplySlowMagic( gameState.GetSpell(3).spellPower );
            }
            else
            {
                attacker.ResetRemoveSlowEffectPeriod();
            }
        }
    }
}
