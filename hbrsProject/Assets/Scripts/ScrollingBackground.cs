using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {
    public float scrollSpeed = 10f;

    private new MeshRenderer renderer;

	// Use this for initialization
	void Awake () {
        this.renderer = this.GetComponent<MeshRenderer>();
    }

    void Start()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0, 0, Camera.main.nearClipPlane));
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, this.transform.position.z));
        float distance;
        xy.Raycast(ray, out distance);
        Vector3 topLeftWorldPos = ray.GetPoint(distance) * 2;
        float scaleValue = Mathf.Abs(Mathf.Min(topLeftWorldPos.x, topLeftWorldPos.y));

        this.transform.localScale = new Vector3(scaleValue, scaleValue, this.transform.localScale.z);
    }

    public Vector3 GetWorldPosition(Vector3 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, Vector3.zero);
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    // Update is called once per frame
    void Update () {
        Vector2 offset = this.renderer.material.mainTextureOffset;
        offset.x += Time.deltaTime / this.scrollSpeed;
        this.renderer.material.mainTextureOffset = offset;
    }
}
