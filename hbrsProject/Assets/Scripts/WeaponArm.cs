using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponArm : MonoBehaviour {
    public GameObject cursor;

    // Use this for initialization
    void Start () {
		
	}

    private void FixedUpdate()
    {
        this.transform.right = (this.cursor.transform.position - this.transform.position).normalized;

        if (Mathf.Sign(this.transform.parent.localScale.x) < 0 && Mathf.Sign(this.transform.localScale.x) > 0
            || Mathf.Sign(this.transform.parent.localScale.x) > 0 && Mathf.Sign(this.transform.localScale.x) < 0)
        {
            Vector3 localScale = this.transform.localScale;
            localScale.x *= -1;
            localScale.y *= -1;
            this.transform.localScale = localScale;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
