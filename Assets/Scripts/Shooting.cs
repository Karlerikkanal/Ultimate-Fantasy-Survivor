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

    public Transform bulletTransform;

    public AudioClipGroup arrowSounds;

    public bool canFire;

    private float delayFiring;
    [SerializeField]
    private float timeBetweenFiring;
    private float bulletSpeed;

    private float rapidFireTimer;
    private bool rapidFire = false;

    public int numShots = 1;// Number of shots fired;
    public float angle; // Angle between shots

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        //Setting bullet properties for shooting
        bullet = bulletType[1];
        Bullet bulletProperties = bullet.GetComponent<Bullet>();

        timeBetweenFiring = bulletProperties.timeBetweenFiring;
        bulletSpeed = bulletProperties.Speed;
        angle = bulletProperties.angle;

        if (PlayerPrefs.HasKey("firerateLevel"))
        {
            timeBetweenFiring *= (100 - ((float)PlayerPrefs.GetInt("firerateLevel") * 4))/100;
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
            arrowSounds?.Play();

            for (int i = 0; i < numShots; i++)
            {
                GameObject bulletClone = Instantiate(bullet, bulletTransform.transform.position, transform.rotation);
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

    public void rapidFirePowerup()
    {
        rapidFire = true;
    }
}
