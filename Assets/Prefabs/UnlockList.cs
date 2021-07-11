using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockList : MonoBehaviour
{
    [SerializeField]
    public List<Unlocks> unlockList;
    private GameManager gm;

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
    }

    private void Start()
    {
        unlockList = new List<Unlocks>();
        unlockList.Add(Unlocks.none);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        }
        else if (unlock == Unlocks.twoSpeed)
        {
            UnlockNextSpeedMax();
        }
        else if (!unlockList.Contains(unlock))
        {
            unlockList.Add(unlock);
        }
    }

    public void UnlockNextMoneyMax()
    {
        if (!unlockList.Contains(Unlocks.firstMoneyMax))
        {
            unlockList.Add(Unlocks.firstMoneyMax);
            gm.maxMoney = 100;
        }
        else if (!unlockList.Contains(Unlocks.secondMoneyMax))
        {
            unlockList.Add(Unlocks.secondMoneyMax);
            gm.maxMoney = 500;
        }
        else if (!unlockList.Contains(Unlocks.thirdMoneyMax))
        {
            unlockList.Add(Unlocks.thirdMoneyMax);
            gm.maxMoney = 1000;
        }
        else if (!unlockList.Contains(Unlocks.fourthMoneyMax))
        {
            unlockList.Add(Unlocks.thirdMoneyMax);
            gm.maxMoney = 1000;
        }
        else if (!unlockList.Contains(Unlocks.fithMoneyMax))
        {
            unlockList.Add(Unlocks.thirdMoneyMax);
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
