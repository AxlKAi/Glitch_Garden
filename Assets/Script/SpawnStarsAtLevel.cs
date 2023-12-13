using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStarsAtLevel : MonoBehaviour
{
    [SerializeField]
    private StarItem sparItemPref;

    private GameState gameState;

    [SerializeField]
    private StarSpawnItemList items;

    private Coroutine currentSpawnCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();

        StartSpawnItems();
    }

    void StartSpawnItems()
    {
        currentSpawnCoroutine = StartCoroutine(StartSpawnItemsCoroutine());
    }

    private IEnumerator StartSpawnItemsCoroutine()
    {
        foreach (StarSpawnItem item in items.starSpawnItemList)
        {
            yield return StartCoroutine(SpawnItem(item));
        }
    }

    IEnumerator SpawnItem(StarSpawnItem item)
    {
        yield return new WaitForSeconds(item.delay);
        StarItem starItem = Instantiate(sparItemPref, item.position, Quaternion.identity);
        starItem.SetStarsAmmount(item.amount);
    }

    public void PauseStarsSpawner()
    {
        StopCoroutine(currentSpawnCoroutine);
        gameObject.SetActive(false);
    }

    public void ContinueStarsSpawner()
    {
        gameObject.SetActive(true);
        currentSpawnCoroutine = StartCoroutine(StartSpawnItemsCoroutine());
    }
}
