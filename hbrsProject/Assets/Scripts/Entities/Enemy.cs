using System;
using UnityEngine;

public class Enemy : Entity
{
    [Header("AmmoCrates")]
    public GameObject RifleCrate;
    public float ammoRifleProb;    [Header("Move conditions")]    public float distMove;
    [Header("Probabilities")]
    private Vector3 movementDirection = Vector3.left;

    new void Start()    {
        base.Start();        
        this.aimingTarget = GameObject.Find("Player").transform;
    }
    void Update()    {        bool canSeePlayer = this.aimingTarget && !Physics2D.Linecast(this.transform.position, this.aimingTarget.position, 1 << LayerMask.NameToLayer("Ground"));        if (canSeePlayer)        {            this.movementDirection = this.aimingTarget.position - this.transform.position;        }    }    new void FixedUpdate()    {        base.FixedUpdate();                if (!this.weaponScript || Vector3.Distance(this.transform.position, this.aimingTarget.position) > this.weaponScript.range)        {            if(Vector2.Distance(this.aimingTarget.position, this.transform.position) < distMove) this.Move(this.movementDirection.normalized, false, this.grounded && UnityEngine.Random.value > 0.995);        }        else if (!Physics2D.Linecast(this.transform.position, this.aimingTarget.position, 1 << LayerMask.NameToLayer("Ground")))        {            this.Move(Vector3.zero, false, false);            this.weaponScript.TryFire();        }    }       private void OnCollisionEnter2D(Collision2D collision)    {        Vector3 dir = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0) - this.transform.position;        if (dir.x != 0)            this.movementDirection *= -1;    }    public override void OnDeath()    {        if(Player.rifleFound) CheckProbabilities();        GameObject.Destroy(this.gameObject);    }    private void CheckProbabilities()    {        if (UnityEngine.Random.Range(0.0f, 1.0f) <= ammoRifleProb)        {            Instantiate(RifleCrate, transform.position, Quaternion.identity);        }    }}