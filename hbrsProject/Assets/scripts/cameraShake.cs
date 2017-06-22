using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeIntensity;

    private Vector3 originPosition;
    private Quaternion originRotation;

    public void Shake(float intensity, float duration)
    {
        this.shakeIntensity = intensity;
        this.originPosition = this.transform.position;
        this.originRotation = this.transform.rotation;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", duration);
    }

    private void BeginShake()
    {
        if (this.shakeIntensity> 0)
        {
            Vector3 originPosition = Camera.main.transform.position;
            Vector2 shakeOffset = Random.insideUnitCircle * this.shakeIntensity;
            this.transform.position = new Vector3(this.transform.position.x + shakeOffset.x, this.transform.position.y + shakeOffset.y, this.transform.position.z);
            this.transform.Rotate(Vector3.forward, Random.Range(-this.shakeIntensity, this.shakeIntensity));
        }
    }

    private void StopShake()
    {
        CancelInvoke("BeginShake");
        this.transform.localPosition = this.originPosition;
        this.transform.rotation = this.originRotation;
    }
}
