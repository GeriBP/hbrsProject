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
    public GameObject nozzleFlash;
    public float range = 10;
    public float cooldown = 1;
    public float accuracy;
    public float reloadTime;
    public float shakeIntensity;
    public float shakeDuration;
    public int maxMagazineBullets;
    public int currentMagazineBullets;
    public int totalBullets;
    public bool hasInfAmmo;

    protected Animator animator;
    protected Transform nozzleTransform;

    private bool canFire = true;

    protected void Awake()
    {
        this.animator = this.GetComponent<Animator>();
        this.nozzleTransform = this.transform.Find("Nozzle");
        this.entity = GetComponentInParent<Entity>();
    }

    protected void FixedUpdate()
    {
        if (this.entity)
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

            //Bug fix
            if (transform.localEulerAngles.y != 0.0f)
            {
                transform.localEulerAngles = new Vector3(0.0f, 0.0f, -179.9f);
            }
        }
    }

    public bool TryFire()
    {
        if (this.reloading) return false;
        if (!this.canFire || MenuHandler.IsMenuOpen) return false;


        if (this.currentMagazineBullets == 0)
        {
            this.TryReload();
            return false;
        }

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
        bullet.GetComponent<Bullet>().BulletShoot(direction, normalDistRandom() * this.accuracy * this.entity.accuracyMultiplier, this.entity.damageMultiplier, this.range);

        if (this.nozzleFlash)
        {
            GameObject nozzleFlash = GameObject.Instantiate(this.nozzleFlash, bulletOrigin, Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward));
            nozzleFlash.transform.SetParent(nozzleTransform);
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
        if(maxMagazineBullets == this.currentMagazineBullets) return;
        if (totalBullets == 0 && !hasInfAmmo)
        {
            //Make sound?
            return;
        }
        this.reloading = true;
        Invoke("Reload", this.reloadTime * this.entity.reloadSpeedMultiplier);
    }

    private void Reload()
    {
        this.reloading = false;
        if (hasInfAmmo)
        {
            this.currentMagazineBullets = this.maxMagazineBullets;
            return;
        }
        if (totalBullets >= maxMagazineBullets) {
            totalBullets -= (maxMagazineBullets - this.currentMagazineBullets);
            this.currentMagazineBullets = this.maxMagazineBullets;
        }
        else
        {
            this.currentMagazineBullets = this.totalBullets;
            totalBullets = 0;
        }
    }

    private float normalDistRandom()
    {
        float u, v, S;

        do
        {
            u = 2.0f * Random.value - 1.0f;
            v = 2.0f * Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0);

        float fac = Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
        return u * fac;
    }
}
