using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    public int damage = 100;
    public float cooldown = 1;

    private bool active = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player" && this.active)
        {
            this.active = false;
            Invoke("ActivateTrap", this.cooldown);
            Player player = collision.gameObject.GetComponent<Player>();
            player.AdjustHealth(-this.damage);
        }
    }

    private void ActivateTrap()
    {
        this.active = true;
    }
}
