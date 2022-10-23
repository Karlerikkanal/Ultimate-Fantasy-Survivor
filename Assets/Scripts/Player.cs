using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private Rigidbody2D rb;
    public float MovementSpeed = 150f;
    public Vector2 movement;

    public float _health;
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = Mathf.Clamp(value, 0f, 1f);
            GameHUD.Instance.SetHealth(_health);
        }
    }
    private float damageDelay = 0.1f;
    private float nextHitTime;

    private float _score;
    public float Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            GameHUD.Instance.SetScore(_score);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Score = 0;
    }
    private void Awake()
    {
        Instance = this;
        Health = 1f;
        nextHitTime = Time.time;
    }


    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    void FixedUpdate()
    {
        movePlayer(movement);
    }

    void movePlayer(Vector2 direction)
    {
        rb.velocity = (direction*MovementSpeed * Time.deltaTime);
    }

    private void isDead()
    {
        GameHUD.Instance.ShowLosePanel();
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
        if (enemy != null)
        {
            if (Time.time >= nextHitTime)
            {
                Health -= enemy.damage / 100;
                nextHitTime += damageDelay;
                if (Health <= 0)
                {
                    isDead();
                }
            }
        }
    }



}
