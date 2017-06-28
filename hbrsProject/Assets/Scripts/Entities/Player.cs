using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    [Header("Ability Stats")]
    public float maxEnergy = 200;
    public float currentEnergy = 200;
    public float energyRegen = 1;
    public float abilityDmgMult = 1.0f;

    private bool shouldJump = false;
    private Text ammoDisplay;
    private CameraShake cameraShake;

    public static int currency = 0;

    new void Awake()
    {
        base.Awake();

        this.ammoDisplay = GameObject.Find("AmmoCount").GetComponent<Text>();
        this.cameraShake = GameObject.Find("MainCamera").GetComponent<CameraShake>();
    }

    new void Start()
    {
        base.Start();

        this.ammoDisplay.text = this.weaponScript.currentMagazineBullets + "/" + this.weaponScript.maxMagazineBullets;
    }

    new void Update()
    {
        base.Update();

        if (!this.shouldJump)
        {
            // Read the jump input in Update so button presses aren't missed
            this.shouldJump = Input.GetKeyDown(KeyCode.W);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && this.weaponScript.TryFire())
        {
            this.cameraShake.Shake(this.weaponScript.shakeIntensity, this.weaponScript.shakeDuration);
        }

        if (Input.GetKeyDown(KeyCode.R) && !this.weaponScript.reloading)
        {
            this.weaponScript.TryReload();
        }

        this.UpdateUI();

        //For Debug purposes, eventually needs to b removed
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Debug.Log("THIS ARE YOUR PLAYER STATS:");
            Debug.Log("HP: " + this.maxHealth);
            Debug.Log("MOVE SP: " + this.movementSpeed);
            Debug.Log("JUMP: " + this.jumpForce);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Debug.Log("THIS ARE YOUR WEAPON STATS:");
            Debug.Log("ACC: " + this.accuracyMultiplier);
            Debug.Log("WEP MULT: " + this.damageMultiplier);
            Debug.Log("REL MULT: " + this.reloadSpeedMultiplier);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Debug.Log("THIS ARE YOUR ABILITY STATS:");
            Debug.Log("ENERGY: " + this.maxEnergy);
            Debug.Log("ENERGY REGEN: " + this.energyRegen);
            Debug.Log("AB DMG: " + this.abilityDmgMult);
        }
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();

        this.Move(Vector3.right * Input.GetAxis("Horizontal"), Input.GetKey(KeyCode.S), this.shouldJump);
        this.shouldJump = false;
    }

    private void UpdateUI()
    {
        this.ammoDisplay.text = this.weaponScript.reloading ? "RELOADING" : this.weaponScript.currentMagazineBullets + "/" + this.weaponScript.maxMagazineBullets;
    }
}
