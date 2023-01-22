using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Shooting : MonoBehaviour
{
    public static Shooting Instance;

    private Camera mainCam;
    private Vector3 mousePos;

    public List<GameObject> bulletType;
    private GameObject bullet;
    private Bullet bulletProperties;

    public Transform bulletTransform;


    public bool canFire;

    private float delayFiring;
    [SerializeField]
    private float timeBetweenFiring;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float bulletDamage;
    [SerializeField]
    private float explosiveRadius;

    private float rapidFireTimer;
    private bool rapidFire = false;

    public int numShots = 1;// Number of shots fired;
    public float angle; // Angle between shots

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        ChangeBullet(1);
    }

    public void ChangeBullet(int bulletNr)
    {
        bullet = bulletType[bulletNr];
        bulletProperties = bullet.GetComponent<Bullet>();

        timeBetweenFiring = bulletProperties.timeBetweenFiring;
        bulletSpeed = bulletProperties.Speed;
        angle = bulletProperties.angle;
        bulletDamage = bulletProperties.damage;
        if (bullet.tag == "Explosive")
        {
            explosiveRadius = bullet.GetComponent<CircleCollider2D>().radius;
        }

        FirerateShopUpgrade();
    }

    public void FirerateShopUpgrade()
    {
        if (PlayerPrefs.HasKey("firerateLevel"))
        {
            timeBetweenFiring *= (100 - ((float)PlayerPrefs.GetInt("firerateLevel") * 4)) / 100;
        }
    }

    public void ReduceShootingAngle()
    {
        angle *= 0.95f; // Reduce -angle;angle by 5%
    }

    public void IncreaseDamage()
    {
        bulletDamage *= 1.05f; // Increase damage by 5%
    }

    public void IncreaseFirerate()
    {
        timeBetweenFiring *= 0.95f; // Decrease damage by 5%
    }

    public void IncreaseBulletVelocity()
    {
        bulletSpeed *= 1.05f;
    }

    public void IncreaseExplosiveRadius()
    {
        if (bullet.tag == "Explosive")
        {
            explosiveRadius *= 1.05f;
        }
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float distance = Vector2.Distance(mousePos, transform.position);
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg; // we are rotating over z axis
        transform.rotation = Quaternion.Euler(0, 0, rotZ);


        if (!canFire)
        {
            if (rapidFire)
            {
                rapidFireTimer += Time.deltaTime;
                if (rapidFireTimer > 5.0f)
                {
                    rapidFire = false;
                    rapidFireTimer = 0;
                }

                delayFiring += Time.deltaTime;
                if (delayFiring > timeBetweenFiring / 2) // interval between bullets / 2
                {
                    canFire = true;
                    delayFiring = 0;
                }
            }
            else
            {
                delayFiring += Time.deltaTime;
                if (delayFiring > timeBetweenFiring) // interval between bullets
                {
                    canFire = true;
                    delayFiring = 0;
                }
            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            bulletProperties.PlayBulletSound();

            for (int i = 0; i < numShots; i++)
            {
                GameObject bulletClone = Instantiate(bullet, bulletTransform.transform.position, transform.rotation);
                bulletClone.GetComponent<Bullet>().damage = bulletDamage; //Increase instantiated bullet damage
                if (bullet.tag == "Explosive") //Increase explosion radius
                {
                    bulletClone.GetComponent<CircleCollider2D>().radius = explosiveRadius;
                }
                float spreadAngle = 0;
                if (numShots > 1)
                {
                    spreadAngle = Random.Range(-angle, angle);
                } 

                var x = mousePos.x - bulletClone.transform.position.x;
                var y = mousePos.y - bulletClone.transform.position.y;
                float rotateAngle = spreadAngle + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);

                var MovementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized;

                bulletClone.GetComponent<Rigidbody2D>().velocity = MovementDirection * bulletSpeed;
            }
        }
    }

    public void RapidFirePowerup()
    {
        rapidFire = true;
    }
}
