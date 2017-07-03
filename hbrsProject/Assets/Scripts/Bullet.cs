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
        myRb.AddForce(new Vector2(Random.Range(dir.x - accuracy, dir.x + accuracy), Random.Range(dir.y - accuracy, dir.y + accuracy)) * bulletSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entity = collision.GetComponent("Entity") as Entity;
        if (entity == null) return;

        entity.AdjustHealth(-bulletDamage);

        GameObject.Instantiate(entity.hitEffectPrefab, this.transform.position, this.transform.rotation);
        
        if (this.explosion)
        {
            GameObject.Instantiate(this.explosion, transform.position, Quaternion.identity);
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(this.gameObject);
    }
}
