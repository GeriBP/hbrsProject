using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {
    [SerializeField]
    float bulletDamage;

    List<GameObject> goList;

    void Start()
    {
        goList = new List<GameObject>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!goList.Contains(collision.gameObject))
        {
            Debug.Log("Greande in: " + Time.time);
            goList.Add(collision.gameObject);
            Entity entity = collision.GetComponent("Entity") as Entity;
            if (entity == null) return;

            entity.AdjustHealth(-bulletDamage);
        }
    }
}
