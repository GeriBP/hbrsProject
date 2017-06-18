﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Attack")]
    public float attackRange = 1;
    public float attackCooldown = 1;

    private Transform playerTransform;
    private bool canAttack = true;

    private Vector3 movementDirection = Vector3.left;

    // Use this for initialization
    new void Start()
    {
        base.Start();

        this.playerTransform = GameObject.Find("Player").transform;
    }

    new void Update()
    {
        base.Update();

        // Chase player
        bool canSeePlayer = !Physics2D.Linecast(this.transform.position, this.playerTransform.position, 1 << LayerMask.NameToLayer("Ground"));
        if (canSeePlayer)
            this.movementDirection = this.playerTransform.position - this.transform.position;
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        
        if (Vector3.Distance(this.transform.position, this.playerTransform.position) > 1 + this.attackRange)
        {
            this.Move(this.movementDirection.normalized, false, this.grounded && Random.value > 0.99);
        }
        else
        {
            // TODO check if player is on side (not on top or below)
            if (this.canAttack)
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
            this.movementDirection *= -1;
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