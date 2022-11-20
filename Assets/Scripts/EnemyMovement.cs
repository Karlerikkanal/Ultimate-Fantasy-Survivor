using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    public float MovementSpeed = 1f;
    public Vector2 movement;

    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = Player.Instance.gameObject;
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        movement = direction;
        anim.SetFloat("xMov", movement.normalized.x);
        anim.SetFloat("yMov", movement.normalized.y);
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
