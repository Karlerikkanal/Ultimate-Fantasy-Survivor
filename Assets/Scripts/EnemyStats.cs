using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float health;
    public float speed; //LINK TO ENEMYMOVEMENT MOVEMENTSPEED
    public float damage;
    private EnemySpawner spawner;
    void Start()
    {

    }
    private void Awake()
    {
        spawner = EnemySpawner.Instance;
        float currentWave = (float) spawner.currentWave / 10; //Scaling here, change the number value to change scaling
        health = 0.7f + currentWave;
        speed = 1 + currentWave;
        damage = 1 + currentWave;
        
    }


    void Update()
    {
        
    }

    public void Hit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameObject.Destroy(gameObject);
            spawner.enemiesAlive--;
            
            //Add score siia: score oleks nt health * 10
        }
    }


}
