using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed, bulletDamage;
    [SerializeField]
    GameObject explosion;
    private new Rigidbody2D rigidbody;

    private void Awake()
    {
        this.rigidbody = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 direction = this.rigidbody.velocity;
        this.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
    }

    public void BulletShoot(Vector2 dir, float accuracy, float dmgMult)
    {
        bulletDamage = bulletDamage * dmgMult;

        // FIX ACURRACY
        this.rigidbody.AddForce(new Vector2(Random.Range(dir.x - accuracy, dir.x + accuracy), Random.Range(dir.y - accuracy, dir.y + accuracy)) * bulletSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;

        if (this.explosion)
        {
            GameObject.Instantiate(this.explosion, transform.position, Quaternion.identity);
            GameObject.Destroy(this.gameObject);
        }

        Entity entity = collision.GetComponent("Entity") as Entity;
        if (entity == null) return;

        entity.AdjustHealth(-bulletDamage);
        GameObject.Instantiate(entity.hitEffectPrefab, this.transform.position, this.transform.rotation);
    }

    private void OnBecameInvisible()
    {
        GameObject.Destroy(this.gameObject);
    }
}
