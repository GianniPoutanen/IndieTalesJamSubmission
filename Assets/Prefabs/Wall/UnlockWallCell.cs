using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnlockWallCell : MonoBehaviour
{
    private GridManager grid;
    private GameManager gm;

    public TileBase replacementWall;

    public int WallBuildUnlockNumber;

    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<GridManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.buyNumber >= WallBuildUnlockNumber)
        {
            grid.wallMap.SetTile(grid.buildMap.WorldToCell(this.transform.position), replacementWall);
        }
    }
}
