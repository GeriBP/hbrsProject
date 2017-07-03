using System.Collections;using System.Collections.Generic;using System.Linq;using UnityEngine;using UnityEngine.UI;public class Player : Entity{    [Header("Energy")]    public GameObject energyBarPrefab;    public float maxEnergy = 200;    public float currentEnergy = 200;    public float energyRegenerationMultiplier = 1;    public float abilityMultiplier = 1.0f;    [Header("Player Weapons")]    public GameObject pistolGo;    public GameObject rifleGo;    private int currentWep = 1; //1-Pistol, 2-Rifle
    public int pistolBullets;    public int rifleBullets;    private bool canSwap = true;
    private bool shouldJump = false;    private Text ammoDisplay;    private CameraShake cameraShake;    private GameObject energyBar;    private Slider energyBarSlider;    private Image energyBarFill;    private Ability abilityScript;    private UpgradeManager upgradeManagerScript;    public static int currency = 0;    new void Awake()    {        base.Awake();        this.ammoDisplay = GameObject.Find("AmmoCount").GetComponent<Text>();        this.cameraShake = GameObject.Find("MainCamera").GetComponent<CameraShake>();        this.upgradeManagerScript = GameObject.Find("GameMenus").GetComponent<UpgradeManager>();    }    new void Start()    {        base.Start();        this.ammoDisplay.text = this.weaponScript.currentMagazineBullets + "/" + this.weaponScript.maxMagazineBullets;        this.energyBar = GameObject.Instantiate(this.energyBarPrefab, this.transform);        this.energyBar.transform.position = this.transform.position + Vector3.up * (this.healthBarOffset - 0.2f);        this.energyBarSlider = this.energyBar.GetComponentInChildren<Slider>();        this.energyBarFill = this.energyBar.GetComponentsInChildren<Image>().FirstOrDefault(image => image.name == "Fill");        if (this.weapon)        {            this.abilityScript = this.weapon.GetComponent("Ability") as Ability;            this.abilityScript.player = this;        }    }    new void Update()    {        base.Update();        if (!this.shouldJump)        {            // Read the jump input in Update so button presses aren't missed            this.shouldJump = Input.GetKeyDown(KeyCode.W);        }        if (Input.GetKey(KeyCode.Mouse0) && this.abilityScript.canFire && this.weaponScript.TryFire())        {            this.cameraShake.Shake(this.weaponScript.shakeIntensity, this.weaponScript.shakeDuration);        }        if (Input.GetKeyDown(KeyCode.Mouse1) && this.abilityScript.TryFire())        {            this.cameraShake.Shake(this.weaponScript.shakeIntensity, this.weaponScript.shakeDuration);        }        if (Input.GetKeyDown(KeyCode.R) && !this.weaponScript.reloading)        {            this.weaponScript.TryReload();        }        this.UpdateUI();

        if ((Input.GetAxis("Mouse ScrollWheel") > 0f) || (Input.GetKeyDown(KeyCode.Alpha2)) && currentWep != 2 && !this.weaponScript.reloading) //next Weapon
        {
            canSwap = false;
            Invoke("EnableSwap", 0.5f);
            pistolBullets = this.weaponScript.currentMagazineBullets;
            Destroy(this.weapon);
            this.weapon = GameObject.Instantiate(this.rifleGo, this.transform);
            this.weaponScript = this.weapon.GetComponent<Weapon>();
            this.weaponScript.currentMagazineBullets = rifleBullets;
            currentWep = 2;
        }
        else if ((Input.GetAxis("Mouse ScrollWheel") < 0f) || (Input.GetKeyDown(KeyCode.Alpha1)) && currentWep != 1 && !this.weaponScript.reloading) //previous Weapon
        {
            canSwap = false;
            Invoke("EnableSwap", 0.5f);
            rifleBullets = this.weaponScript.currentMagazineBullets;
            Destroy(this.weapon);
            this.weapon = GameObject.Instantiate(this.pistolGo, this.transform);
            this.weaponScript = this.weapon.GetComponent<Weapon>();
            this.weaponScript.currentMagazineBullets = pistolBullets;
            currentWep = 1;
        }

        //For Debug purposes, eventually needs to b removed
        if (Input.GetKeyDown(KeyCode.U))        {            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!");            Debug.Log("THIS ARE YOUR PLAYER STATS:");            Debug.Log("HP: " + this.maxHealth);            Debug.Log("MOVE SP: " + this.movementSpeed);            Debug.Log("JUMP: " + this.jumpForce);        }
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
            Debug.Log("ENERGY REGEN: " + this.energyRegenerationMultiplier);
            Debug.Log("AB DMG: " + this.abilityMultiplier);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            upgradeManagerScript.modMoney(1000);
        }

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
        if(!MenuHandler.isPaused) this.Move(Vector3.right * Input.GetAxis("Horizontal"), Input.GetKey(KeyCode.S), this.shouldJump);
        this.shouldJump = false;
    }

    private void UpdateUI()
    {
        this.ammoDisplay.text = this.weaponScript.reloading ? "RELOADING" : this.weaponScript.currentMagazineBullets + "/" + this.weaponScript.maxMagazineBullets;
    }    private void EnableSwap()
    {
        canSwap = true;
    }
}
