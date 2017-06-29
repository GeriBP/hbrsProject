using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpray : Ability {
    private float originalCooldown;

    protected override void Fire()
    {
        this.originalCooldown = this.player.weaponScript.cooldown;
        this.player.weaponScript.cooldown = 0;
        for (int i = 0; i < 3; i++)
        {
            Invoke("FireWeapon", 0.1f * i);
        }
        Invoke("EndSpray", 0.1f * 3);
    }

    protected override bool FireCondition()
    {
        return this.player.weaponScript.currentMagazineBullets >= 3;
    }

    private void FireWeapon()
    {
        this.player.weaponScript.TryFire();
    }

    private void EndSpray()
    {
        this.player.weaponScript.cooldown = this.originalCooldown;
        this.ResetFire();
    }
}
