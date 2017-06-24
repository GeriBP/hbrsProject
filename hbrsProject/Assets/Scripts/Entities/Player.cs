using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Weapon Stats")]
    public float accuracy = 0.25f;
    public float weaponDmgMult = 1.0f;
    public float reloadTimeMult = 1.0f;
    // ! Multipliers affect the damage and reload values of weapons

    [Header("Ability Stats")]
    public int maxEnergy = 200;
    public int currentEnergy = 200;
    public int energyRegen = 1;

    private bool shouldJump = false;
    private Transform crosshairTransform;

    new void Awake()
    {
        base.Awake();

        this.crosshairTransform = Camera.main.transform.Find("Crosshair").transform;
    }

    new void Update()
    {
        base.Update();

        if (!this.shouldJump)
        {
            // Read the jump input in Update so button presses aren't missed
            this.shouldJump = Input.GetKeyDown(KeyCode.W);
        }
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();

        this.Move(Vector3.right * Input.GetAxis("Horizontal"), Input.GetKey(KeyCode.S), this.shouldJump);
        this.shouldJump = false;

        Vector3 crosshairPosition = this.crosshairTransform.position;
        if (crosshairPosition.x < this.transform.position.x && Mathf.Sign(this.transform.localScale.x) > 0
            || crosshairPosition.x > this.transform.position.x && Mathf.Sign(this.transform.localScale.x) < 0)
        {
            Vector3 localScale = this.transform.localScale;
            localScale.x *= -1;
            this.transform.localScale = localScale;
        }
    }
}
