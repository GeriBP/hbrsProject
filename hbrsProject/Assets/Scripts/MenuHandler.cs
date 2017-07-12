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

    private static GameObject currentMenu;
	
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
        if (MenuHandler.currentMenu)
            this.CloseMenu();

        Time.timeScale = 1;
        float timeTillFreeze = 0;
        Cursor.visible = true;
        menu.SetActive(true);
        MenuHandler.currentMenu = menu;

        Animator animator = menu.GetComponent<Animator>();
        if (animator)
        {
            animator.SetTrigger("down");
            timeTillFreeze = 1;
        }

        Invoke("FreezeTime", timeTillFreeze);
    }

    public void CloseMenu()
    {
        Animator animator = MenuHandler.currentMenu.GetComponent<Animator>();
        if (animator)
        {
            animator.SetTrigger("up");
        }
        MenuHandler.currentMenu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        MenuHandler.currentMenu = null;
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void FreezeTime()
    {
        Time.timeScale = 0;
    }
}
