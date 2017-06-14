using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public int maxHealth = 100;
    public int currentHealth = 100;
    public float healthBarOffset = 0.35f;
    public GameObject healthBarPrefab;
    public Canvas canvas;

    private GameObject healthBar;
    private Slider healthBarSlider;

	// Use this for initialization
	void Start () {
        this.healthBar = GameObject.Instantiate(this.healthBarPrefab);
        this.healthBar.transform.SetParent(this.canvas.transform);

        this.healthBarSlider = this.healthBar.GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        this.healthBarSlider.value = this.currentHealth / (float)this.maxHealth;
        this.healthBar.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + this.healthBarOffset, this.transform.position.z);
	}

    public void Adjust(int change)
    {
        this.currentHealth += change;
    }
}
