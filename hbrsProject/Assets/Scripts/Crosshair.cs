using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
    public float maxClampRadius = 20f;
    public float minClampRadius = 8f;

    private Transform playerTransform;

    private void Awake()
    {
        this.playerTransform = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        if (!MenuHandler.isPaused && this.playerTransform)
        {
            Vector3 newPosition = this.transform.position + new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

            Vector3 offset = Vector3.ClampMagnitude(newPosition - this.playerTransform.position, this.maxClampRadius);
            if ((double)offset.sqrMagnitude < (double)this.minClampRadius * (double)this.minClampRadius)
            {
                offset = offset.normalized * this.minClampRadius;
            }

            this.transform.position = this.playerTransform.position + offset;
        }
    }
}