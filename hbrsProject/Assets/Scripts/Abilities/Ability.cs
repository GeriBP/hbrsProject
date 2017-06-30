using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public bool canFire = true;
    public float energyCosts;

    protected Animator animator;

    private float originalDamageMultiplier;

    protected void Awake()
    {
        this.animator = this.GetComponent<Animator>();
    }

    public bool TryFire()
    {
        if (!this.CanFire()) return false;

        this.canFire = false;
        this.originalDamageMultiplier = this.player.damageMultiplier;
        this.player.damageMultiplier = this.player.abilityMultiplier;
        this.player.currentEnergy -= this.energyCosts;

        if (this.animator)
        {
            this.animator.Play("Ability");
        }

        this.Fire();

        return true;
    }

    protected void ResetFire()
    {
        this.canFire = true;
        this.player.damageMultiplier = this.originalDamageMultiplier;
    }

    protected abstract void Fire();

    public bool CanFire()
    {
        return !MenuHandler.isPaused && this.canFire && this.player.currentEnergy >= this.energyCosts && this.FireCondition();
    }
    protected abstract bool FireCondition();
}