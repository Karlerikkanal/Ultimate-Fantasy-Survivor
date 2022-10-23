using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    
    public List<EnemyGeneric> enemies = new List<EnemyGeneric>();
    public int currentWave;
    public float waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    private GameObject player;

    public Transform[] spawnLocations;
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
    public int enemiesAlive = 0;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GenerateWave();
        player = GameObject.FindWithTag("Player");
    }

    
    void FixedUpdate()
    {
        float radius = 10f; //Enemy spawnlocations floating around player
        int amountSpawners = spawnLocations.Length;
        for (int i = 0; i < amountSpawners; i++)
        {
            float angle = i * Mathf.PI * 2f / amountSpawners;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            spawnLocations[i].transform.position = newPos + player.transform.position;
        }

        transform.position = player.transform.position; //Enemyspawner object on player
        if (waveTimer < 0)
        {
            currentWave++;
            GenerateWave();
        }
        if (spawnTimer < 0 && enemiesToSpawn.Count != 0) // Spawn Enemy if possible
        {
            if (enemiesToSpawn.Count > 0 && enemiesAlive < 100) //
            {
                Instantiate(enemiesToSpawn[0], spawnLocations[Random.Range(0,10)].position, Quaternion.identity);
                enemiesToSpawn.RemoveAt(0);
                enemiesAlive++;
                spawnTimer = spawnInterval;
            }
        }
        else // Move on with wave
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
    }

    public void GenerateWave()
    {
        waveValue = currentWave + 5;
        
        GenerateEnemies();

        spawnInterval = 0.2f;
        waveTimer = waveDuration;
    }

    public void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while(waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;
            if(waveValue-randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }
}

[System.Serializable]
public class EnemyGeneric
{
    public GameObject enemyPrefab;
    public int cost;
}
