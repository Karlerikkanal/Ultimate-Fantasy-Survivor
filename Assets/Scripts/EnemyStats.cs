using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health; 
    public float speed; //LINK TO ENEMYMOVEMENT MOVEMENTSPEED
    public float damage;
    public float droprate;
    public GameObject[] powerups;
    private EnemySpawner spawner;
    public AudioClipGroup hitSounds;
    public AudioClipGroup deathSound;
    private float score;
    // enemymovement
    public EnemyMovement movement;

    void Start()
    {
        spawner = EnemySpawner.Instance;
        float currentWave = (float)spawner.currentWave / 10; //Scaling here, change the number value to change scaling, Smaller number is tougher scaling, higher is easier scaling
        health += currentWave;
        speed += currentWave / 2;
        damage += currentWave;
        score = health * 10;
        // enemymovement 
        movement = GetComponent<EnemyMovement>();
    }
    


    void Update()
    {
     
    }

    public void Hit(float damage)
    {
        health -= damage;
        hitSounds?.Play();
        if (health <= 0)
        {
            deathSound?.Play();
            GetComponent<EnemyMovement>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            movement.isDead();
            Destroy(gameObject, 1f);
            Vector3 coords = gameObject.transform.position;
            GameObject.Destroy(gameObject);
            GenerateDrop(coords);
            spawner.enemiesAlive--;
            Player.Instance.Score = Player.Instance.Score + score;
            Player.Instance.Xp = Player.Instance.Xp + score;
        }
    }

    public void GenerateDrop(Vector3 pos)
    {
        float threshold = Random.Range(0f, 100f);
        // Kui droprate nt 30 ja genereeritud arv on 29, siis tee drop
        if (threshold <= droprate)
        {
            int generator = (int) Random.Range(0, powerups.Length);
            GameObject powerup = powerups[generator];
            Instantiate(powerup, pos, Quaternion.identity);
        }
        else return;
    }
}
