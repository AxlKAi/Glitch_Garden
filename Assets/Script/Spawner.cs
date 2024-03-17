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

    private float _firstPlayIdleSFXDelay = 3f;

    [SerializeField]
    private float _playIdleSFXDelayPeriod = 12f;
    private float _idleSFXDelayRandomCoefficient = 1f;

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
        StartCoroutine(PlayEnemyIdleSoundFirstTime());
    }

    IEnumerator StartSpawner(int wave_num)
    {
        SpawnList attList = attakers[wave_num-1];
        yield return StartCoroutine(SpawnOneWave(attList));
        gameState.SetEndOfWave(true);
        gameState.IncEmptySpawner();
    }

    // IEnumerator PlayEnemyIdleSound
    // первый раз проигрывает звук 1-3 секунды после спавнв первого моба
    // потом раз в 5-7 секунд проигрывает в зависимости от приоритета
    // крокодил - 1 лиса - 2 и т.д.
    // как расчитать приоритет ??
    // Сделаем массив из типов врагов, и приоритета
    // Приоритеты это int, они складываются, и затем рандомайзер выбирает число определяющее победителя
    // Например, крокодил - 1, лиса - 3, краб 5. сумма всех чисел = 9
    // Рандом от 1 до 9.  1 - крокодил. 2 - 4 лиса, 5-9 краб!


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
        spawnableEnemy.PlayIdleSFX();
    }

    public int GetRemainMobSpawnCount()
    {
        InitializeRemainMobSpawnCount();
        return remainMobSpawnCount;
    }

    IEnumerator PlayEnemyIdleSoundFirstTime()
    {
        yield return new WaitForSeconds(_firstPlayIdleSFXDelay + startSpawnerDelay);

        Attacker child;
        transform.GetChild(0).TryGetComponent<Attacker>(out child);

        if (child != null)
            child.PlayIdleSFX();

        StartCoroutine(PlayEnemyIdleSoundContiniusly());
    }

    IEnumerator PlayEnemyIdleSoundContiniusly()
    {
        while (true)
        {
            float deltaTime = Random.Range(0, _idleSFXDelayRandomCoefficient);
                
            yield return new WaitForSeconds(_playIdleSFXDelayPeriod + deltaTime);

            int childCount = transform.childCount;

            if(childCount > 0)
            {
                Attacker child;
                int randomChild = Random.Range(0, childCount);
                transform.GetChild(randomChild).TryGetComponent<Attacker>(out child);
                child.PlayIdleSFX();
            }
        }
    }

}
