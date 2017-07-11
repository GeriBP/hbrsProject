using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {
    public GameObject pauseMenu, upgradeMenu, checkPMenu;

    public static bool IsMenuOpen  
    {
        get
        {
            return MenuHandler.currentMenu != null;
        }
    }

    private Animator upgradeAnim;
    private static GameObject currentMenu;

    // Use this for initialization
    void Start () {
        upgradeAnim = upgradeMenu.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //PAUSE
        if (Input.GetKeyDown(KeyCode.Escape) && !MenuHandler.currentMenu)
        {
            this.OpenMenu(this.pauseMenu);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && MenuHandler.currentMenu)
        {
            this.CloseMenu();
        }
    }

    public void OpenMenu(GameObject menu)
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        menu.SetActive(true);
        MenuHandler.currentMenu = menu;
    }

    public void CloseMenu()
    {
        MenuHandler.currentMenu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        MenuHandler.currentMenu = null;
    }

    public void unPause()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }

    public void UpgradeOpen()
    {
        upgradeAnim.SetTrigger("down");
        this.OpenMenu(this.upgradeMenu);
    }

    public void UpgradeClose()
    {
        upgradeAnim.SetTrigger("up");
        this.CloseMenu();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void loadMenu()
    {
        Time.timeScale = 1.0f;
        MessagesMenu.isFirst = false;
        SceneManager.LoadScene("Menu2", LoadSceneMode.Single);
    }
}
