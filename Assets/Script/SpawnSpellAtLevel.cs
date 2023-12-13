using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpellAtLevel : MonoBehaviour
{
    [SerializeField] private SpawnebleSpellItemList items;

    GameState gameState;

    [SerializeField]
    Spell_item spellItemPref;

    private Coroutine currentSpawnCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        StartSpawnItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartSpawnItems()
    {
        currentSpawnCoroutine = StartCoroutine( StartSpawnItemsCoroutine() );
    }

    private IEnumerator StartSpawnItemsCoroutine()
    {
        foreach (SpawnebleSpellItem item in items.spawnebleSpellItemList)
        {
           yield return StartCoroutine(SpawnItem(item));
        }
    }

    IEnumerator SpawnItem(SpawnebleSpellItem item)
    {
        yield return new WaitForSeconds(item.delay);
        Spell_item spell_item = Instantiate(spellItemPref, item.position, Quaternion.identity);
        spell_item.SetSpell_id(item.spell_id);
    }

    public void PauseSpellSpawner()
    {
        StopCoroutine(currentSpawnCoroutine);
        gameObject.SetActive(false);
    }

    public void ContinueSpellSpawner()
    {
        gameObject.SetActive(true);
        currentSpawnCoroutine = StartCoroutine(StartSpawnItemsCoroutine());
    }
}

