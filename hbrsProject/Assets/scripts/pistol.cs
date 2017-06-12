using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistol : MonoBehaviour {
    [SerializeField]
    GameObject cursor, bullet, gunPoint;
    [SerializeField]
    float fireRate, smoothPos;
    [SerializeField]
    player playerS;

    private bool faceRight = true;
    private bool flipUp = true;
    private bool shootUp = true;
    private Vector2 dir;
    // Use this for initialization
    void Start () {
		
	}

    private void FixedUpdate()
    {
        float tan = (cursor.transform.position.y - transform.position.y) / (cursor.transform.position.x - transform.position.x);
        float arctan = Mathf.Rad2Deg * Mathf.Atan(tan);
        transform.rotation = Quaternion.Euler(0, 0, arctan);
        transform.position = Vector3.Lerp(transform.position, gunPoint.transform.position, smoothPos);
        if (flipUp && cursor.transform.position.x < transform.position.x && faceRight)
        {
            flipUp = false;
            playerS.flip();
            flip();
            StartCoroutine(flipActivate());
        }
        else if (flipUp && cursor.transform.position.x > transform.position.x && !faceRight)
        {
            flipUp = false;
            playerS.flip();
            flip();
            StartCoroutine(flipActivate());
        }
    }
    // Update is called once per frame
    void Update () {
		
	}

    //flip weapon
    public void flip()
    {
        faceRight = !faceRight;
        Vector3 theScale = transform.localScale;
        theScale.x = theScale.x * -1;
        transform.localScale = theScale;
    }

    //Reactivates flip
    private IEnumerator flipActivate()
    {
        yield return new WaitForSeconds(0.05f);
        flipUp = true;
    }

    //Reactivates shoot
    private IEnumerator shootActivate()
    {
        yield return new WaitForSeconds(fireRate);
        shootUp = true;
    }
}
