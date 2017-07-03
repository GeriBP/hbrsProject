using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour {
    [SerializeField]
    GameObject pauseMenu, upgradeMenu;

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
        upgradeAnim.SetTrigger("down");
        Cursor.visible = true;
        upgrade = true;
        Invoke("freezeTime", 1.0f);
        isPaused = true;
    }

    public void UpgradeClose()
    {
        Time.timeScale = 1.0f;
        upgradeAnim.SetTrigger("up");
        Cursor.visible = false;
        upgrade = false;
        isPaused = false;
    }

}
