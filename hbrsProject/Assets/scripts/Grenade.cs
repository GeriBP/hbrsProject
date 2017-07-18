using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {
    [SerializeField]
    float bulletDamage;

    List<GameObject> goList;

    private bool hasExploded = false;

    void Start()
    {
        goList = new List<GameObject>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasExploded)
        {
            hasExploded = true;
            GameObject.Find("MainCamera").GetComponent<CameraShake>().Shake(0.8f, 0.1f);
        }
        if (!goList.Contains(collision.gameObject))
        {
            goList.Add(collision.gameObject);
            Entity entity = collision.GetComponent("Entity") as Entity;
            if (entity == null) return;

            entity.AdjustHealth(-bulletDamage*GameObject.Find("Player").GetComponent<Player>().abilityMultiplier);
        }
    }
}
