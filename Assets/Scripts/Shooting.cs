using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    private float rapidFireTimer;
    private float rapidFireSpeed;
    private bool rapidFire = false;
    private float rightSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }

    }

    public void rapidFirePowerup()
    {
        rapidFireTimer += Time.deltaTime;
        rapidFireSpeed = timeBetweenFiring / 2;
        rightSpeed = timeBetweenFiring;
        timeBetweenFiring = rapidFireSpeed;
        rapidFire = true;





        //Debug.Log("rapidfirepowerup initiated");
        //float timer2 = Time.deltaTime;
        //float rightSpeed = timeBetweenFiring;
        //float newSpeed = timeBetweenFiring / 2;
        

        //while (timer2 < 5.00)
        //{
        //    timeBetweenFiring = newSpeed;
        //}
        //Debug.Log("rapidfirePowerup stopped");
        //timeBetweenFiring = rightSpeed;
        //Debug.Log("outofthemethod");
    }
}
