using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject enemy;

    private GameObject currentEnemy;

    private void Start()
    {
        this.Spawn();
    }

    public void Spawn()
    {
        if (this.currentEnemy)
        {
            //this.currentEnemy.GetComponent<Enemy>().OnDeath();
            Destroy(this.currentEnemy);
        }
        this.currentEnemy = GameObject.Instantiate(this.enemy, this.transform.position, Quaternion.LookRotation(Vector3.forward));
    }
}
