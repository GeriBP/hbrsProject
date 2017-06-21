using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour {
    [SerializeField]
    GameObject cursor, bullet, nozzle;
    [SerializeField]
    float fireRate, intensityShoot, accuracy;
    [Header("Script References")]
    [SerializeField]
    cameraShake cSh;

    private bool faceRight = true;
    private bool shootUp = true;
    private Vector2 dir;
    // Use this for initialization
    void Start () {
		
	}

    //private void FixedUpdate()
    //{
    //    float tan = (cursor.transform.position.y - transform.position.y) / (cursor.transform.position.x - transform.position.x);
    //    float arctan = Mathf.Rad2Deg * Mathf.Atan(tan);
    //    transform.rotation = Quaternion.Euler(0, 0, arctan);
    //    transform.position = Vector3.Lerp(transform.position, gunPoint.transform.position, smoothPos);
    //}
    // Update is called once per frame
    void Update () {
        if (shootUp && Input.GetAxis("Fire1") != 0.0f)
        {
            shootUp = false;
            Invoke("enableShoot", fireRate);
            cSh.Shake(intensityShoot);

            dir = new Vector2(cursor.transform.position.x - transform.position.x, cursor.transform.position.y - transform.position.y).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //GameObject temp = Instantiate(shootExpl, nozzle.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            //temp.transform.SetParent(transform);
            GameObject temp = Instantiate(bullet, nozzle.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            temp.GetComponent<bullet>().bulletShoot(dir, accuracy);
        }
    }

    private void enableShoot()
    {
        shootUp = true;
    }
}
