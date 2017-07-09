using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
    private MenuHandler menuScript;
    [Header("Interact Key display GO")]
    public GameObject interactSprite;
    private bool canEnter = false;
    private Player playerS;
    // Use this for initialization
    void Start () {
        menuScript = GameObject.Find("GameMenus").GetComponent<MenuHandler>();
    }

    private void Update()
    {
        if (canEnter && Input.GetKeyDown(KeyCode.E))
        {
            menuScript.CheckPOpen();
            playerS = GameObject.Find("Player").GetComponent<Player>();
            playerS.AdjustHealth(playerS.maxHealth);
            playerS.currentEnergy = playerS.maxEnergy;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            interactSprite.SetActive(true);
            canEnter = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            interactSprite.SetActive(false);
            canEnter = false;
        }
    }
}
