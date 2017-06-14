using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 0.1f;
    public float attackCooldown = 1;

    private Transform playerTransform;
    private Rigidbody2D ridgidbody;
    private float attackTimer;

    private Vector3 direction = Vector3.left;

    // Use this for initialization
    void Start()
    {
        this.playerTransform = GameObject.Find("player").transform;
        this.ridgidbody = this.GetComponent<Rigidbody2D>();
        this.attackTimer = this.attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        // Attack cooldown
        this.attackTimer -= Time.deltaTime;
        if (this.attackTimer < 0)
            this.attackTimer = 0;

        // Chase player
        bool canSeePlayer = Physics2D.Linecast(this.transform.position, this.playerTransform.position, 1 << LayerMask.NameToLayer("Ground"));
        if (!canSeePlayer)
            this.direction = this.playerTransform.position.x - this.transform.position.x > 0 ? Vector3.right : Vector3.left;

        bool grounded = Physics2D.Linecast(this.transform.position, this.transform.position + Vector3.down, 1 << LayerMask.NameToLayer("Ground"));
        if (Vector3.Distance(this.transform.position, this.playerTransform.position) > 2)
        {
            // Jump random
            if (grounded && Random.value > 0.99)
                this.Jump();

            // Move
            Vector3 movement = this.direction * this.speed * Time.deltaTime;
            if (!grounded) movement *= 0.5f;
            this.transform.position += movement;
        }
        else
        {
            // TODO check if player is on side (not on top or below)
            if (grounded && attackTimer == 0)
            {
                this.Attack();
                this.attackTimer = this.attackCooldown;
            }
        }

        // TODO handle localScale.x to control sprite orientation
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 dir = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0) - this.transform.position;
        if (collision.gameObject.tag != "Player" && dir.x != 0)
            this.direction *= -1;
    }

    private void Jump()
    {
        this.ridgidbody.velocity = new Vector2(0, 10);
    }

    private void Attack()
    {
        Debug.Log("Attack");
        Health health = this.playerTransform.GetComponent<Health>();
        health.Adjust(-10);
    }
}