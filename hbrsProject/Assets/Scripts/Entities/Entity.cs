using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Entity : MonoBehaviour {
    [Header("Movement")]
    public float movementSpeed = 1f;
    public float airMovementModifier = 1f;
    public float crouchMovementModifier = 1f;
    public float jumpForce = 400f;

    [Header("Health")]
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float healthBarOffset = 0.35f;
    public GameObject healthBarPrefab;
    public GameObject hitEffectPrefab;

    [Header("Weapon")]
    public GameObject[] weaponPrefabs;
    public Transform aimingTarget;
    public float damageMultiplier = 1;
    public float accuracyMultiplier = 1;
    public float reloadSpeedMultiplier = 1;

    [Header("Death")]
    public GameObject deathPs;
    public int reward;

    [HideInInspector]
    public Weapon weaponScript;

    private Transform groundCheck;
    private Transform ceilingCheck;
    private GameObject healthBar;
    private Slider healthBarSlider;

    protected new Rigidbody2D rigidbody;
    protected Animator animator;
    protected bool grounded = false;
    protected bool crouched = false;
    protected List<GameObject> weapons = new List<GameObject>();
    protected int currentWeaponIndex = -1;
    protected bool canSwitchWeapon = true;
    protected bool canTakeDmg = true;
    protected UpgradeManager upgradeManagerScript;

    protected GameObject CurrentWeapon
    {
        get
        {
            if (this.currentWeaponIndex == -1 || this.weapons.Count <= this.currentWeaponIndex) return null;
            return this.weapons[this.currentWeaponIndex];
        }
    }

    protected void Awake()
    {
        this.groundCheck = this.transform.Find("GroundCheck");
        this.ceilingCheck = this.transform.Find("CeilingCheck");
        this.rigidbody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
        this.upgradeManagerScript = GameObject.Find("GameMenus").GetComponent<UpgradeManager>();
    }

    protected void Start () {
        if (this.healthBarPrefab)
        {
            this.healthBar = GameObject.Instantiate(this.healthBarPrefab, this.transform);
            this.healthBar.transform.position = this.transform.position + Vector3.up * this.healthBarOffset;
            this.healthBarSlider = this.healthBar.GetComponentInChildren<Slider>();
        }

        if (this.weaponPrefabs.Length > 0)
        {
            this.TrySwitchWeapon(0);
        }
    }

    protected void FixedUpdate()
    {
        Vector3 aimingTargetPosition = this.aimingTarget.position;
        if (aimingTargetPosition.x < this.transform.position.x && Mathf.Sign(this.transform.localScale.x) > 0
            || aimingTargetPosition.x > this.transform.position.x && Mathf.Sign(this.transform.localScale.x) < 0)
        {
            Vector3 localScale = this.transform.localScale;
            localScale.x *= -1;
            this.transform.localScale = localScale;
        }

        this.grounded = this.rigidbody.gravityScale > 0 && Physics2D.Linecast(this.groundCheck.position, this.groundCheck.position + Vector3.down * 0.01f, 1 << LayerMask.NameToLayer("Ground"));
        if (this.animator)
        {
            this.animator.SetBool("grounded", this.grounded);
            this.animator.SetFloat("verticalSpeed", this.rigidbody.velocity.y);
            this.animator.SetFloat("speed", Mathf.Abs(this.rigidbody.velocity.x));
        }
    }

    protected void Move(Vector3 movementDirection, bool crouch, bool jump)
    {
        if (!crouch && this.crouched)
        {
            crouch = Physics2D.Linecast(this.groundCheck.position, this.ceilingCheck.position + Vector3.up * 0.01f);
        }
        this.crouched = crouch;

        if (this.rigidbody.gravityScale > 0)
            movementDirection.y = 0;
        movementDirection = movementDirection.normalized;

        if (!this.grounded)
            movementDirection *= this.airMovementModifier;
        else if (this.crouched)
            movementDirection *= this.crouchMovementModifier;
        movementDirection *= this.movementSpeed;
        
        if (this.rigidbody.gravityScale > 0)
            movementDirection.y = this.rigidbody.velocity.y;
        this.rigidbody.velocity = movementDirection;
        

        if (this.grounded && jump)
        {
            this.grounded = false;
            this.rigidbody.AddForce(new Vector2(0, this.jumpForce), ForceMode2D.Impulse);
        }

        if (this.animator)
        {
            this.animator.SetBool("grounded", this.grounded);
            this.animator.SetBool("crouched", this.crouched);
        }
    }

    public void AdjustHealth(float change)
    {
        if (change < 0 && this.canTakeDmg)
        {
            this.canTakeDmg = false;
            Invoke("EnableTakeDamage", 0.01f);
        }
        else if (change < 0 && !this.canTakeDmg)
        {
            return;
        }

        Debug.Log("change: "+ change + " from " + this.currentHealth + " in : " + Time.time);

        this.currentHealth += change;
        this.currentHealth = Mathf.Clamp(this.currentHealth, 0, this.maxHealth);

        if (this.currentHealth == 0)
        {
            Instantiate(this.deathPs, transform.position, Quaternion.identity);
            this.upgradeManagerScript.ModMoney(this.reward);
            GameObject.Destroy(this.gameObject);
            return;
        }

        if (this.healthBarPrefab)
        {
            this.healthBarSlider.value = this.currentHealth / (float)this.maxHealth;
        }
    }

    private void EnableTakeDamage()
    {
        canTakeDmg = true;
    }

    protected bool TrySwitchWeapon(int weaponIndex)
    {
        if (!this.canSwitchWeapon || (this.weaponScript && this.weaponScript.reloading) || weaponIndex == this.currentWeaponIndex) return false;

        if (this.CurrentWeapon)
        {
            this.CurrentWeapon.SetActive(false);
        }
        
        this.canSwitchWeapon = false;
        Invoke("EnableWeaponSwitch", 0.5f);

        this.currentWeaponIndex = weaponIndex;

        if (this.CurrentWeapon)
        {
            this.CurrentWeapon.SetActive(true);
        }
        else
        {
            this.weapons.Add(GameObject.Instantiate(this.weaponPrefabs[this.currentWeaponIndex], this.transform));
        }

        this.weaponScript = this.CurrentWeapon.GetComponent<Weapon>();
        this.weaponScript.entity = this;

        return true;
    }

    private void EnableWeaponSwitch()
    {
        this.canSwitchWeapon = true;
    }
}
