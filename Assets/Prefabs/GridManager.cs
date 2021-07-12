using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap wallMap;
    public Tilemap buildMap;
    public Tilemap floorMap;
    public Tilemap unlockMap;
    public GameManager gm;

    [Header("Tile Variables")]
    public RuleTile wallTile;
    public TileBase floorTile;
    public TileBase innerWallTile;
    public TileBase belowWallTile;


    // Positions on the map blocked by an object/s
    public List<Vector3Int> blockedPositions = new List<Vector3Int>();
    private Dictionary<Vector3Int, GameObject> markPosition = new Dictionary<Vector3Int, GameObject>();

    public void BreakWall(Vector3Int pos)
    {
        floorMap.SetTile(pos, floorTile);
        if (HasResearchMark(pos))
        {
            DestroyResearchMark(pos);
        }
        wallMap.SetTile(pos, null);
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Vector3Int tempOffset = new Vector3Int(i, j, 0);
                if (wallMap.GetTile(pos + tempOffset) == null && floorMap.GetTile(pos + tempOffset) == null)
                {
                    //TODO fix this
                    if (unlockMap.GetTile(pos + tempOffset) == null)
                    {
                        wallMap.SetTile(pos + tempOffset, wallTile);
                    }
                    else
                    {
                        wallMap.SetTile(pos + tempOffset, unlockMap.GetTile(pos + tempOffset));

                    }
                    if (HasResearchMark(pos + tempOffset))
                    {
                        markPosition[pos + tempOffset].SetActive(true);
                    }
                }
            }
        }
        gm.BuyWall();
    }

    public bool AboveFloor(Vector3Int initialPos)
    {
        TileBase tile = floorMap.GetTile(floorMap.WorldToCell(initialPos));
        if (tile == floorTile)
        {
            return true;
        }
        return false;
    }

    public bool BelowFloor(Vector3Int initialPos)
    {
        TileBase tile = floorMap.GetTile(floorMap.WorldToCell(initialPos + new Vector3Int(-3, 1, 0)));
        if (tile == floorTile)
        {
            return true;
        }
        return false;
    }

    public bool NextToFloor(Vector3 pos)
    {
        int[] x = new int[] { -1, 0, 1, 0 };
        int[] y = new int[] { 0, 1, 0, -1 };
        for (int i = 0; i < 4; i++)
        {
            TileBase tile = floorMap.GetTile(floorMap.WorldToCell(pos + new Vector3(x[i], y[i], 0)));
            if (tile == floorTile)
            {
                return true;
            }
        }

        return false;
    }

    public void AddBlockedPosition(Vector3 pos)
    {
        blockedPositions.Add(this.GetComponent<Grid>().WorldToCell(pos));
    }
    public void RemoveBlockedPosition(Vector3 pos)
    {
        blockedPositions.Remove(this.GetComponent<Grid>().WorldToCell(pos));
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


    public void AddResearchMark(Vector3 pos, GameObject obj)
    {
        markPosition.Add(wallMap.WorldToCell(pos), obj);
    }

    public bool HasResearchMark(Vector3 pos)
    {
        return markPosition.ContainsKey(wallMap.WorldToCell(pos));
    }
    public bool HasResearchMark(Vector3Int pos)
    {
        return markPosition.ContainsKey(pos);
    }

    public void DestroyResearchMark(Vector3Int pos)
    {
        GameObject.Destroy(markPosition[pos]);
        markPosition.Remove(wallMap.WorldToCell(pos));
    }
}
