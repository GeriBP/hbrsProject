using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
    public float radius = 100f;

    private Transform playerTransform;
    private CameraFollow cameraFollow;

    private void Awake()
    {
        this.playerTransform = GameObject.Find("Player").transform;
        this.cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    void LateUpdate()
    {
        this.transform.position = this.cameraFollow.GetWorldPosition(Input.mousePosition);
    }
}
