using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    public GameObject weaponPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;

        Player player = collision.gameObject.GetComponent<Player>();
        player.weaponPrefabs.Add(this.weaponPrefab);
        GameObject.Destroy(this.gameObject);
    }
}
