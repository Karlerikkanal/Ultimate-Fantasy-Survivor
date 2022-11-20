using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private Rigidbody2D rb;
    public AudioClipGroup deathSounds;
    public AudioClipGroup hitSounds;
    public AudioClipGroup steppingSounds;
    //public AudioClip hitSound;
    //public AudioClip stepSound;
    //private AudioSource audioSource;
    //public AudioClip deathsound;

    public float MovementSpeed = 150f;
    public Vector2 movement;
    public Animator animator;

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

    private float regenDelay = 3f;
    private float nextRegenTick;

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
        //audioSource = GetComponent<AudioSource>();
        Score = 0;
        if (PlayerPrefs.HasKey("speedLevel"))
        {
            MovementSpeed += (float) PlayerPrefs.GetInt("speedLevel") * 10f;
        }
    }
    private void Awake()
    {
        Instance = this;
        Health = 1f;
        nextHitTime = Time.time;
        nextRegenTick = Time.time;
    }


    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (PlayerPrefs.HasKey("healthLevel") && Time.time > nextRegenTick)
        {
            if (Health < 1f)
            {
                Health += (float)PlayerPrefs.GetInt("healthLevel") / 100f;
                nextRegenTick += regenDelay;
            }
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            //steppingSounds?.Play(audioSource);
            //audioSource.PlayOneShot(stepSound);
        }
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
                hitSounds?.Play();
                //audioSource.PlayOneShot(hitSound);
                Health -= enemy.damage / 100;
                nextHitTime += damageDelay;
                if (Health <= 0)
                {
                    //deathSounds?.Play();
                    isDead();
                }
            }
        }
    }



}
