using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jabba : MonoBehaviour
{
    Attacker attacker;

    private void Start()
    {
        attacker = GetComponent<Attacker>();
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
