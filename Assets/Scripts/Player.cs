using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private Rigidbody2D rb;
    public AudioClipGroup fightSound;
    public AudioClipGroup deathSound;
    public AudioClipGroup steppingSounds;
    public AudioClipGroup powerupSounds;
    public AudioClipGroup levelupSounds;


    public float MovementSpeed = 150f;
    public Vector2 movement;
    public Animator animator;

    public bool gamePaused = false;


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

    public float _xp;
    private float _xpneededfornextlevel;

    public float Xp
    {
        get
        {
            return _xp;
        }
        set
        {
            _xp = value;
        }
    }

    public int _level;
    public int Level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
            GameHUD.Instance.SetLevelText(_level);
        }
    }

    public bool InVulnerable;
    private float inVulnerabilityTimer;
    private SpriteRenderer rend;

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

    public float XpNeededForNextLevel
    {
        get
        {
            return _xpneededfornextlevel;
        }
        set
        {
            _xpneededfornextlevel = value;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // for invulnerabilty powerup
        rend = GetComponent<SpriteRenderer>();

        Score = 0;
        if (PlayerPrefs.HasKey("speedLevel"))
        {
            MovementSpeed += (float) PlayerPrefs.GetInt("speedLevel") * 10f;
        }

        fightSound?.Play();
    }
    private void Awake()
    {
        Instance = this;
        Health = 1f;
        Armor = 0f;
        Xp = 0f;
        Level = 0;
        XpNeededForNextLevel = 100;
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
            steppingSounds?.Play();
        }

        if (InVulnerable)
        {
            inVulnerabilityTimer += Time.deltaTime;
            if (inVulnerabilityTimer > 10f)
            {
                InVulnerable = false;
                inVulnerabilityTimer = 0;
                // change back to normal
                rend.color = new Color (1, 1, 1 ,1);
            }
        }
        
        if (XpNeededForNextLevel <= Xp)
        {
            Level += 1;
            levelupSounds?.Play();
            GameHUD.Instance.PauseGame(true);
            // Xp overflowi jaoks
            Xp = Xp - XpNeededForNextLevel;
            XpNeededForNextLevel = XpNeededForNextLevel * 2;
        }
        //Skoori ja xp bari pidev uuendamine
        GameHUD.Instance.SetXp(Xp, XpNeededForNextLevel);

        if (Input.GetKeyDown(KeyCode.Escape)) //Mängu pausilepanek
        {
            if (!gamePaused)
            {
                GameHUD.Instance.PauseGame(false);
            }
            else
            {
                GameHUD.Instance.ResumeGame(false);
            }
            gamePaused = !gamePaused;
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
        animator.SetBool("IsDead", true);
        deathSound?.Play();
        Invoke("WaitTwoSeconds", 2f);
    }

    public void IncreaseMovementSpeed()
    {
        MovementSpeed *= 1.1f; //Increase movementspeed by 10%
    }

    private void WaitTwoSeconds()
    {
        GameHUD.Instance.ShowLosePanel();
    }

    // Powerupide jaoks
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Armor"))
        {
            powerupSounds?.Play();
            Armor = 1f;            
            GameObject.Destroy(collision.gameObject);
        }

        else if (collision.gameObject.name.Contains("RapidFire"))
        {
            powerupSounds?.Play();
            Shooting shootingScript = GameObject.FindGameObjectWithTag("RotatePoint").GetComponent<Shooting>();
            shootingScript.RapidFirePowerup();
            GameObject.Destroy(collision.gameObject);
        }

        else if (collision.gameObject.name.Contains("Invulnerability"))
        {
            powerupSounds?.Play();
            InVulnerable = true;
            // make a bit transparent           
            rend.color = new Color (1, 1, 1, 0.5f);
            GameObject.Destroy(collision.gameObject);
        }

        else if (collision.gameObject.name.Contains("Instakill"))
        {
            powerupSounds?.Play();
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyStats>().Hit(9999999);
            }
            GameObject.Destroy(collision.gameObject);
        }

        else if (collision.gameObject.name.Contains("CoinPowerup"))
        {
            powerupSounds?.Play();
            Score += 2000f;
            GameObject.Destroy(collision.gameObject);
        }

        else if (collision.gameObject.name.Contains("HealthRefill"))
        {
            powerupSounds?.Play();
            Health = 1f;
            GameObject.Destroy(collision.gameObject);
        }


        else if (collision.gameObject.name.Contains("XPBoost"))
        {
            powerupSounds?.Play();
            Xp += XpNeededForNextLevel;
            GameObject.Destroy(collision.gameObject);
        }
    }
}
