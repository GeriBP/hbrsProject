using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEnemies : MonoBehaviour {
    public float distance;
    private bool pos1Bool = true;
    // Use this for initialization
    void Start () {
        StartCoroutine(changeSide());
    }

    IEnumerator changeSide() {
        yield return new WaitForSeconds(Random.Range(1.0f, 4.0f));
        if (pos1Bool)
        {
            transform.position = new Vector3(distance, transform.position.y, transform.position.z);
            pos1Bool = false;
        }
        else if (!pos1Bool)
        {
            transform.position = new Vector3(-distance, transform.position.y, transform.position.z);
            pos1Bool = true;
        }
        StartCoroutine(changeSide());
    }
}
