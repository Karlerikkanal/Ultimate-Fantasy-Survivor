using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    public float MovementSpeed = 1f;
    public Vector2 movement;
    public EnemyStats stats;

    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        anim = GetComponent<Animator>();
        stats = GetComponent<EnemyStats>();
    }
    private void Start()
    {
        MovementSpeed = stats.speed;
        player = Player.Instance.gameObject;
    }

    void FixedUpdate()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        movement = direction;
        anim.SetFloat("xMov", movement.normalized.x);
        anim.SetFloat("yMov", movement.normalized.y);
        moveEnemy(movement);
    }

    void moveEnemy(Vector3 direction)
    {
        rb.MovePosition(transform.position + (direction * MovementSpeed * Time.deltaTime));
    }

}
