using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {
    private int hpLvl = 1;
    private int msLvl = 1;
    private int jhLvl = 1;

    private int acLvl = 1;
    private int wdLvl = 1;
    private int rtLvl = 1;

    private int eLvl = 1;
    private int erLvl = 1;
    private int adLvl = 1;

    [Header("Upgrade Increments")]
    public int hpInc = 25;
    public int msInc = 2;
    public int jhInc = 4;

    public float acInc = 0.2f; //WE REDUCE
    public float wdInc = 0.2f;
    public float rtInc = 0.1f; //WE REDUCE

    public int eInc = 20; 
    public float adInc = 0.2f;
    public float erInc = 0.01f;

    [Header("Upgrade Initial Costs")]
    public int hpCost = 100;
    public int msCost = 100;
    public int jhCost = 100;

    public int acCost = 100;
    public int wdCost = 100;
    public int rtCost = 100;

    public int eCost = 100;
    public int adCost = 100;
    public int erCost = 100;

    [Header("Upgrade Current Lvl Display")]
    public Text hpTxt;
    public Text msTxt, jhTxt, acTxt, wdTxt, rtTxt, eTxt, adTxt, erTxt;

    [Header("Upgrade Current Cost Display")]
    public Text hpTxtC;
    public Text msTxtC, jhTxtC, acTxtC, wdTxtC, rtTxtC, eTxtC, adTxtC, erTxtC;

    [Header("Upgrade Increment Mult")]
    public float upradeInc = 0.1f;

    [Header("Don't touch")]
    public static int money = 0;

    private Player playerS;
    private string costStr = "Cost: \n";
    // Use this for initialization
    void Start () {
        playerS = GameObject.Find("Player").GetComponent<Player>();
        hpTxtC.text = costStr + hpCost.ToString();
        msTxtC.text = costStr + msCost.ToString();
        jhTxtC.text = costStr + jhCost.ToString();
        acTxtC.text = costStr + acCost.ToString();
        wdTxtC.text = costStr + wdCost.ToString();
        rtTxtC.text = costStr + rtCost.ToString();
        eTxtC.text = costStr + eCost.ToString();
        erTxtC.text = costStr + erCost.ToString();
        adTxtC.text = costStr + adCost.ToString();
    }
	
    public void BuyHp()
    {
        if (money - hpCost >= 0 && hpLvl < 5)
        {
            money = money - hpCost;
            hpLvl++;
            playerS.maxHealth += hpInc;
            playerS.currentHealth = playerS.maxHealth;
            hpCost = (int)Mathf.Round((float)hpCost * (float)(1 + (hpLvl * upradeInc))/ 10.0f) * 10;
            if(hpLvl < 5) hpTxtC.text = costStr + hpCost.ToString();
            else hpTxtC.text = "MAX\n LEVEL";
            hpTxt.text = hpLvl.ToString() + "/5";
        }
        else
        {
            //PLAY SOUND (*MEEEEEEC*)
        }
    }
}
