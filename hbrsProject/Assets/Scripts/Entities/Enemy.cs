using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private Vector3 movementDirection = Vector3.left;

    new void Start()    {
        base.Start();

        this.aimingTarget = GameObject.Find("Player").transform;
    }

    new void Update()
    {
        base.Update();

        bool canSeePlayer = !Physics2D.Linecast(this.transform.position, this.aimingTarget.position, 1 << LayerMask.NameToLayer("Ground"));
        if (canSeePlayer)
        {
            this.movementDirection = this.aimingTarget.position - this.transform.position;
        }
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        
        if (!this.weaponScript || Vector3.Distance(this.transform.position, this.aimingTarget.position) > this.weaponScript.range)
        {
            this.Move(this.movementDirection.normalized, false, this.grounded && Random.value > 0.995);
        }
        else if (!Physics2D.Linecast(this.transform.position, this.aimingTarget.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            this.Move(Vector3.zero, false, false);
            this.weaponScript.TryFire();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 dir = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0) - this.transform.position;
        if (dir.x != 0)
            this.movementDirection *= -1;
    }
}