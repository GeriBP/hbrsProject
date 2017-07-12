using System.Linq;
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
            menuScript.OpenMenu(menuScript.checkPMenu);
            playerS = GameObject.Find("Player").GetComponent<Player>();
            playerS.AdjustHealth(playerS.maxHealth);
            playerS.currentEnergy = playerS.maxEnergy;
            playerS.lastCheckpoint = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            System.Array.ForEach(GameObject.Find("Level").GetComponentsInChildren<Spawner>(), spawner => spawner.Spawn());
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
