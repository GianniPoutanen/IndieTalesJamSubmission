using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap wallMap;
    public Tilemap buildMap;
    public Tilemap floorMap;
    public GameManager gm;

    [Header("Tile Variables")]
    public RuleTile wallTile;
    public RuleTile floorTile;
    public TileBase innerWallTile;

    // Positions on the map blocked by an object/s
    public List<Vector3Int> blockedPositions = new List<Vector3Int>();

    public void BreakWall(Vector3Int pos)
    {
        floorMap.SetTile(pos, floorTile);
        wallMap.SetTile(pos, null);
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Vector3Int tempOffset = new Vector3Int(i, j, 0);
                if (wallMap.GetTile(pos + tempOffset) == null && floorMap.GetTile(pos + tempOffset) == null)
                {
                    if (!AboveFloor(pos + tempOffset))
                    {
                        wallMap.SetTile(pos + tempOffset, wallTile);
                    }
                    else
                    {
                        wallMap.SetTile(pos + tempOffset, innerWallTile);
                    }
                }
            }
        }
        gm.BuyWall();
    }

    public bool AboveFloor(Vector3Int initialPos)
    {
        TileBase tile = floorMap.GetTile(floorMap.WorldToCell(initialPos + new Vector3(0, -1, 0)));
        if (tile == floorTile)
        {
            return true;
        }
        return false;
    }

    public void AddBlockedPosition(Vector3 pos)
    {
        blockedPositions.Add(this.GetComponent<Grid>().WorldToCell(pos));
    }

    public bool CheckPositionFree(Vector3Int pos)
    {
        return !blockedPositions.Contains(pos) && floorMap.GetTile(pos) == floorTile;
    }
    public bool CheckPositionFree(Vector3 pos)
    {
        return CheckPositionFree(wallMap.WorldToCell(pos));
    }

    public bool CheckPositionsFree(List<Vector3Int> positions)
    {
        foreach (Vector3Int pos in positions)
        {
            if (!CheckPositionFree(pos))
            {
                return false;
            }
        }
        return true;
    }

}
