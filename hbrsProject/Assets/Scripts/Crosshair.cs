using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
    public float clampRadius = 100f;

    private Transform playerTransform;

    private void Awake()
    {
        this.playerTransform = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        if (!MenuHandler.isPaused)
        {
            Vector3 newPosition = this.transform.position + new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
            this.transform.position = this.playerTransform.position + Vector3.ClampMagnitude(newPosition - this.playerTransform.position, this.clampRadius);
        }
    }
}
