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
    public AudioClipGroup powerupSounds;
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

    public float _armor;
    public float Armor
    {
        get
        {
            return _armor;
        }
        set
        {
            _armor = Mathf.Clamp(value, 0f, 1f);
            GameHUD.Instance.SetArmor(_armor);
        }
    }

    public bool InVulnerable;
    private float inVulnerabilityTimer;
    private SpriteRenderer rend;

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
        // for invulnerabilty powerup
        rend = GetComponent<SpriteRenderer>();

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
        Armor = 0f;
        nextHitTime = Time.time;
        nextRegenTick = Time.time;
        InVulnerable = false;
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
                Health += (float)PlayerPrefs.GetInt("healthLevel") / 200f;
                nextRegenTick = Time.time;
                nextRegenTick += regenDelay;
            }
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            //steppingSounds?.Play(audioSource);
            //audioSource.PlayOneShot(stepSound);
        }

        if (InVulnerable)
        {
            inVulnerabilityTimer += Time.deltaTime;
            if (inVulnerabilityTimer > 10.00)
            {
                InVulnerable = false;
                inVulnerabilityTimer = 0;
                // change back to normal
                rend.color = new Color (1, 1, 1 ,1);
            }
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

    public void isDead()
    {
        GameHUD.Instance.ShowLosePanel();
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
        if (enemy != null)
        {
            if (!InVulnerable)
            {
                if (Time.time >= nextHitTime)
                {
                    hitSounds?.Play();
                    //audioSource.PlayOneShot(hitSound);
                    if (Armor > 0)
                    {
                        Armor -= enemy.damage / 100;
                    }
                    else
                    {
                        Health -= enemy.damage / 100;
                    }
                    //Health -= enemy.damage / 100;
                    nextHitTime = Time.time;
                    nextHitTime += damageDelay;
                    if (Health <= 0)
                    {
                        //deathSounds?.Play();
                        isDead();
                    }
                }
            }
            /*
            if (Time.time >= nextHitTime)
            {
                hitSounds?.Play();
                //audioSource.PlayOneShot(hitSound);
                if (Armor > 0)
                {
                    Armor -= enemy.damage / 100;
                }
                else
                {
                    Health -= enemy.damage / 100;
                }
                //Health -= enemy.damage / 100;
                nextHitTime += damageDelay;
                if (Health <= 0)
                {
                    //deathSounds?.Play();
                    isDead();
                }
            }
            */
        }
    }

    // Powerupide jaoks
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Armor"))
        {
            Debug.Log("playerscriptis armor korjatud");
            powerupSounds?.PlayAtIndex(1);
            Armor = 1f;            
            GameObject.Destroy(collision.gameObject);
        }

        else if (collision.gameObject.name.Contains("RapidFire"))
        {
            powerupSounds?.PlayAtIndex(1);
            Shooting shootingScript = GameObject.FindGameObjectWithTag("RotatePoint").GetComponent<Shooting>();
            Debug.Log("Bullet interval before colliding: " + shootingScript.timeBetweenFiring);
            shootingScript.rapidFirePowerup();
            Debug.Log("Bullet speed after colliding: " + shootingScript.timeBetweenFiring);
            GameObject.Destroy(collision.gameObject);
        }

        else if (collision.gameObject.name.Contains("Invulnerability"))
        {
            powerupSounds?.PlayAtIndex(1);
            Debug.Log("playerscriptis invulnerability korjatud");
            InVulnerable = true;
            // make a bit transparent           
            rend.color = new Color (1, 1, 1, 0.5f);
            Debug.Log("peaks olema invulnerable");
            GameObject.Destroy(collision.gameObject);
        }

        else if (collision.gameObject.name.Contains("Instakill"))
        {
            powerupSounds?.PlayAtIndex(1);
            Debug.Log("playerscriptis Instakill korjatud");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyStats>().Hit(9999999);
            }
            GameObject.Destroy(collision.gameObject);
        }
    }
}
