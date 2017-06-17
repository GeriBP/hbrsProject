using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour {
    [Header("Movement")]
    public float movementSpeed = 1f;
    public float airMovementModifier = 1f;
    public float crouchMovementModifier = 1f;
    public float jumpForce = 400f;

    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth = 100;
    public bool showHealthBar = true;
    public float healthBarOffset = 0.35f;
    public GameObject healthBarPrefab;

    private Transform groundCheck;
    private Transform ceilingCheck;
    private GameObject healthBar;
    private Slider healthBarSlider;

    protected new Rigidbody2D rigidbody;
    protected Animator animator;

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
        
        if (this.animator)
        {
            this.animator.SetBool("grounded", Physics2D.Linecast(this.groundCheck.position, this.groundCheck.position + Vector3.down * 0.01f, 1 << LayerMask.NameToLayer("Ground")));
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

    public void Move(float horizontalMovement, bool crouch, bool jump)
    {
        if (!crouch && this.animator.GetBool("crouched"))
        {
            if (Physics2D.Linecast(this.groundCheck.position, this.ceilingCheck.position + Vector3.up * 0.01f))
            {
                crouch = true;
            }
        }
        this.animator.SetBool("crouched", crouch);

        float movement = horizontalMovement;
        if (!this.animator.GetBool("grounded"))
            movement *= this.airMovementModifier;
        else if (this.animator.GetBool("crouched"))
            movement *= this.crouchMovementModifier;

        this.rigidbody.velocity = new Vector2(horizontalMovement * this.movementSpeed, this.rigidbody.velocity.y);
        this.animator.SetFloat("speed", Mathf.Abs(this.rigidbody.velocity.x));

        if (this.animator.GetBool("grounded") && jump)
        {
            this.animator.SetBool("grounded", false);
            this.rigidbody.AddForce(new Vector2(0, this.jumpForce), ForceMode2D.Impulse);
        }
    }

    public void AdjustHealth(int change)
    {
        this.currentHealth += change;
        this.currentHealth = Mathf.Clamp(this.currentHealth, 0, this.maxHealth);
    }
}
