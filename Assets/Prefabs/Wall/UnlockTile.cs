using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnlockList;

public class UnlockTile : MonoBehaviour
{
    public Unlocks unlock;

    private void Start()
    {
        GameObject.Find("Grid").GetComponent<GridManager>().AddResearchMark(this.transform.position, this.gameObject);
    }
    private void OnDestroy()
    {
        GameObject.Find("GameManager").GetComponent<UnlockList>().UnlockItem(unlock);
    }
}
