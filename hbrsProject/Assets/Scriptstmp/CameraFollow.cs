using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    
    void Start () {
        Cursor.visible = false;
        this.transform.parent = null;
    }

    private void LateUpdate()
    {
        this.transform.position = new Vector3(this.target.position.x, this.target.position.y, this.transform.position.z);
    }

    public Vector3 GetWorldPosition(Vector3 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, Vector3.zero);
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
