using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InImage : MonoBehaviour
{
    private SpriteRenderer sr;
    private void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Item item = this.transform.parent.GetComponent<InputOutputMachine>().heldItem;
        if (item != null && this.transform.parent.GetComponent<InputOutputMachine>().CurrentStage().type == Stage.StageType.In)
        {
            sr.sprite = item.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            sr.sprite = null;
        }
    }
}
