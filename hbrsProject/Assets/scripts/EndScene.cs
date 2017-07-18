using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Quit", 40.0f);
    }

    void Quit()
    {
        Application.Quit();
    }
}
