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
        this.gameObject.SetActive(GameObject.Find("Grid").GetComponent<GridManager>().wallMap.GetTile(GameObject.Find("Grid").GetComponent<GridManager>().wallMap.WorldToCell(this.transform.position)) != null);

    }
    private void OnDestroy()
    {
        GameObject.Find("GameManager").GetComponent<UnlockList>().UnlockItem(unlock);
    }
}
