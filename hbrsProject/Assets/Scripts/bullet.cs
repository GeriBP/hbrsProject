﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed, bulletDamage;
    [SerializeField]
    GameObject explosion;
    private Rigidbody2D myRb;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void bulletShoot(Vector2 dir, float accuracy, float dmgMult)
    {
        bulletDamage = bulletDamage * dmgMult;
        myRb = GetComponent<Rigidbody2D>();

        // FIX ACURRACY
        myRb.AddForce(new Vector2(dir.x, Random.Range(dir.y - accuracy, dir.y + accuracy)) * bulletSpeed, ForceMode2D.Impulse);
        //myRb.AddForce(new Vector2(dir.x, Random.Range(dir.y , dir.y)) * bulletSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.tag == "Player")
        {
            GetComponent<Player>().AdjustHealth(-bulletDamage); 
        }*/
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().AdjustHealth(-bulletDamage);
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
