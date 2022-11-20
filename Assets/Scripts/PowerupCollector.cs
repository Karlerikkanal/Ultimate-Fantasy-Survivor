using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCollector : MonoBehaviour
{
    public AudioClipGroup powerupSounds;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name.Contains("RapidFire"))
        {
            //Debug.Log("Collected Fire Powerup");
            //Shooting shootingScript = GameObject.FindGameObjectWithTag("RotatePoint").GetComponent<Shooting>();
            //Debug.Log("Bullet interval before colliding: " + shootingScript.timeBetweenFiring);
            //shootingScript.timeBetweenFiring = shootingScript.timeBetweenFiring / 2;
            //Debug.Log("Bullet speed after colliding: " + shootingScript.timeBetweenFiring);
            //GameObject.Destroy(collision.gameObject);
            powerupSounds?.PlayAtIndex(1);
            Shooting shootingScript = GameObject.FindGameObjectWithTag("RotatePoint").GetComponent<Shooting>();
            Debug.Log("Bullet interval before colliding: " + shootingScript.timeBetweenFiring);
            shootingScript.rapidFirePowerup();
            Debug.Log("Bullet speed after colliding: " + shootingScript.timeBetweenFiring);
            GameObject.Destroy(collision.gameObject);

        }



    }
}
