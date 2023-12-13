using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderCloseCombat : MonoBehaviour
{
    [SerializeField] int damage = 40;
    Defender defender;

    private List<Attacker> enemies = new List<Attacker>();
    public List<Attacker> GetAttackers() { return enemies; }

    // Start is called before the first frame update
    void Start()
    {
        defender = GetComponentInParent<Defender>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage()
    {
        for (var i = enemies.Count; i>0; i--)
        {
            if(enemies[i-1])
            {
                enemies[i-1].Hit(damage);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Attacker enemy = other.GetComponent<Attacker>();
        if (!enemies.Contains(enemy)&&enemy) { enemies.Add(enemy); }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Attacker enemy = other.GetComponent<Attacker>();
        enemies.Remove(enemy);
    }

    public bool isEnemyNearBy()
    {
        return (enemies.Count > 0);
    }
}
