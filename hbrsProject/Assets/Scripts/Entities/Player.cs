﻿using System;
using System.Collections;using System.Collections.Generic;using System.Linq;using UnityEngine;using UnityEngine.UI;public class Player : Entity{    [Header("Energy")]    public GameObject energyBarPrefab;    public float maxEnergy = 200;    public float currentEnergy = 200;    public float energyRegenerationMultiplier = 1;    public float abilityMultiplier = 1.0f;
    [Header("PickUp Particles")]
    public GameObject rifleAmmoPs;
    [HideInInspector]
    public Vector3 lastCheckpoint;
    private bool shouldJump = false;    private Text ammoDisplay;    private CameraShake cameraShake;    private GameObject energyBar;    private Slider energyBarSlider;    private Image energyBarFill;    private Ability abilityScript;    [HideInInspector]    public static bool rifleFound = false;     //Change to false!

    private AudioManager AM;

    new void Awake()    {        base.Awake();        this.ammoDisplay = GameObject.Find("AmmoCount").GetComponent<Text>();        this.cameraShake = GameObject.Find("MainCamera").GetComponent<CameraShake>();    }    new void Start()    {
        //AM = GameObject.Find("AudioManager").GetComponent<AudioManager>();        base.Start();        this.lastCheckpoint = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);        this.energyBar = GameObject.Instantiate(this.energyBarPrefab, this.transform);        this.energyBar.transform.position = this.transform.position + Vector3.up * (this.healthBarOffset - 0.2f);        this.energyBarSlider = this.energyBar.GetComponentInChildren<Slider>();        this.energyBarFill = this.energyBar.GetComponentsInChildren<Image>().FirstOrDefault(image => image.name == "Fill");        if (this.CurrentWeapon)        {            this.abilityScript = this.CurrentWeapon.GetComponent("Ability") as Ability;            this.abilityScript.player = this;        }    }    void Update()    {        if (!this.shouldJump)        {
            // Read the jump input in Update so button presses aren't missed
            this.shouldJump = Input.GetKeyDown(KeyCode.W);
            //AM.PlaySound(AudioManager.SFX.Jump);
        }        if (Input.GetKey(KeyCode.Mouse0) && this.abilityScript.canFire && this.weaponScript.TryFire())        {            this.cameraShake.Shake(this.weaponScript.shakeIntensity, this.weaponScript.shakeDuration);        }        if (Input.GetKeyDown(KeyCode.Mouse1) && this.abilityScript.TryFire())        {            this.cameraShake.Shake(this.weaponScript.shakeIntensity, this.weaponScript.shakeDuration);        }        if (Input.GetKeyDown(KeyCode.R))        {            this.weaponScript.TryReload();        }

        if ((Input.GetAxis("Mouse ScrollWheel") != 0 && this.TrySwitchWeapon((this.currentWeaponIndex + (int)Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")) + this.weaponPrefabs.Count) % this.weaponPrefabs.Count))
            || (Input.GetKeyDown(KeyCode.Alpha1) && this.TrySwitchWeapon(0))
            || (Input.GetKeyDown(KeyCode.Alpha2) && this.TrySwitchWeapon(1)))
        {
            this.abilityScript = this.weapons[this.currentWeaponIndex].GetComponent("Ability") as Ability;            this.abilityScript.player = this;
        }        this.UpdateUI();

        //For Debug purposes, eventually needs to b removed
        //if (Input.GetKeyDown(KeyCode.U))        //{        //    Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!");        //    Debug.Log("THIS ARE YOUR PLAYER STATS:");        //    Debug.Log("HP: " + this.maxHealth);        //    Debug.Log("MOVE SP: " + this.movementSpeed);        //    Debug.Log("JUMP: " + this.jumpForce);        //}
        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //    Debug.Log("THIS ARE YOUR WEAPON STATS:");
        //    Debug.Log("ACC: " + this.accuracyMultiplier);
        //    Debug.Log("WEP MULT: " + this.damageMultiplier);
        //    Debug.Log("REL MULT: " + this.reloadSpeedMultiplier);
        //}
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //    Debug.Log("THIS ARE YOUR ABILITY STATS:");
        //    Debug.Log("ENERGY: " + this.maxEnergy);
        //    Debug.Log("ENERGY REGEN: " + this.energyRegenerationMultiplier);
        //    Debug.Log("AB DMG: " + this.abilityMultiplier);
        //}
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    upgradeManagerScript.ModMoney(1000);
        //}
        ///////////////////////////////////////////

        this.currentEnergy = Mathf.Lerp(this.currentEnergy, this.maxEnergy, Time.deltaTime * this.energyRegenerationMultiplier * 0.1f);
        if (!this.abilityScript.CanFire())
            this.energyBarFill.color = Color.gray;
        else
            this.energyBarFill.color = Color.yellow;
        this.energyBarSlider.value = this.currentEnergy / (float)this.maxEnergy;
    }
    new void FixedUpdate()
    {
        base.FixedUpdate();

        // Condition to prevent player from walking during menus
        //(when time is not freezed when the upgrade menu is coming down)
        if (!MenuHandler.IsMenuOpen) this.Move(Vector3.right * Input.GetAxis("Horizontal"), Input.GetKey(KeyCode.S), this.shouldJump);
        this.shouldJump = false;
    }
    private void UpdateUI()
    {
        this.ammoDisplay.text = this.weaponScript.reloading ? "RELOADING" : this.weaponScript.currentMagazineBullets + "/" + this.weaponScript.maxMagazineBullets;
        if (this.weaponScript.reloading)
        {
            this.ammoDisplay.text = "RELOADING";
        }
        else
        {
            if (this.weaponScript.hasInfAmmo)
            {
                this.ammoDisplay.text  = this.weaponScript.currentMagazineBullets + "/INF";
            }
            else
            {
                this.ammoDisplay.text = this.weaponScript.currentMagazineBullets + "/" + this.weaponScript.totalBullets;
            }
        }
    }

    public override void OnDeath()
    {
        this.upgradeManagerScript.resetMoney();
        this.animator.Play("IdlePlayer");
        this.gameObject.SetActive(false);
        Invoke("Respawn", 1);
    }

    private void Respawn()
    {
        this.gameObject.SetActive(true);
        this.AdjustHealth(this.maxHealth);
        this.currentEnergy = this.maxEnergy;
        this.transform.position = new Vector3(this.lastCheckpoint.x, this.lastCheckpoint.y + (this.transform.position.y - this.transform.Find("GroundCheck").position.y) + 1, this.lastCheckpoint.z);
        System.Array.ForEach(GameObject.Find("Level").GetComponentsInChildren<Spawner>(), spawner => spawner.Spawn());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ammo")
        {
            this.weapons[1].GetComponent<Weapon>().totalBullets += 15;
            Instantiate(rifleAmmoPs, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if (other.tag == "End")
        {
            MenuHandler.loadEnd();
        }
    }

    public void updateAbility()
    {
        this.abilityScript = this.weapons[this.currentWeaponIndex].GetComponent("Ability") as Ability;
        this.abilityScript.player = this;
    }

}