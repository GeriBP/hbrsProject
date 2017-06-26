using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pistol : MonoBehaviour {
    [SerializeField]
    GameObject cursor, bullet, nozzle;
    [SerializeField]
    float fireRate, intensityShoot, shakeTime, reloadTime;
    [Header("Pistol Values")]
    public int magSize = 12;

    private bool faceRight = true;
    private bool shootUp = true;
    private bool reloading = false;
    public int magBullets;
    private Vector2 dir;

    private Text ammoDisp;

    private CameraShake cSh;
    private Player playerS;
    // Use this for initialization
    void Start () {
        magBullets = magSize;
        ammoDisp = GameObject.Find("AmmoCount").GetComponent<Text>();
        ammoDisp.text = magBullets.ToString() + "/" + magSize.ToString();
        cSh = GameObject.Find("MainCamera").GetComponent<CameraShake>();
        if (cSh == null) Debug.Log("adjhashsdk");
        playerS = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update () {
        if (!MenuHandler.isPaused) {
            if (shootUp && Input.GetKeyDown(KeyCode.Mouse0) && magBullets > 0 && !reloading)
            {
                magBullets--;
                ammoDisp.text = magBullets.ToString() + "/" + magSize.ToString();
                shootUp = false;
                Invoke("enableShoot", fireRate);
                cSh.Shake(intensityShoot, shakeTime);

                dir = new Vector2(cursor.transform.position.x - transform.position.x, cursor.transform.position.y - transform.position.y).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //GameObject temp = Instantiate(shootExpl, nozzle.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
                //temp.transform.SetParent(transform);
                GameObject temp = Instantiate(bullet, nozzle.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
                temp.GetComponent<Bullet>().bulletShoot(dir, playerS.accuracy, playerS.weaponDmgMult);
            }
            else if (shootUp && Input.GetKeyDown(KeyCode.Mouse0) && magBullets == 0 && !reloading)
            {
                reload();
            }
            if (shootUp && Input.GetKeyDown(KeyCode.R) && !reloading)
            {
                reload();
            }
        }
    }

    private void reload()
    {
        ammoDisp.text = "RELOADING";
        reloading = true;
        Invoke("refillMag", reloadTime * playerS.reloadTimeMult);
    }
    private void refillMag()
    {
        reloading = false;
        magBullets = magSize;
        ammoDisp.text = magBullets.ToString() + "/" + magSize.ToString();
    }
    private void enableShoot()
    {
        shootUp = true;
    }
}
