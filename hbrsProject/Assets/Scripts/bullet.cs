using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed, bulletDamage;
    [SerializeField]
    GameObject explosion;
    private Rigidbody2D myRb;

    public void BulletShoot(Vector2 dir, float accuracy, float dmgMult)
    {
        bulletDamage = bulletDamage * dmgMult;
        myRb = GetComponent<Rigidbody2D>();

        // FIX ACURRACY
        myRb.AddForce(new Vector2(dir.x, Random.Range(dir.y - accuracy, dir.y + accuracy)) * bulletSpeed, ForceMode2D.Impulse);
        //myRb.AddForce(new Vector2(dir.x, Random.Range(dir.y , dir.y)) * bulletSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().AdjustHealth(-bulletDamage);
        }
        else if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().AdjustHealth(-bulletDamage);
        }

        if (this.explosion)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(this.gameObject);
    }
}
