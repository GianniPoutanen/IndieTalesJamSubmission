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
        None,
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
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public bool IsUnlcoked(Unlocks unlock)
    {
        return unlockList.Contains(unlock);
    }

    public void UnlockItem(Unlocks unlock)
    {
        if (unlock == Unlocks.firstMoneyMax)
        {
            UnlockNextMoneyMax();
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
