using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAction : MonoBehaviour
{
    GameState gameState;
    Spell castingSpell;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.FindObjectOfType<GameState>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TryToCastSpell(Vector3 currentPosition, Defender defender = null, Attacker[] attackers = null)
    {
        bool isCastUsed = false;
        int id = gameState.GetCurrentSpell().id;
        castingSpell = gameState.GetCurrentSpell();

        switch (id)
        {
            // Health
            case 1: 
                if (defender != null)
                {
                    defender.Healing(castingSpell.spellPower);
                    ShowParticle(currentPosition);
                    isCastUsed = true;
                }
                    break;

            //FireBlast
            case 2:
                if(attackers!=null && attackers.Length>0)
                {
                    foreach(Attacker attacker in attackers)
                    {
                        attacker.Hit(castingSpell.spellPower);
                    }
                    ShowParticle(currentPosition);
                    isCastUsed = true;
                }
                break;

            //Slow
            case 3:
                ShowParticle(currentPosition);
                isCastUsed = true;
                break;

            //Freeze
            case 4:
                if (attackers != null && attackers.Length > 0)
                {
                    foreach (Attacker attacker in attackers)
                    {
                        attacker.SetFreezeEffect();
                    }
                    ShowParticle(currentPosition);
                    isCastUsed = true;
                }
                break;
        }

        return isCastUsed;
    }

    private void ShowParticle(Vector3 currentPosition)
    {
        if (castingSpell.particle)
        {
            Instantiate(castingSpell.particle, currentPosition, castingSpell.particle.transform.rotation);
        }
    }
}
