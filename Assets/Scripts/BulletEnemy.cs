using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float Speed = 10f;
    public float damage = 0.1f;
    private Rigidbody2D rb;
    
    void Start()
    {
        Vector3 player = Player.Instance.transform.position;
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = player - transform.position;
        // rotation for the bullet
        Vector3 rotation = transform.position - player;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * Speed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (!player.InVulnerable)
            {
                if (Player.Instance.Armor > 0)
                {
                    Player.Instance.Armor -= damage;
                }
                else
                {
                    Player.Instance.Health -= damage;
                }
                //Player.Instance.Health -= damage;
                GameObject.Destroy(gameObject);
            }
            /*
            if (Player.Instance.Armor > 0)
            {
                Player.Instance.Armor -= damage;
            }
            else
            {
                Player.Instance.Health -= damage;
            } 
            //Player.Instance.Health -= damage;
            GameObject.Destroy(gameObject);
            */
        }
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(gameObject);
    }
}
