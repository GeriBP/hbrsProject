using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject enemy;
    public float respawnTime;

    private GameObject currentEnemy;

    private void FixedUpdate()
    {
        if (!this.currentEnemy && !IsInvoking("Spawn"))
        {
            Invoke("Spawn", this.respawnTime);
        }
    }

    private void Spawn()
    {
        this.currentEnemy = GameObject.Instantiate(this.enemy, this.transform.position, Quaternion.LookRotation(Vector3.forward));
    }
}
