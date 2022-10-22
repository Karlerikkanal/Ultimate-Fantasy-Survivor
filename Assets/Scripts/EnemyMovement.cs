using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    public float MovementSpeed = 1f;
    public Vector2 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        movement = direction;
    }
    void FixedUpdate()
    {
        moveEnemy(movement);
    }

    void moveEnemy(Vector3 direction)
    {
        rb.MovePosition(transform.position + (direction * MovementSpeed * Time.deltaTime));
    }

}
