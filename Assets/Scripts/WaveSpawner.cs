using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;

    }

    public Wave[] waves;
    private int nextWave = 0;
    public int NextWave {
        get { return nextWave; }
    }

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;
    public float WaveCountdown {
        get { return waveCountdown; }
    }


    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    
    public SpawnState State {
        get { return state; }
    }

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {

        if (state == SpawnState.WAITING )
        {
            //check if enemies are still alive
            if (!EnemyIsAlive()) //enemies are dead
            {
                //begin a new round
                WaveCompleted();
               

            }
            else //enemies are still alive
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                // Start spawning wave
                if(nextWave < waves.Length) {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            }
           
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1  > waves.Length - 1)
        {
            Debug.Log("ALL WAVES COMPLETE! Looping...");
            GetComponent<WaveSpawner>().enabled = false;
            GameObject.FindGameObjectWithTag("WaveUi").GetComponent<WaveUI>().enabled = false;
            return;
        }
        nextWave++;
    }

    bool EnemyIsAlive ()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        // spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        
        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {
        //spawn enemy
        //Debug.Log("Spawning Enemy: " + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
        
    }

}
