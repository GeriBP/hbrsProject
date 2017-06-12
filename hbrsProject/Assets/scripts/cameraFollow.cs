using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {
    [SerializeField]
    GameObject camFollowP;
    [SerializeField]
    float smooth;
    private float iniZ;
    // Use this for initialization
    void Start () {
        //Disable OS cursor, we just want to see the crosshair
        Cursor.visible = false;
        iniZ = transform.position.z;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(camFollowP.transform.position.x, camFollowP.transform.position.y, iniZ), smooth);
    }
}
