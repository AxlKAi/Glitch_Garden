using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    GameState gameState;
    [SerializeField]
    private int starsAward = 50;
    [SerializeField]
    private float awardPeriod = 10f;
    [SerializeField]
    private float createdStarTTL = 9f;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        StartCoroutine(CreateStars());
    }

    public IEnumerator CreateStars()
    {
        do
        {
            yield return new WaitForSeconds(awardPeriod);
            StarItem star = Instantiate(gameState.GetStarItemPref(), transform.position, Quaternion.identity);
            star.SetStarsAmmount(starsAward);
            star.SetStarTTL(createdStarTTL);

        } while(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
