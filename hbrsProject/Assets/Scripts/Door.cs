using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [Header("Interact Key display GO")]
    public GameObject interactSprite;

    private bool canOpen = false;
    private Animator animator;

    private void Awake()
    {
        this.animator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (this.canOpen && Input.GetKeyDown(KeyCode.E))
        {
            this.animator.SetTrigger("Open");
            this.interactSprite.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            this.interactSprite.SetActive(true);
            this.canOpen = true;
        }
    }
}
