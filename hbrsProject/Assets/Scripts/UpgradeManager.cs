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
    public int hpInc;
    public float msInc;
    public int jhInc;

    public float acInc; //WE REDUCE
    public float wdInc;
    public float rtInc; //WE REDUCE

    public int eInc;
    public float adInc;
    public float erInc;

    [Header("Upgrade Initial Costs")]
    public int hpCost;
    public int msCost;
    public int jhCost;

    public int acCost;
    public int wdCost;
    public int rtCost;

    public int eCost;
    public int adCost;
    public int erCost;

    [Header("Upgrade Current Lvl Display")]
    public Text hpTxt;
    public Text msTxt, jhTxt, acTxt, wdTxt, rtTxt, eTxt, adTxt, erTxt;

    [Header("Upgrade Current Cost Display")]
    public Text hpTxtC;
    public Text msTxtC, jhTxtC, acTxtC, wdTxtC, rtTxtC, eTxtC, adTxtC, erTxtC;

    [Header("Upgrade Increment Mult")]
    public float upradeInc = 0.1f;

    [Header("Money Display")]
    public Text moneyDisp;
    
    private int money = 0;

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

        moneyDisp.text = money.ToString() + "$";
    }
	
    public void BuyHp()
    {
        if (money - hpCost >= 0 && hpLvl < 5)
        {
            ModMoney(-hpCost);
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

    public void BuyMs()
    {
        if (money - msCost >= 0 && msLvl < 5)
        {
            ModMoney(-msCost);
            msCost = (int)Mathf.Round((float)msCost * (float)(1 + (msLvl * upradeInc)) / 10.0f) * 10;
            msLvl++;
            if (msLvl < 5) msTxtC.text = costStr + msCost.ToString();
            else msTxtC.text = "MAX\n LEVEL";
            msTxt.text = msLvl.ToString() + "/5";

            Debug.Log("Increase MS ("+ playerS.movementSpeed + ") by " + msInc +"and the result is:");
            playerS.movementSpeed += msInc;
            Debug.Log(playerS.movementSpeed);
        }
        else
        {
            //PLAY SOUND (*MEEEEEEC*)
        }
    }

    public void BuyJh()
    {
        if (money - jhCost >= 0 && jhLvl < 5)
        {
            ModMoney(-jhCost);
            jhCost = (int)Mathf.Round((float)jhCost * (float)(1 + (jhLvl * upradeInc)) / 10.0f) * 10;
            jhLvl++;
            if (jhLvl < 5) jhTxtC.text = costStr + jhCost.ToString();
            else jhTxtC.text = "MAX\n LEVEL";
            jhTxt.text = jhLvl.ToString() + "/5";

            playerS.jumpForce += jhInc;
        }
        else
        {
            //PLAY SOUND (*MEEEEEEC*)
        }
    }

    public void BuyAc()
    {
        if (money - acCost >= 0 && acLvl < 5)
        {
            ModMoney(-acCost);
            acCost = (int)Mathf.Round((float)acCost * (float)(1 + (acLvl * upradeInc)) / 10.0f) * 10;
            acLvl++;
            if (acLvl < 5) acTxtC.text = costStr + acCost.ToString();
            else acTxtC.text = "MAX\n LEVEL";
            acTxt.text = acLvl.ToString() + "/5";

            playerS.accuracyMultiplier -= acInc;
        }
        else
        {
            //PLAY SOUND (*MEEEEEEC*)
        }
    }

    public void BuyWd()
    {
        if (money - wdCost >= 0 && wdLvl < 5)
        {
            ModMoney(-wdCost);
            wdCost = (int)Mathf.Round((float)wdCost * (float)(1 + (wdLvl * upradeInc)) / 10.0f) * 10;
            wdLvl++;
            if (wdLvl < 5) wdTxtC.text = costStr + wdCost.ToString();
            else wdTxtC.text = "MAX\n LEVEL";
            wdTxt.text = wdLvl.ToString() + "/5";

            playerS.damageMultiplier += wdInc;
        }
        else
        {
            //PLAY SOUND (*MEEEEEEC*)
        }
    }

    public void BuyRt()
    {
        if (money - rtCost >= 0 && rtLvl < 5)
        {
            ModMoney(-rtCost);
            rtCost = (int)Mathf.Round((float)rtCost * (float)(1 + (rtLvl * upradeInc)) / 10.0f) * 10;
            rtLvl++;
            if (rtLvl < 5)rtTxtC.text = costStr + rtCost.ToString();
            else rtTxtC.text = "MAX\n LEVEL";
            rtTxt.text = rtLvl.ToString() + "/5";

            playerS.reloadSpeedMultiplier -= rtInc;
        }
        else
        {
            //PLAY SOUND (*MEEEEEEC*)
        }
    }

    public void BuyE()
    {
        if (money - eCost >= 0 && eLvl < 5)
        {
            ModMoney(-eCost);
            eCost = (int)Mathf.Round((float)eCost * (float)(1 + (eLvl * upradeInc)) / 10.0f) * 10;
            eLvl++;
            if (eLvl < 5) eTxtC.text = costStr + eCost.ToString();
            else eTxtC.text = "MAX\n LEVEL";
            eTxt.text = eLvl.ToString() + "/5";

            playerS.maxEnergy += eInc;
            playerS.currentEnergy = playerS.maxEnergy;
        }
        else
        {
            //PLAY SOUND (*MEEEEEEC*)
        }
    }

    public void BuyAd()
    {
        if (money - adCost >= 0 && adLvl < 5)
        {
            ModMoney(-adCost);
            adCost = (int)Mathf.Round((float)adCost * (float)(1 + (adLvl * upradeInc)) / 10.0f) * 10;
            adLvl++;
            if (adLvl < 5) adTxtC.text = costStr + adCost.ToString();
            else adTxtC.text = "MAX\n LEVEL";
            adTxt.text = adLvl.ToString() + "/5";

            playerS.abilityMultiplier += adInc;
        }
        else
        {
            //PLAY SOUND (*MEEEEEEC*)
        }
    }

    public void BuyEr()
    {
        if (money - erCost >= 0 && erLvl < 5)
        {
            ModMoney(-erCost);
            erCost = (int)Mathf.Round((float)erCost * (float)(1 + (erLvl * upradeInc)) / 10.0f) * 10;
            erLvl++;
            if (erLvl < 5) erTxtC.text = costStr + erCost.ToString();
            else erTxtC.text = "MAX\n LEVEL";
            erTxt.text = erLvl.ToString() + "/5";

            playerS.energyRegenerationMultiplier += erInc;
        }
        else
        {
            //PLAY SOUND (*MEEEEEEC*)
        }
    }

    // Adds 'value' to the money count (used to add and substract money)
    // Checks must be done before
    public void ModMoney(int value)
    {
        money += value;
        moneyDisp.text = money.ToString() + "$";
    }
}
