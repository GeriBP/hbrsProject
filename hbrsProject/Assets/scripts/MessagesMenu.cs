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
    private int currMessage = 0;
    private bool canPress = false;
    // Use this for initialization
    void Start () {
        DisplayText.text = messages[currMessage];
        Invoke("enablePress", 5.0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey && canPress)
        {
            canPress = false;
            Invoke("enablePress", 1.0f);
            ++currMessage;
            DisplayText.text = messages[currMessage];
            if(currMessage == messages.Length - 1)
            {
                CancelInvoke("enablePress");
                //Go black
                //Load next scene
            }
        }
	}

    void enablePress()
    {
        canPress = true;
    }
}
