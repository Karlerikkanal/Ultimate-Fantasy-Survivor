using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyCollision : MonoBehaviour
{
    private float damageDelay = 0.1f;
    private float nextHitTime;

    public AudioClipGroup hitSounds;

    private void Awake()
    {
        nextHitTime = Time.time;
    }

    private void Update()
    {
        transform.position = Player.Instance.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
        if (enemy != null)
        {
            if (!Player.Instance.InVulnerable)
            {
                if (Time.time >= nextHitTime)
                {
                    hitSounds?.Play();

                    if (Player.Instance.Armor > 0)
                    {
                        Player.Instance.Armor -= enemy.damage / 100;
                    }
                    else
                    {
                        Player.Instance.Health -= enemy.damage / 100;
                    }
                    nextHitTime = Time.time;
                    nextHitTime += damageDelay;
                    if (Player.Instance.Health <= 0)
                    {
                        Player.Instance.isDead();
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
