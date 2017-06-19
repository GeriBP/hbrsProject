using UnityEngine;
using System.Collections;

public class cameraShake : MonoBehaviour
{
    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay;
    public float shake_intensity;
    private Quaternion iniPos;
    // Use this for initialization
    void Start()
    {
        iniPos = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (shake_intensity > 0)
        {
            transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            transform.rotation = new Quaternion(
            originRotation.x + Random.Range(-shake_intensity, shake_intensity) * .2f,
            originRotation.y + Random.Range(-shake_intensity, shake_intensity) * .2f,
            originRotation.z + Random.Range(-shake_intensity, shake_intensity) * .2f,
            originRotation.w + Random.Range(-shake_intensity, shake_intensity) * .2f);
            shake_intensity -= shake_decay;
        }
        else
        {
            shake_intensity = 0;
            transform.rotation = iniPos;
        }
    }

    public void Shake(float proportion)
    {
        if (shake_intensity == 0)
        {
            originPosition = transform.position;
            originRotation = transform.rotation;
            shake_intensity = .2f*proportion;
            shake_decay = 0.05f * proportion;
        }
    }
}
