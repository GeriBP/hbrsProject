using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 0.1f;
    public float attackRange = 1;
    public float attackCooldown = 1;

    private Transform playerTransform;
    private Rigidbody2D ridgidbody;
    private bool canAttack = true;

    private Vector3 direction = Vector3.left;

    // Use this for initialization
    void Start()
    {
        this.playerTransform = GameObject.Find("playerBody").transform;
        this.ridgidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Chase player
        bool canSeePlayer = !Physics2D.Linecast(this.transform.position, this.playerTransform.position, 1 << LayerMask.NameToLayer("Ground"));
        if (canSeePlayer)
            this.direction = (this.playerTransform.position - this.transform.position).normalized;

        bool grounded = Physics2D.Linecast(this.transform.position, this.transform.position + Vector3.down, 1 << LayerMask.NameToLayer("Ground"));
        bool canFly = this.GetComponent<Rigidbody2D>().gravityScale == 0;
        if (Vector3.Distance(this.transform.position, this.playerTransform.position) > 1 + this.attackRange)
        {
            // Jump random
            if (!canFly && grounded && Random.value > 0.99)
                this.Jump();

            // Move
            if (!canFly)
                this.direction = this.direction.x > 0 ? Vector3.right : Vector3.left;
            Vector3 movement = this.direction * this.speed * Time.deltaTime;
            if (!(grounded || canFly)) movement *= 0.5f;
            this.transform.position += movement;
        }
        else
        {
            // TODO check if player is on side (not on top or below)
            if ((grounded || canFly) && this.canAttack)
            {
                this.canAttack = false;
                Invoke("ResetAttack", this.attackCooldown);
                this.Attack();
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
        Health health = this.playerTransform.GetComponent<Health>();
        health.Adjust(-10); 
    }

    private void ResetAttack()
    {
        this.canAttack = true;
    }
}