using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap map;
    public Tilemap buildMap;
    public GameManager gm;

    [Header("Tile Variables")]
    public RuleTile wallTile;
    public RuleTile floorTile;

    // Positions on the map blocked by an object/s
    public List<Vector3Int> blockedPositions = new List<Vector3Int>();

    public void BreakWall(Vector3Int pos)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Vector3Int tempOffset = new Vector3Int(i, j, 0);
                if (map.GetTile(pos + tempOffset) == null)
                {
                    map.SetTile(pos + tempOffset, wallTile);
                }
            }
        }
        gm.BuyWall();
        map.SetTile(pos, floorTile);
    }


    public void AddBlockedPosition(Vector3 pos)
    {
        blockedPositions.Add(this.GetComponent<Grid>().WorldToCell(pos));
    }

    public bool CheckPositionFree(Vector3Int pos)
    {
        return !blockedPositions.Contains(pos) && map.GetTile(pos) == floorTile;
    }
    public bool CheckPositionFree(Vector3 pos)
    {
        return CheckPositionFree(map.WorldToCell(pos));
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
