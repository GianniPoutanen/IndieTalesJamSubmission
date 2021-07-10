using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyWallButton : MonoBehaviour
{
    public void OnClick()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().SetBuyWallMode() ;
    }
}
