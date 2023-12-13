using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] SpawnList[] attakers;
    [SerializeField] [Range(.1f, 20f)] float startSpawnerDelay = 1f;
    int remainMobSpawnCount = 0;
    GameState gameState;
    private int waveCount = 0;

    private float _difficultyCoefficient =1f;


    // Start is called before the first frame update
    void Start()
    {
        gameState = FindObjectOfType<GameState>();
        //InitializeRemainMobSpawnCount();

        if (GameState.Difficulty_level == 0)
            _difficultyCoefficient = 1.3f;
        else if (GameState.Difficulty_level == 1)
            _difficultyCoefficient = 1f;
        else
            _difficultyCoefficient = .9f;

        gameState.StartWaveEvent += StartWave;
        waveCount = attakers.Length;
        gameState.SetWameMaxNum(waveCount);
    }

    void InitializeRemainMobSpawnCount()
    {
        if (attakers.Length == 0)
        {
            Debug.Log("No enemies list on spawner " + name);
        }

        foreach (SpawnList att in attakers)
        {
            foreach (SpawnItems spawnItem in att.spawnList)
            {
                remainMobSpawnCount += spawnItem.count;
            }
        }
    }

    public void StartWave(int wave_num)
    {
        StartCoroutine(StartSpawner(wave_num));
    }

    IEnumerator StartSpawner(int wave_num)
    {
        SpawnList attList = attakers[wave_num-1];
        yield return StartCoroutine(SpawnOneWave(attList));
        gameState.SetEndOfWave(true);
        gameState.IncEmptySpawner();
    }


    IEnumerator SpawnOneWave(SpawnList att)
    {
        yield return new WaitForSeconds(startSpawnerDelay);

        foreach (SpawnItems spawnItem in att.spawnList)
        {
            yield return StartCoroutine(SpawnOneSpawnItem(spawnItem.attackerPrefab, spawnItem.count, spawnItem.delay));
        }
    }


    IEnumerator SpawnOneSpawnItem(Attacker attacker, int count, float spawnDelay)
    {
        for(int mobNum = 0; mobNum < count; mobNum++)
        {
            SpawnMob(attacker);
            yield return new WaitForSeconds(spawnDelay*_difficultyCoefficient);
        }
    }

    void SpawnMob(Attacker attacker)
    {
        Attacker spawnableEnemy = Instantiate(attacker, transform.position, Quaternion.identity) as Attacker;
        spawnableEnemy.transform.parent = transform;
    }

    public int GetRemainMobSpawnCount()
    {
        InitializeRemainMobSpawnCount();
        return remainMobSpawnCount;
    }
}
