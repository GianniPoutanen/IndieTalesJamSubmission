using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputOutputMachine : MonoBehaviour
{
    [Header("Stages State Behaviour")]
    [SerializeField]
    public Stage[] stages;
    public int stageIndex;
    public Item itemBuffer;
    public Item heldItem;

    [Space(10)]
    [Header("Insert Machine Code Here")]
    public MachineBase machine;

    public bool drawGizmoMarkers;

    private GridManager grid;
    public GameManager gm;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<GridManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.AddMachine(this);
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        machine = this.GetComponent<MachineBase>();
    }

    private void OnDestroy()
    {
        gm.RemoveMachine(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (stages[stageIndex].type == Stage.StageType.In)
        {
            if (this.CurrentStage().inputs.Length > 0 || this.CurrentStage().inputRestricted)
            {
                machine.jammed = !CheckInputClear();
            }
        }
        if (this.CurrentStage().type == Stage.StageType.Out)
        {
            if (this.CurrentStage().outputs.Length > 0 || this.CurrentStage().outputRestricted)
            {
                machine.jammed = !CheckOutputClear();
            }
        }

        if (this.spriteRenderer.sprite != this.CurrentStage().sprite)
        {
            this.spriteRenderer.sprite = this.CurrentStage().sprite;
        }
    }

    public virtual void StageUpdate()
    {
        if (!machine.jammed)
        {
            if (this.CurrentStage().type == Stage.StageType.Processs)
            {
                if (heldItem != this.CurrentStage().processItem)
                {
                    heldItem = this.CurrentStage().processItem;
                    StageNext();
                }
                else
                {
                    return;
                }
            }
            else if (stages[stageIndex].type == Stage.StageType.In)
            {
                HandleInput();
            }
            else if (stages[stageIndex].type == Stage.StageType.Out)
            {
                HandleOutput();
            }
            else if (stages[stageIndex].type == Stage.StageType.Trash)
            {
                if (heldItem != null)
                {
                    heldItem = null;
                }
                StageNext();
            }
            else if (stages[stageIndex].type == Stage.StageType.Trash)
            {
                if (heldItem != null)
                {
                    gm.currentMoney = heldItem.value;
                    heldItem = null;
                }
                StageNext();
            }
            else
            {
                StageNext();
            }
        }
    }

    public void StageUpdateFinished()
    {
        if (itemBuffer != null && heldItem == null)
        {
            heldItem = itemBuffer;
            itemBuffer = null;
        }
        if (this.CurrentStage().type == Stage.StageType.In && this.heldItem != null)
        {
            StageNext();
        }
        if (this.CurrentStage().type == Stage.StageType.Out && this.heldItem == null)
        {
            StageNext();
        }
    }


    public Stage CurrentStage()
    {
        return this.stages[this.stageIndex];
    }

    public bool HandleOutput()
    {
        foreach (Out output in this.CurrentStage().outputs)
        {
            InputOutputMachine machine = GetMachineAtPoint(this.transform.position + output.pos);
            if (machine != null && machine.heldItem == null)
            {
                machine.itemBuffer = this.heldItem;
                heldItem = null;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public bool HandleInput()
    {
        foreach (In input in this.CurrentStage().inputs)
        {
            InputOutputMachine machine = GetMachineAtPoint(this.transform.position + input.pos);
            if (machine != null && machine.heldItem != null)
            {
                itemBuffer = machine.heldItem;
                machine.heldItem = null;
            }
        }
        return itemBuffer != null;
    }

    public bool CheckInputClear()
    {
        foreach (In input in stages[stageIndex].inputs)
        {
            InputOutputMachine machine = GetMachineAtPoint(this.transform.position + input.pos);
            if (machine != null && !machine.CanOutput(this.transform.position, this.transform.position + input.pos))
            {
                return false;
            }
        }
        return true;
    }

    public bool CheckOutputClear()
    {
        foreach (Out output in stages[stageIndex].outputs)
        {
            InputOutputMachine machine = GetMachineAtPoint(this.transform.position + output.pos);
            if (machine != null && !machine.CanAcceptInput(this.transform.position, this.transform.position + output.pos, this.heldItem))
            {
                return false;
            }
        }
        return true;
    }


    public bool CanOutput(Vector3 machinePos, Vector3 outputPosition)
    {
        if (heldItem != null && CurrentStage().type == Stage.StageType.Out)
        {
            if (!this.CurrentStage().outputRestricted)
                return true;

            foreach (Out output in this.CurrentStage().outputs)
            {
                if ((output.pos + this.transform.position) == machinePos && this.transform.position == outputPosition)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public virtual bool CanAcceptInput(Vector3 machinePos, Vector3 inputPosition, Item item)
    {
        if (itemBuffer == null && CurrentStage().type == Stage.StageType.In)
        {
            if (!this.CurrentStage().inputRestricted)
                return this.CurrentStage().inputTypeRestriction == null || item == this.CurrentStage().inputTypeRestriction;

            foreach (In input in this.CurrentStage().inputs)
            {
                if ((input.pos + this.transform.position) == machinePos && this.transform.position == inputPosition)
                {
                    return this.CurrentStage().inputTypeRestriction == null || item == this.CurrentStage().inputTypeRestriction;
                }
            }
        }
        return false;
    }

    public static InputOutputMachine GetMachineAtPoint(Vector3 pos)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(pos, Vector2.zero, 0f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform != null && hit.transform.gameObject != null && hit.transform.tag == "Machine")
            {
                return hit.transform.gameObject.GetComponent<InputOutputMachine>();
            }
        }
        return null;
    }

    public void StageNext()
    {
        stageIndex++;
        if (stageIndex >= stages.Length)
        {
            stageIndex = 0;
        }
        spriteRenderer.sprite = stages[stageIndex].sprite;
    }

    private void OnDrawGizmos()
    {
        if (drawGizmoMarkers)
        {
            foreach (In input in stages[stageIndex].inputs)
            {
                Gizmos.DrawCube(this.transform.position + input.pos, new Vector3(0.5f, 0.5f, 0.5f));
            }
            foreach (Out output in stages[stageIndex].outputs)
            {
                Gizmos.DrawCube(this.transform.position + output.pos, new Vector3(0.5f, 0.5f, 0.5f));
            }
        }
    }
}




[System.Serializable]
public class Stage
{
    public enum StageType
    {
        Idle,
        In,
        Out,
        TryIn,
        TryOut,
        Processs,
        Trash,
        Sell
    }

    public StageType type;

    public bool inputRestricted = false;
    public Item inputTypeRestriction;
    public In[] inputs;
    public bool outputRestricted = false;
    public Out[] outputs;

    public GameObject effect;

    public Item processItem;

    [Header("StageSprite")]
    public Sprite sprite;
}

[System.Serializable]
public class In
{
    public Vector3 pos;
}

[System.Serializable]
public class Out
{
    public Vector3 pos;
}