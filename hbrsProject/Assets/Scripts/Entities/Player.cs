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
    public float maxEnergy = 200;
    public float currentEnergy = 200;
    public float energyRegen = 1;
    public float abilityDmgMult = 1.0f;

    private bool shouldJump = false;
    private Transform crosshairTransform;

    public static int currency = 0;

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

        //For Debug purposes, eventually needs to b removed
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Debug.Log("THIS ARE YOUR PLAYER STATS:");
            Debug.Log("HP: " + maxHealth);
            Debug.Log("MOVE SP: " + movementSpeed);
            Debug.Log("JUMP: " + jumpForce);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Debug.Log("THIS ARE YOUR WEAPON STATS:");
            Debug.Log("ACC: " + accuracy);
            Debug.Log("WEP MULT: " + weaponDmgMult);
            Debug.Log("REL MULT: " + reloadTimeMult);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Debug.Log("THIS ARE YOUR ABILITY STATS:");
            Debug.Log("ENERGY: " + maxEnergy);
            Debug.Log("ENERGY REGEN: " + energyRegen);
            Debug.Log("AB DMG: " + abilityDmgMult);
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
