using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingMachine : InputOutputMachine
{
    public int maxValue;
    public int heldValue;
    private void Update()
    {
        if (this.heldItem != null)
        {
            AddValue();
            this.heldItem = null;
        }
    }

    public override bool CanAcceptInput(Vector3 machinePos, Vector3 inputPosition, Item item)
    {
        return true;
    }

    public override void StageUpdate()
    {
        if (this.CurrentStage().type == Stage.StageType.Sell)
        {
            SellValue();
            StageNext();
        }
        else
        {
            base.StageUpdate();
        }
    }

    public void AddValue()
    {
        if (heldValue + this.heldItem.value > maxValue)
        {
            heldValue = maxValue;
            // TODO Lost Value Animation
        }
        else
        {
            heldValue += this.heldItem.value;
        }
    }

    public void SellValue()
    {
        heldValue -= gm.AddMoney(heldValue);
    }
}
