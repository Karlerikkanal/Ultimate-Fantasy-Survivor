using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float Speed = 10f;
    public float damage = 1f;
    public float timeBetweenFiring = 0.5f;
    public float angle;
    public ExplosionParticle ParticlePrefab;
    public AudioClipGroup hitSound;
    private CircleCollider2D circleCollider2D;
    public bool isExplosive;


    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
                
        if (enemy != null)
        {
            if (isExplosive)
            {
                //Saa kõik collisionid
                Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, circleCollider2D.radius);

                // Kahjusta kõiki kes piirides on
                foreach (Collider2D collider in collisions)
                {
                    EnemyStats enemy1 = collider.gameObject.GetComponent<EnemyStats>();
                    if (enemy1 != null) {
                        Debug.Log("Kahjustan vastast!");
                        hitSound?.Play();
                        enemy1.Hit(damage); 
                    }
                }
                GameObject.Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
                GameObject.Destroy(gameObject);
            }
            else
            {
                enemy.Hit(damage);
                GameObject.Destroy(gameObject);
            }     
        }
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(gameObject);
    }

}
