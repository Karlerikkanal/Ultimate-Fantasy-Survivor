using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float Speed = 10f;
    public float damage = 1f;
    public float timeBetweenFiring = 0.5f;
    public float angle;
    public ExplosionParticle ParticlePrefab;
    private CircleCollider2D circleCollider2D;

    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
        if (enemy != null)
        {
            enemy.Hit(damage);
            GameObject.Instantiate(ParticlePrefab, transform.position, Quaternion.identity);
            GameObject.Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(gameObject);
    }
}
