using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockList : MonoBehaviour
{
    [SerializeField]
    public List<Unlocks> unlockList;
    private GameManager gm;
    private GameObject player;

    public GameObject maxIncreaseEffect;
    public GameObject speedUpgradeEffect;
    public GameObject unlockBuildingEffect;
    public GameObject x2MoneyEffect;
    public enum Unlocks
    {
        // None
        none,
        // Machines
        Producer,
        Seller,
        grabber,
        melter,
        Press,
        FinalMachine,
        // Game Upgrades
        twoSpeed,
        fourSpeed,
        eightSpeed,
        twoMoney,
        threeMoney,
        firstMoneyMax,
        secondMoneyMax,
        thirdMoneyMax,
        fourthMoneyMax,
        fithMoneyMax,
        Cuber,
        Portal
    }

    private void Start()
    {
        unlockList = new List<Unlocks>();
        unlockList.Add(Unlocks.none);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
    }


    public bool IsUnlocked(Unlocks unlock)
    {
        return unlockList.Contains(unlock);
    }

    public void UnlockItem(Unlocks unlock)
    {
        if (unlock == Unlocks.firstMoneyMax)
        {
            UnlockNextMoneyMax();
            GameObject obj = Instantiate(maxIncreaseEffect);
            Vector3 pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            obj.transform.position = pos;
        }
        else if (unlock == Unlocks.twoSpeed)
        {
            UnlockNextSpeedMax();
            GameObject obj = Instantiate(speedUpgradeEffect);
            Vector3 pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            obj.transform.position = pos;
        }
        else if (!unlockList.Contains(unlock))
        {
            unlockList.Add(unlock);
            if (unlock != Unlocks.twoMoney)
            {
                GameObject obj = Instantiate(unlockBuildingEffect);
                Vector3 pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                obj.transform.position = pos;
            }
        }
        if (unlock == Unlocks.twoMoney)
        {
            GameObject obj = Instantiate(x2MoneyEffect);
            Vector3 pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            obj.transform.position = pos;
        }

    }

    public void UnlockNextMoneyMax()
    {
        if (!unlockList.Contains(Unlocks.secondMoneyMax))
        {
            unlockList.Add(Unlocks.secondMoneyMax);
            gm.maxMoney = 100;
        }
        else if (!unlockList.Contains(Unlocks.thirdMoneyMax))
        {
            unlockList.Add(Unlocks.thirdMoneyMax);
            gm.maxMoney = 500;
        }
        else if (!unlockList.Contains(Unlocks.fourthMoneyMax))
        {
            unlockList.Add(Unlocks.fourthMoneyMax);
            gm.maxMoney = 1000;
        }
        else if (!unlockList.Contains(Unlocks.fithMoneyMax))
        {
            unlockList.Add(Unlocks.fithMoneyMax);
            gm.maxMoney = 100000;
        }
    }
    public void UnlockNextSpeedMax()
    {
        if (!unlockList.Contains(Unlocks.twoSpeed))
        {
            unlockList.Add(Unlocks.firstMoneyMax);
        }
        else if (!unlockList.Contains(Unlocks.fourSpeed))
        {
            unlockList.Add(Unlocks.secondMoneyMax);
        }
        else if (!unlockList.Contains(Unlocks.eightSpeed))
        {
            unlockList.Add(Unlocks.thirdMoneyMax);
        }
        gm.tickTime = gm.tickTime / 2f;
    }
}
