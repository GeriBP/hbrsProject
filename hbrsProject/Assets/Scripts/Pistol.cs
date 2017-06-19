using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour {
    [SerializeField]
    GameObject cursor, bullet, gunPoint;
    [SerializeField]
    float fireRate, smoothPos;
    [SerializeField]
    Player playerS;

    private bool faceRight = true;
    private bool shootUp = true;
    private Vector2 dir;
    // Use this for initialization
    void Start () {
		
	}

    private void FixedUpdate()
    {
        float tan = (cursor.transform.position.y - transform.position.y) / (cursor.transform.position.x - transform.position.x);
        float arctan = Mathf.Rad2Deg * Mathf.Atan(tan);
        transform.rotation = Quaternion.Euler(0, 0, arctan);
        transform.position = Vector3.Lerp(transform.position, gunPoint.transform.position, smoothPos);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
