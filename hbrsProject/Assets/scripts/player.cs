using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    //GENERAL
    [SerializeField]
    Rigidbody2D myRb;
    [SerializeField]
    Animator myAnim;
    private bool faceRight = true;

    //MOVE
    [SerializeField]
    float moveSpeed;

    //JUMP
    [SerializeField]
    float airMoveSpeed, iniJumpForce;
    [SerializeField]
    float jumpForce;
    private bool canJump = true;
    private bool jumpUp = false;
    private bool grounded = false;
    [SerializeField]
    GameObject groundPoint;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        grounded = checkGround();
        if (Input.GetAxis("Horizontal") != 0.0f)
        {
            //if (grounded) myRb.AddForce(Vector2.right * Input.GetAxis("Horizontal") * moveSpeed, ForceMode2D.Force);
            //else myRb.AddForce(Vector2.right * Input.GetAxis("Horizontal") * airMoveSpeed, ForceMode2D.Force);
            if (grounded) myRb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, myRb.velocity.y);
            else myRb.velocity = new Vector2(Input.GetAxis("Horizontal") * airMoveSpeed, myRb.velocity.y);
        }

        //Variable no acceleration jump
        if (Input.GetKeyDown(KeyCode.W) && grounded && canJump)
        {
            canJump = false;
            StartCoroutine(jumpBlock());
            jumpUp = true;
            StartCoroutine(noJump());
            myRb.AddForce(Vector2.up * iniJumpForce, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.W) && jumpUp)
        {
            myRb.velocity = new Vector2(myRb.velocity.x, jumpForce);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            jumpUp = false;
        }

        //Set anim
        myAnim.SetBool("grounded", grounded);
        if (grounded)
        {
            myAnim.SetFloat("speed", Mathf.Abs(myRb.velocity.x));
        }
        else
        {
            myAnim.SetFloat("speed", 0.0f);
        }
    }

    //Reactivates jump
    private IEnumerator jumpBlock()
    {
        yield return new WaitForSeconds(0.5f);
        canJump = true;
    }

    //Deactivates jumpUp
    private IEnumerator noJump()
    {
        yield return new WaitForSeconds(0.2f);
        jumpUp = false;
    }

    //Checks if player is grounded
    private bool checkGround()
    {
        return Physics2D.Raycast(groundPoint.transform.position, Vector2.down, 0.01f);
    }

    //flips player
    public void flip()
    {
        if (true) //pause condition to negate
        {
            faceRight = !faceRight;
            Vector3 theScale = transform.localScale;
            theScale.x = theScale.x * -1;
            transform.localScale = theScale;
        }
    }
}
