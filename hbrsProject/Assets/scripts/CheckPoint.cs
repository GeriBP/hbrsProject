using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
    private MenuHandler menuScript;
    [Header("Interact Key display GO")]
    public GameObject interactSprite;
    // Use this for initialization
    void Start () {
        menuScript = GameObject.Find("GameMenus").GetComponent<MenuHandler>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            interactSprite.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            interactSprite.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            menuScript.UpgradeOpen();
        }
    }
    //On trigger enter display E on Players

    //On trigger stay and E open upgrade

    //Add exit to uprade
}
