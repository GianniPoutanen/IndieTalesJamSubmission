using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCell : MonoBehaviour
{
    private GridManager grid;
    private GameManager gm;

    public RuleTile floorTile;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        grid = GameObject.Find("Grid").GetComponent<GridManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    private void OnMouseDown()
    {
        if (gm.currentMode == GameManager.GameState.WallBuy && CanDestory() && grid.gm.CanBuyWall())
        {
            grid.BreakWall(grid.map.WorldToCell(this.transform.position));
        }
    }

    public bool CanDestory()
    {
        int[] x = new int[] { -1, 0, 1, 0 };
        int[] y = new int[] { 0, 1, 0, - 1 };
        for (int i = 0; i < 4   ; i++)
        {
            if (grid.map.GetTile(grid.map.WorldToCell(this.transform.position + new Vector3(x[i],y[i],0))) == floorTile)
            {
                return true;
            }
        }

        return false;
    }
}
