using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health;
    public float speed; //LINK TO ENEMYMOVEMENT MOVEMENTSPEED
    public float damage;
    private EnemySpawner spawner;
    private AudioSource audioSource;
    private float score;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        spawner = EnemySpawner.Instance;
        float currentWave = (float)spawner.currentWave / 10; //Scaling here, change the number value to change scaling, Smaller number is tougher scaling, higher is easier scaling
        health += currentWave;
        speed += currentWave / 2;
        damage += currentWave;
        score = health * 10;
    }
    


    void Update()
    {
        
    }

    public void Hit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            audioSource.Play();
            GameObject.Destroy(gameObject);
            spawner.enemiesAlive--;
            Player.Instance.Score = Player.Instance.Score + score;
            Player.Instance.Xp = Player.Instance.Xp + score;
        }
    }


}
