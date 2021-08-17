using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildMapManager : MonoBehaviour
{
    List<Vector2> previousMarkerPostiions = new List<Vector2>();
    List<Vector2> currentMarkerPositions = new List<Vector2>();
    Tilemap buildMap;

    GameManager gm;
    GridManager grid;
    public RuleTile markerTile;
    public TileBase wallTile;

    public bool canBuild = false;


    // Start is called before the first frame update
    void Start()
    {
        buildMap = this.GetComponent<Tilemap>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        grid = this.transform.parent.GetComponent<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.currentMode == GameManager.GameState.Build && gm.machineToPlace != null)
        {
            SetMarkers(gm.machineToPlace.GetShape(), Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else if (gm.currentMode == GameManager.GameState.WallBuy)
        {
            SetWallMarkers(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else
        {
            ClearMarkers();
        }
    }

    public void SetMarkers(bool[,] positions, Vector2 initPos)
    {
        ClearMarkers();

        previousMarkerPostiions.Clear();
        for (int i = -positions.GetLength(0) / 2; i < ((positions.GetLength(0) / 2) + (positions.GetLength(0)) % 2); i++)
        {
            for (int j = -positions.GetLength(1) / 2; j < ((positions.GetLength(1) / 2) + (positions.GetLength(1)) % 2); j++)
            {
                previousMarkerPostiions.Add(new Vector2(initPos.x + i + (((1 + positions.GetLength(0)) % 2) * 0.5f),
                    initPos.y + j + (((1 + positions.GetLength(1)) % 2) * 0.5f)));
            }
        }

        canBuild = true;
        foreach (Vector2 cellPos in previousMarkerPostiions)
        {
            buildMap.SetTile(buildMap.WorldToCell(cellPos), markerTile);
            if (!grid.CheckPositionFree(buildMap.WorldToCell(cellPos)))
            {
                buildMap.SetColor(buildMap.WorldToCell(cellPos), new Color(1, 0, 0, 0.5f));
                canBuild = false;
            }
            else
            {
                buildMap.SetColor(buildMap.WorldToCell(cellPos), new Color(0, 1, 0, 0.5f));

            }
        }
    }
    public void SetWallMarkers(Vector2 initPos)
    {
        ClearMarkers();

        Vector2 pos = new Vector2(0, -0.5f) + initPos;

        previousMarkerPostiions.Clear();

        previousMarkerPostiions.Add(pos);


        canBuild = true;
        foreach (Vector2 cellPos in previousMarkerPostiions)
        {
            buildMap.SetTile(buildMap.WorldToCell(cellPos), markerTile);
            TileBase tile = grid.wallMap.GetTile(grid.wallMap.WorldToCell(cellPos));
            if (!(gm.CanBuyWall() && grid.NextToFloor(cellPos) && tile == wallTile))
            {
                buildMap.SetColor(buildMap.WorldToCell(cellPos), new Color(1, 0, 0, 0.5f));
                canBuild = false;
            }
            else
            {
                buildMap.SetColor(buildMap.WorldToCell(cellPos), new Color(0, 1, 0, 0.5f));

            }
        }
    }

    public void ClearMarkers()
    {
        foreach (Vector2 cellPos in previousMarkerPostiions)
        {
            buildMap.SetTile(buildMap.WorldToCell(cellPos), null);
        }
    }

    public bool[,] CreateBuildingArray(int width, int height)
    {
        bool[,] array = new bool[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                array[i, j] = true;
            }
        }

        return array;
    }
}
