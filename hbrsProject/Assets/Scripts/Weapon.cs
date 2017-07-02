using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    [HideInInspector]
    public Entity entity;
    [HideInInspector]
    public bool reloading = false;
    [HideInInspector]
    public Ability abilityScript;
    public GameObject bullet;
    public GameObject bulletExplsion;
    public float range = 10;
    public float cooldown = 1;
    public float accuracy;
    public float reloadTime;
    public float shakeIntensity;
    public float shakeDuration;
    public int maxMagazineBullets;
    public int currentMagazineBullets;

    protected Animator animator;
    protected Transform nozzleTransform;

    private bool canFire = true;

    protected void Awake()
    {
        this.animator = this.GetComponent<Animator>();
        this.nozzleTransform = this.transform.Find("Nozzle");
        entity = GetComponentInParent<Entity>();
    }

    protected void FixedUpdate()
    {
        Vector3 right = (this.entity.aimingTarget.position - this.transform.position).normalized;
        this.transform.right = right.normalized;

        if (this.transform.parent.localScale.x < 0 && Mathf.Sign(this.transform.localScale.x) > 0
            || this.transform.parent.localScale.x > 0 && Mathf.Sign(this.transform.localScale.x) < 0)
        {
            Vector3 localScale = this.transform.localScale;
            localScale.x *= -1;
            localScale.y *= -1;
            this.transform.localScale = localScale;
        }
    }

    public bool TryFire()
    {
        if (!this.canFire || this.currentMagazineBullets == 0 || MenuHandler.isPaused) return false;

        Invoke("ResetFire", this.cooldown);
        this.canFire = false;
        this.currentMagazineBullets--;

        if (this.animator)
        {
            this.animator.Play("Attacking");
        }

        Vector3 bulletOrigin = this.nozzleTransform ? this.nozzleTransform.position : this.transform.position;
        Vector3 direction = (this.entity.aimingTarget.position - bulletOrigin).normalized;
        GameObject bullet = GameObject.Instantiate(this.bullet, bulletOrigin, Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward));
        bullet.GetComponent<Bullet>().BulletShoot(direction, this.accuracy * this.entity.accuracyMultiplier, this.entity.damageMultiplier);
        GameObject explosion = GameObject.Instantiate(this.bulletExplsion, bulletOrigin, Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward));
        explosion.transform.SetParent(nozzleTransform);

        if (this.currentMagazineBullets == 0)
        {
            this.TryReload();
        }

        return true;
    }

    private void ResetFire()
    {
        this.canFire = true;
    }

    public void TryReload()
    {
        if (this.reloading) return;
        this.reloading = true;
        Invoke("Reload", this.reloadTime * this.entity.reloadSpeedMultiplier);
    }

    private void Reload()
    {
        this.reloading = false;
        this.currentMagazineBullets = this.maxMagazineBullets;
    }
}
