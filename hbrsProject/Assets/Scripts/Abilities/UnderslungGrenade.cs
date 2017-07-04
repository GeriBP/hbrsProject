using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderslungGrenade : Ability
{
    public GameObject grenade;

    protected Transform nozzleTransform;

    private new void Awake()
    {
        base.Awake();

        this.nozzleTransform = this.transform.Find("UnderslungNozzle");
    }

    protected override void Fire()
    {
        Vector3 direction = (this.player.aimingTarget.position - this.nozzleTransform.position).normalized;
        GameObject bullet = GameObject.Instantiate(this.grenade, this.nozzleTransform.position, Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward));
        bullet.GetComponent<Bullet>().BulletShoot(direction, 0, 1);

        this.ResetFire();
    }

    protected override bool FireCondition()
    {
        return true;
    }
}
