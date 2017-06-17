using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private bool shouldJump = false;
    private CameraFollow cameraFollow;

    new void Awake()
    {
        base.Awake();

        this.cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    new void Update()
    {
        base.Update();

        if (!this.shouldJump)
        {
            // Read the jump input in Update so button presses aren't missed
            this.shouldJump = Input.GetKeyDown(KeyCode.W);
            if (this.shouldJump)
            {
                Debug.Log("GERADE");
            }
        }
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();

        this.Move(Input.GetAxis("Horizontal"), Input.GetKey(KeyCode.S), this.shouldJump);
        this.shouldJump = false;

        Vector3 mousePosition = this.cameraFollow.GetWorldPosition(Input.mousePosition);
        if (mousePosition.x < this.transform.position.x && Mathf.Sign(this.transform.localScale.x) > 0)
        {
            Vector3 localScale = this.transform.localScale;
            localScale.x *= -1;
            this.transform.localScale = localScale;
        }
        else if (mousePosition.x > this.transform.position.x && Mathf.Sign(this.transform.localScale.x) < 0)
        {
            Vector3 localScale = this.transform.localScale;
            localScale.x *= -1;
            this.transform.localScale = localScale;
        }
    }
}
