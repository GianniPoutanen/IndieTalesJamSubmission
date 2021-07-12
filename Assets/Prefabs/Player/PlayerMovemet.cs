using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemet : MonoBehaviour
{
    private GameManager gm;
    private GridManager grid;
    private Vector3 endPosition;

    public float speed;

    public Item heldItem;
    public SpriteRenderer itemInHand;

    // Start is called before the first frame update
    void Start()
    {
        endPosition = this.transform.position;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        grid = GameObject.Find("Grid").GetComponent<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (endPosition != this.transform.position)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, endPosition, speed);
        }

        HandleHeldItem();

        if (gm.currentMode == GameManager.GameState.Play)
            HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (grid.CheckPositionFree(endPosition + new Vector3(0, 1, 0)))
            {
                endPosition += new Vector3Int(0, 1, 0);
            }
            else
            {
                InputOutputMachine machine = InputOutputMachine.GetMachineAtPoint(this.transform.position + new Vector3Int(0, 1, 0));
                if (machine != null && machine.CanOutput(this.transform.position, this.transform.position + new Vector3Int(0, 1, 0)) && this.heldItem == null)
                {
                    this.heldItem = machine.heldItem;
                    machine.heldItem = null;
                }
                else if (machine != null && machine.CanAcceptInput(this.transform.position, this.transform.position + new Vector3Int(0, 1, 0), this.heldItem) && machine.heldItem == null)
                {
                    machine.itemBuffer = this.heldItem;
                    this.heldItem = null;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (grid.CheckPositionFree(endPosition + new Vector3(0, -1, 0)))
            {
                endPosition += new Vector3Int(0, -1, 0);
            }
            else
            {
                InputOutputMachine machine = InputOutputMachine.GetMachineAtPoint(this.transform.position + new Vector3Int(0, -1, 0));
                if (machine != null && machine.CanOutput(this.transform.position, this.transform.position + new Vector3Int(0, -1, 0)) && this.heldItem == null)
                {
                    this.heldItem = machine.heldItem;
                    machine.heldItem = null;
                }
                else if (machine != null && machine.CanAcceptInput(this.transform.position, this.transform.position + new Vector3Int(0, -1, 0), this.heldItem) && machine.heldItem == null)
                {
                    machine.itemBuffer = this.heldItem;
                    this.heldItem = null;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            if (grid.CheckPositionFree(endPosition + new Vector3(1, 0, 0)))
            {
                endPosition += new Vector3Int(1, 0, 0);
            }
            else
            {
                InputOutputMachine machine = InputOutputMachine.GetMachineAtPoint(this.transform.position + new Vector3Int(1, 0, 0));
                if (machine != null && machine.CanOutput(this.transform.position, this.transform.position + new Vector3Int(1, 0, 0)) && this.heldItem == null)
                {
                    this.heldItem = machine.heldItem;
                    machine.heldItem = null;
                }
                else if (machine != null && machine.CanAcceptInput(this.transform.position, this.transform.position + new Vector3Int(1, 0, 0), this.heldItem) && machine.heldItem == null)
                {
                    machine.itemBuffer = this.heldItem;
                    this.heldItem = null;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
            if (grid.CheckPositionFree(endPosition + new Vector3(-1, 0, 0)))
            {
                endPosition += new Vector3Int(-1, 0, 0);
            }
            else
            {
                InputOutputMachine machine = InputOutputMachine.GetMachineAtPoint(this.transform.position + new Vector3Int(-1, 0, 0));
                if (machine != null && machine.CanOutput(this.transform.position, this.transform.position + new Vector3Int(-1, 0, 0)) && this.heldItem == null)
                {
                    this.heldItem = machine.heldItem;
                    machine.heldItem = null;
                }
                else if (machine != null && machine.CanAcceptInput(this.transform.position, this.transform.position + new Vector3Int(-1, 0, 0), this.heldItem) && machine.heldItem == null)
                {
                    machine.itemBuffer = this.heldItem;
                    this.heldItem = null;
                }
            }
        }

    }

    public void HandleHeldItem()
    {
        if (heldItem == null)
        {
            itemInHand.sprite = null;
        }
        else
        {
            itemInHand.sprite = heldItem.GetComponent<SpriteRenderer>().sprite;

        }
    }
}
