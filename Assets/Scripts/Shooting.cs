using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public List<GameObject> bullet;
    public Transform bulletTransform;
    public AudioClipGroup arrowSounds;
    private AudioSource audioSource;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    private float rapidFireTimer;
    private float rapidFireSpeed;
    private bool rapidFire = false;
    private float rightSpeed;
    private bool machineGun = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("firerateLevel"))
        {
            timeBetweenFiring -= ((float)PlayerPrefs.GetInt("firerateLevel") * 0.04f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (rapidFire)
        {
            rapidFireTimer += Time.deltaTime;
            if (rapidFireTimer > 5.00)
            {
                rapidFire = false;
                timeBetweenFiring = rightSpeed;
                rapidFireTimer = 0;
            }
        }
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg; // we are rotating over z axis

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime; 
            if (timer > timeBetweenFiring) // interval between bullets
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            arrowSounds?.Play(audioSource);
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            if (machineGun)
            {
                Instantiate(bullet[1], bulletTransform.position, Quaternion.identity);
            }
            else Instantiate(bullet[0], bulletTransform.position, Quaternion.identity);
        }

    }

    public void MachineGunShooting()
    {
        if (rapidFire)
        {
            timeBetweenFiring = rightSpeed * 0.6f;
            rightSpeed = timeBetweenFiring;
            Debug.Log("timebetweenfiring is" + timeBetweenFiring);
        }
        else timeBetweenFiring = timeBetweenFiring * 0.6f;
        Debug.Log("We are changing the fire rate!");
        machineGun = true;

    }

    public void rapidFirePowerup()
    {
        rapidFireTimer += Time.deltaTime;
        rapidFireSpeed = timeBetweenFiring / 2;
        rightSpeed = timeBetweenFiring;
        Debug.Log("Rightspeed is" + rightSpeed);
        timeBetweenFiring = rapidFireSpeed;
        rapidFire = true;
    }
}
