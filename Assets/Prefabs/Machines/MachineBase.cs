using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBase : MonoBehaviour
{
    public bool jammed;

    // Shape of the machine
    public bool[,] shape;

    [SerializeField]
    public Vector3 offset;
    protected List<Vector3Int> blockedPositions = new List<Vector3Int>();

    protected GridManager grid;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<GridManager>();
        if (shape == null)
        {
            shape = GenerateGridRectangle(1, 1);
        }
        SetBlockedPositions();
    }

    public virtual bool[,] GetShape()
    {
        return shape;
    }

    public void SetBlockedPositions()
    {
        if (grid != null)
        {
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    if (shape[i, j])
                    {
                        Vector3Int blockedPosition = grid.map.WorldToCell(this.transform.position);

                        grid.AddBlockedPosition(this.transform.position);
                        blockedPositions.Add(blockedPosition);
                    }
                }
            }
        }
    }

    public bool[,] GenerateGridRectangle(int width, int height)
    {
        bool[,] shape = new bool[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                shape[i, j] = true;
            }
        }
        return shape;
    }


}
