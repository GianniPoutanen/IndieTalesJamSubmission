using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockList : MonoBehaviour
{

    List<Unlocks> unlockList;

    public enum Unlocks
    {
        // Machines
        grabber,
        melter,
        // Game Upgrades
        twoSpeed,
        fourSpeed,
        eightSpeed,
        twoMoney,
        threeMoney,
        firstMoneyMax,
        secondMoneyMax,
        thirdMoneyMax,
    }

    private void Start()
    {
        unlockList = new List<Unlocks>();
    }


    public bool IsUnlcoked(Unlocks unlock)
    {
        return unlockList.Contains(unlock);
    }

    public void UnlockItem(Unlocks unlock)
    {
        if (!unlockList.Contains(unlock))
        {
            unlockList.Add(unlock);
        }
    }

    public void UnlockNextMoneyMax()
    {
        if (!unlockList.Contains(Unlocks.firstMoneyMax))
        {
            unlockList.Add(Unlocks.firstMoneyMax);
        }
        else if (!unlockList.Contains(Unlocks.secondMoneyMax))
        {
            unlockList.Add(Unlocks.secondMoneyMax);
        }
        else if (!unlockList.Contains(Unlocks.thirdMoneyMax))
        {
            unlockList.Add(Unlocks.thirdMoneyMax);
        }
    }
}
