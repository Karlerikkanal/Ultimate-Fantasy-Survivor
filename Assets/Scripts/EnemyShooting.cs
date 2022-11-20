using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public BulletEnemy EnemyBulletPrefab;
    public float BulletDelay = 2f;
    private float NextSpawnTime;
    private void Awake()
    {
        NextSpawnTime = Time.time;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time >= NextSpawnTime)
        {
            BulletEnemy enemybullet = GameObject.Instantiate<BulletEnemy>(EnemyBulletPrefab, transform.position, Quaternion.identity, null);
            NextSpawnTime += BulletDelay;
        }
    }
}
