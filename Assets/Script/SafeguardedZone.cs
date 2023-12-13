using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeguardedZone : MonoBehaviour
{
    GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attacker attacker = collision.GetComponent<Attacker>();
        if(attacker)
        {
            gameState.DecLife();
            attacker.ReachSafeguardZone();
        }
        else
        {
            Debug.LogWarning("Something collide with SafeguardZonfe, but this is not right.");
        }
    }
}
