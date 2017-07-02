using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomizeSprites : MonoBehaviour {
    [Header("Left Eyes")]
    public Sprite[] lEyes;
    public SpriteRenderer leftEye;

    [Header("Righy Eyes")]
    public Sprite[] rEyes;
    public SpriteRenderer rightEye;

    [Header("Mouths")]
    public Sprite[] mouths;
    public SpriteRenderer mouth;

    // Use this for initialization
    void Start () {
		if(lEyes.Length > 0)
        {
            leftEye.sprite = lEyes[Random.Range(0,lEyes.Length)];
        }
        if (rEyes.Length > 0)
        {
            rightEye.sprite = rEyes[Random.Range(0, rEyes.Length)];
        }
        if (mouths.Length > 0)
        {
            mouth.sprite = mouths[Random.Range(0, mouths.Length)];
        }
    }
}
