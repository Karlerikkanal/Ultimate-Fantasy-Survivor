using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDead : MonoBehaviour
{
    private float damage = 1000000f;
    private bool activated;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                activated = false;
                timer = 0f;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("triggerstay alldeadis");
        EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();
        Debug.Log("enemy" + enemy);
        if (activated)
        {
            Debug.Log("VAHEPEATUS");
            if (enemy != null)
        {
                enemy.Hit(damage);
                Debug.Log("jõuame siia üldse?");
                //GameObject.Destroy(gameObject);
            }
        }
        
    }

    public void Activate()
    {
        activated = true;
        Debug.Log("aktiveeritud");
    }
}
