using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBase : MonoBehaviour
{
    public bool jammed;
    public int cost;

    // Shape of the machine
    [SerializeField]
    public bool[,] shape;

    [SerializeField]
    public Vector3 offset;
    protected List<Vector3Int> blockedPositions = new List<Vector3Int>();

    protected GridManager grid;
    protected GameManager gm;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<GridManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        this.tag = "Machine";
        if (shape == null)
        {
            shape = GenerateGridRectangle(1, 1);
        }
        SetBlockedPositions();
    }

    public virtual bool[,] GetShape()
    {
        if (shape == null)
        {
            shape = GenerateGridRectangle(1, 1);
        }
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
                        Vector3Int blockedPosition = grid.wallMap.WorldToCell(this.transform.position);

                        grid.AddBlockedPosition(this.transform.position);
                        blockedPositions.Add(blockedPosition);
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        RemoveBlockedPositions();
        gm.AddMoney(cost);
    }

    public void RemoveBlockedPositions()
    {
        if (grid != null)
        {
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    if (shape[i, j])
                    {
                        Vector3Int blockedPosition = grid.wallMap.WorldToCell(this.transform.position);

                        grid.RemoveBlockedPosition(this.transform.position);
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

    private void OnMouseOver()
    {
        if (gm.currentMode == GameManager.GameState.Trash)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 0.2f, 0.2f);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }
    }
    private void OnMouseExit()
    {

        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

    private void OnMouseDown()
    {
        if (gm.currentMode == GameManager.GameState.Trash)
            GameObject.Destroy(this.gameObject);
    }
}
