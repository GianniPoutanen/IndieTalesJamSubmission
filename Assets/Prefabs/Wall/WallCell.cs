using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallCell : MonoBehaviour
{
    private GridManager grid;
    private GameManager gm;

    public TileBase floorTile;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<GridManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    private void OnMouseDown()
    {
        if (gm.currentMode == GameManager.GameState.WallBuy && CanDestory() && grid.gm.CanBuyWall() && grid.buildMap.GetComponent<BuildMapManager>().canBuild)
        {
            grid.BreakWall(grid.wallMap.WorldToCell(this.transform.parent.position));
        }
    }

    public bool CanDestory()
    {
        int[] x = new int[] { -1, 0, 1, 0 };
        int[] y = new int[] { 0, 1, 0, -1 };
        for (int i = 0; i < 4; i++)
        {
            TileBase tile = grid.floorMap.GetTile(grid.floorMap.WorldToCell(this.transform.parent.position + new Vector3(x[i], y[i], 0)));
            if (tile == floorTile)
            {
                return true;
            }
        }

        return false;
    }
}
