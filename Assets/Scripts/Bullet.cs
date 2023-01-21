using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 10f;
    public float damage = 1f;
    public float timeBetweenFiring = 0.5f;
    public float angle;
    public ExplosionParticle ParticlePrefab;
    public AudioClipGroup hitSound;
    private CircleCollider2D circleCollider2D;

    public AudioClipGroup bulletSound;


    private void Awake()
    {
        if (tag == "Explosive") circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
        if (enemy != null)
        {
            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, circleCollider2D.radius);
            int count = 0;
            foreach (Collider2D collider in collisions)
            {
                EnemyStats enemy1 = collider.gameObject.GetComponent<EnemyStats>();
                if (enemy1 != null)
                {
                    count++;
                    hitSound?.Play();
                    enemy1.Hit(damage);
                }
            }
            Debug.Log(count);
            GameObject.Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
            GameObject.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tag != "Explosive")
        {
            EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();

            if (enemy != null)
            {
                enemy.Hit(damage);
                if (tag != "Laser")
                {
                    GameObject.Destroy(gameObject);
                }
            }
        }
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(gameObject);
    }

    public void PlayBulletSound()
    {
        bulletSound?.Play();
    }
}
