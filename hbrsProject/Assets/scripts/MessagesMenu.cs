using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MessagesMenu : MonoBehaviour {
    [SerializeField]
    string[] messages;
    [SerializeField]
    Text DisplayText;
    [SerializeField]
    Animator doorsAnim;
    [SerializeField]
    GameObject screen;
    private int currMessage = 0;
    private bool canPress = false;
    // Use this for initialization
    void Start () {
        DisplayText.text = messages[currMessage];
        Invoke("enablePress", 5.0f);
        Cursor.visible = false;
        Invoke("enableCursor", 5.0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey && canPress)
        {
            if (currMessage == messages.Length - 1)
            {
                CancelInvoke("enablePress");
                doorsAnim.SetTrigger("open");
                Cursor.visible = false;
                screen.SetActive(false);
                Invoke("loadLevel", 2.0f);
            }
            else
            {
                canPress = false;
                Invoke("enablePress", 1.0f);
                ++currMessage;
                DisplayText.text = messages[currMessage];
            }
        }
	}

    void enablePress()
    {
        canPress = true;
    }

    void enableCursor()
    {
        Cursor.visible = true;
    }

    void loadLevel()
    {
        SceneManager.LoadScene("FirstLevel", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
