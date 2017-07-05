﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject enemy;
    public float spawnTime;

    private void Start()
    {
        InvokeRepeating("Spawn", 0, this.spawnTime);
    }

    private void Spawn()
    {
        GameObject.Instantiate(this.enemy, this.transform.position, Quaternion.LookRotation(Vector3.forward));
    }
}
