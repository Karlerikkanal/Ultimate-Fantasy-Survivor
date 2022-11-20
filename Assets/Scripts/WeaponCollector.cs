using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "SMG")
        {
            Shooting shootingScript = GameObject.FindGameObjectWithTag("RotatePoint").GetComponent<Shooting>();
            shootingScript.MachineGunShooting();
            GameObject.Destroy(collision.gameObject);
            //Debug.Log("Bullet interval before colliding: " + shootingScript.timeBetweenFiring);
            //shootingScript.rapidFirePowerup();
            //Debug.Log("Bullet speed after colliding: " + shootingScript.timeBetweenFiring);
            //GameObject.Destroy(collision.gameObject);

        }
    }
}
