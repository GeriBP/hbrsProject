using UnityEngine;
using System.Collections;

public class crossAim : MonoBehaviour
{
    private Vector3 mousePosition;
    public float smooth;
    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x, mousePosition.y);
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(mousePosition.x, mousePosition.y, 0.0f), smooth);
    }
}
