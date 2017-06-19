using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public float speed = 0.1f;
    public float attackRange = 1;
    public float attackCooldown = 1;

    private Transform playerTransform;
    private bool canAttack = true;

    private Vector3 direction = Vector3.left;

    // Use this for initialization
    new void Start()
    {
        base.Start();

        this.playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        // Chase player
        //bool canSeePlayer = !Physics2D.Linecast(this.transform.position, this.playerTransform.position, 1 << LayerMask.NameToLayer("Ground"));
        //if (canSeePlayer)
        //    this.direction = (this.playerTransform.position - this.transform.position).normalized;

        //bool canFly = this.GetComponent<Rigidbody2D>().gravityScale == 0;
        //if (Vector3.Distance(this.transform.position, this.playerTransform.position) > 1 + this.attackRange)
        //{
        //    // Jump random
        //    if (!canFly && grounded && Random.value > 0.99)
        //        this.Jump();

        //    // Move
        //    if (!canFly)
        //        this.direction = this.direction.x > 0 ? Vector3.right : Vector3.left;
        //    Vector3 movement = this.direction * this.speed * Time.deltaTime;
        //    if (!(grounded || canFly)) movement *= 0.5f;
        //    this.transform.position += movement;
        //}
        //else
        //{
        //    // TODO check if player is on side (not on top or below)
        //    if ((grounded || canFly) && this.canAttack)
        //    {
        //        this.canAttack = false;
        //        Invoke("ResetAttack", this.attackCooldown);
        //        this.Attack();
        //    }
        //}

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
        this.rigidbody.velocity = new Vector2(0, 10);
    }

    private void Attack()
    {
        Player player = this.playerTransform.GetComponent<Player>();
        player.AdjustHealth(-10); 
    }

    private void ResetAttack()
    {
        this.canAttack = true;
    }
}