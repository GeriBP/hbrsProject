using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public float dynamicCameraStartOffset = 8;
    public float dynamicCameraMultiplier = 0;

    private Transform crosshairTransform;

    void Awake()
    {
        this.crosshairTransform = this.transform.Find("Crosshair").transform;
    }

    void Start () {
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        Vector3 directionToCrossHair = this.crosshairTransform.position - this.target.transform.position;
        Vector3 dynamicOffset = directionToCrossHair.normalized * Mathf.Max(0, directionToCrossHair.magnitude - this.dynamicCameraStartOffset) * this.dynamicCameraMultiplier;
        this.transform.position = new Vector3(this.target.position.x, this.target.position.y, this.transform.position.z) + dynamicOffset;
    }
}