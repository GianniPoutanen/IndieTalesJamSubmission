using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildOptionButton : MonoBehaviour
{
    public MachineBase machine;
    public int cost;

    public void OnClick()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().SetBuildObject(machine, cost);
        this.transform.parent.gameObject.SetActive(false);
    }
}
