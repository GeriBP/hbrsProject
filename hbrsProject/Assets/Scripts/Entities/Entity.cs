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
    public bool showHealthBar = true;
    public float healthBarOffset = 0.35f;
    public GameObject healthBarPrefab;

    private Transform groundCheck;
    private Transform ceilingCheck;
    private GameObject healthBar;
    private Slider healthBarSlider;
    
    protected new Rigidbody2D rigidbody;
    protected Animator animator;
    protected bool grounded = false;
    protected bool crouched = false;

    protected void Awake()
    {
        this.groundCheck = this.transform.Find("GroundCheck");
        this.ceilingCheck = this.transform.Find("CeilingCheck");
        this.rigidbody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
    }

    protected void Start () {
        if (this.showHealthBar)
        {
            this.healthBar = GameObject.Instantiate(this.healthBarPrefab);
            this.healthBar.transform.SetParent(this.transform);
            this.healthBar.transform.position = this.transform.position + Vector3.up * this.healthBarOffset;
            this.healthBarSlider = this.healthBar.GetComponentInChildren<Slider>();
        }
    }

    protected void FixedUpdate()
    {
        if (this.currentHealth == 0)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }

        this.grounded = this.rigidbody.gravityScale > 0 && Physics2D.Linecast(this.groundCheck.position, this.groundCheck.position + Vector3.down * 0.01f, 1 << LayerMask.NameToLayer("Ground"));
        if (this.animator)
        {
            this.animator.SetBool("grounded", this.grounded);
            this.animator.SetFloat("verticalSpeed", this.rigidbody.velocity.y);
        }
    }

    protected void Update()
    {
        if (this.showHealthBar)
        {
            this.healthBarSlider.value = this.currentHealth / (float)this.maxHealth;
        }
    }

    public void Move(Vector3 movementDirection, bool crouch, bool jump)
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
            this.animator.SetFloat("speed", Mathf.Abs(this.rigidbody.velocity.x));
        }
    }

    public void AdjustHealth(float change)
    {
        this.currentHealth += change;
        this.currentHealth = Mathf.Clamp(this.currentHealth, 0, this.maxHealth);
    }
}
