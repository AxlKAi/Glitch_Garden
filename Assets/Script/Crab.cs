using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : MonoBehaviour
{
    GameObject defendersGameObject;
    Attacker attacker;
    bool isCrabInAttack = false;
    [SerializeField] float deleyBeforeCrabCheckDefenders = 10f;
    [SerializeField] float maxAdditionalRandomDelayBeforeFirstCheck = 10;
        
    // Start is called before the first frame update
    void Start()
    {
        //After Crab spawn, hi walk about 1.5 sec + random time about 1.5, and if it see any Defender, he stops and start range attack
        defendersGameObject = GameObject.Find(GameState.DEFENDER_PARENT_GAME_OBJ);
        attacker = GetComponent<Attacker>();
        StartCoroutine(FirstCheckDefenders());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FirstCheckDefenders()
    {
        float delay = Random.Range(0, maxAdditionalRandomDelayBeforeFirstCheck);
        yield return new WaitForSeconds(delay+deleyBeforeCrabCheckDefenders);
        StartCoroutine( CheckDefenders() );
    }

    IEnumerator CheckDefenders()
    {
        do
        {
            yield return new WaitForSeconds(.3f);
            isCrabInAttack = IsDefenderBeforeYou();
            if (isCrabInAttack)
            {
                attacker.RangeAttack();
            }
            else
            {
                attacker.StopRangeAttack();
                StartCoroutine( FirstCheckDefenders() );
            }
        } while (isCrabInAttack);
    }

    bool IsDefenderBeforeYou()
    {
       bool isDefBefore = false;
       foreach(Transform defenderTransform in defendersGameObject.transform)
        {
            Defender defender = defenderTransform.gameObject.GetComponent<Defender>();
            if (defender)
            {
                if(defender.transform.position.y == transform.position.y && defender.transform.position.x < transform.position.x)
                {
                    isDefBefore = true;
                    // print("Find defender on x=" + defender.transform.position.x + "  y=" + defender.transform.position.y);
                }                
            }
        }
        return isDefBefore;
    }

}
