using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    Attacker attacker;
    [SerializeField] float foxRunningTime = 1f;
    [SerializeField] float foxRunningRandomDelay = 1f;
    float timeToFoxStartRunning = 100f;
    bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        attacker = GetComponent<Attacker>();
        timeToFoxStartRunning = Random.Range(0f, foxRunningRandomDelay);

    }

    // Update is called once per frame
    void Update()
    {
        timeToFoxStartRunning -= Time.deltaTime;
        if (timeToFoxStartRunning <=0)
        {
            Run();
            timeToFoxStartRunning = 500000f; //newer run
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Defender attackingDefender = collision.GetComponent<Defender>();
        if (attackingDefender)
        {
            if (isRunning)
            {
                StopRun();
            }
            if( IsItGrave(attackingDefender.name) )
            {
                attacker.Jump();
            }
            else
            {
                attacker.Attack(attackingDefender);
            } 
        }
    }

    bool IsItGrave(string defenderName)
    {
        string subString = "Grave";
        int indexOfSubstring = defenderName.IndexOf(subString);
        return (indexOfSubstring >= 0);
    }

    void Run()
    {
        isRunning = true;
        attacker.Run();
    }

    void StopRun()
    {
        isRunning = false;
        attacker.StopRun();
    }

}
