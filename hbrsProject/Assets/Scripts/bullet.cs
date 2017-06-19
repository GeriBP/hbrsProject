using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed;
    private Rigidbody2D myRb;
    private bool isAlly = true;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void bulletShoot(Vector2 dir)
    {
        myRb = GetComponent<Rigidbody2D>();

        ////////!!!!!!!1
        // FIX ACURRACY
        //myRb.AddForce(new Vector2(dir.x, Random.Range(dir.y - pWeapon.acurracy, dir.y + pWeapon.acurracy)) * bulletSpeed, ForceMode2D.Impulse);
    }

    public void setAsAlly()
    {
        isAlly = true;
    }
}
