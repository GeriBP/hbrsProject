using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {
    [SerializeField]
    GameObject pauseMenu, upgradeMenu, checkPMenu;

    private Animator upgradeAnim;

    public static bool isPaused = false;
    public static bool upgrade = false;
    // Use this for initialization
    void Start () {
        upgradeAnim = upgradeMenu.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //PAUSE
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pauseMenu.SetActive(true);
            isPaused = true;
            Cursor.visible = true;
            Time.timeScale = 0.0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            pauseMenu.SetActive(false);
            isPaused = false;
            Cursor.visible = false;
            Time.timeScale = 1.0f;
        }

        //Temp
        if (Input.GetKeyDown(KeyCode.P) && !isPaused && !upgrade)
        {
            UpgradeOpen();
        }
        else if (Input.GetKeyDown(KeyCode.P) && isPaused && upgrade)
        {
            UpgradeClose();
        }
    }

    private void freezeTime()
    {
        Time.timeScale = 0.0f;
    }

    public void unPause()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
    }

    public void UpgradeOpen()
    {
        Time.timeScale = 1.0f;
        Invoke("freezeTime", 1.0f);
        upgradeAnim.SetTrigger("down");
        upgrade = true;
        checkPMenu.SetActive(false);
    }

    public void UpgradeClose()
    {
        Time.timeScale = 1.0f;
        upgradeAnim.SetTrigger("up");
        upgrade = false;
        Cursor.visible = false;
        isPaused = false;
    }

    public void CheckPOpen()
    {
        freezeTime();
        checkPMenu.SetActive(true);
        Cursor.visible = true;
        isPaused = true;
    }

    public void CheckPClose()
    {
        checkPMenu.SetActive(false);
        Cursor.visible = false;
        isPaused = false;
        Time.timeScale = 1.0f;
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
