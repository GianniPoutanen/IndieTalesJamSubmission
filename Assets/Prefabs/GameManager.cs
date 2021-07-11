using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int wallValue = 1;
    public int buyNumber = 1;
    [Range(0f, 2f)]
    public float wallValueFactorRange;

    public int maxMoney = 10;
    public int currentMoney = 5;

    [Header("Curser Option")]
    public GameObject curserObject;

    [Header("Building Variables")]
    public MachineBase machineToPlace;
    public int buildCost;
    public GameObject buildMenu;
    public bool building;

    public List<InputOutputMachine> machines = new List<InputOutputMachine>();

    [Header("Timer Variables")]
    public float tickTime = 2;
    private float tickTimer;

    public enum GameState
    {
        Play,
        Build,
        Trash,
        WallBuy,
        Pause
    }
    public GameState currentMode;

    private void Start()
    {
        Cursor.visible = false;
        tickTimer = tickTime;
    }

    private void Update()
    {
        if (currentMode == GameState.Play)
        {
            if (tickTimer <= 0)
            {
                UpdateMachineStates();
                tickTimer = tickTime;
            }
            else
            {
                tickTimer -= Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleMenuEscape();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (buildMenu.activeSelf)
            {
                ClearBuildObject();
                buildMenu.SetActive(false);
                currentMode = GameState.Play;
                Cursor.visible = false;
            }
            else
            {
                ClearBuildObject();
                buildMenu.SetActive(true);
                currentMode = GameState.Pause;
                Cursor.visible = true;
            }
        }

        SetCurserObjectPosition();

        if (building && Input.GetMouseButtonDown(0) && CanBuildMachine())
        {
            HandleBuildMachine();
        }
    }

    public bool CanBuildMachine()
    {
        return buildCost <= currentMoney;
    }
    public bool CanBuildMachine(int cost)
    {
        return cost <= currentMoney;
    }

    public void HandleBuildMachine()
    {
        Vector3 fixedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) +
               new Vector3(((1 + machineToPlace.width % 2) * 0.5f), ((1 + machineToPlace.height % 2) * 0.5f), 0);
        fixedPos = Vector3Int.RoundToInt(fixedPos);
        GameObject newMachine = Instantiate(machineToPlace.gameObject);
        newMachine.transform.position = new Vector3(fixedPos.x - ((1 + machineToPlace.width % 2) * 0.5f), fixedPos.y - ((1 + machineToPlace.height % 2) * 0.5f), 0);

        this.currentMoney -= buildCost;
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            ClearBuildObject();
            currentMode = GameState.Play;
        }
    }

    public void SetCurserObjectPosition()
    {
        if (machineToPlace != null)
        {
            Vector3 fixedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) +
                new Vector3(((1 + machineToPlace.width % 2) * 0.5f), ((1 + machineToPlace.height % 2) * 0.5f), 0);
            if (building)
            {
                fixedPos = Vector3Int.RoundToInt(fixedPos);
            }
            curserObject.transform.position = new Vector3(fixedPos.x - ((1 + machineToPlace.width % 2) * 0.5f), fixedPos.y - ((1 + machineToPlace.height % 2) * 0.5f), 0);
        }
    }

    private void OnEnable()
    {
        Cursor.visible = true;
    }

    /// <summary>
    /// Returns the negative difference
    /// </summary>
    /// <returns></returns>
    public int AddMoney(int money)
    {
        int difference = 0;
        if (currentMoney + money > maxMoney)
        {
            difference = currentMoney + money - maxMoney;
            currentMoney = 10;
        }
        else
        {
            currentMoney += money;
        }
        return difference;
    }

    public void HandleMenuEscape()
    {
        if (building)
        {
            ClearBuildObject();
            buildMenu.SetActive(true);
            ClearBuildObject();
        }
        else if (buildMenu.activeSelf)
        {
            currentMode = GameState.Play;
            buildMenu.SetActive(false);
        }
        else if (currentMode == GameState.WallBuy || currentMode == GameState.Build || currentMode == GameState.Trash)
        {
            buildMenu.SetActive(true);
            currentMode = GameState.Pause;
            currentMode = GameState.Play;
            Cursor.visible = true;
        }
    }

    public void SetBuyWallMode()
    {
        this.currentMode = GameState.WallBuy;
        buildMenu.SetActive(false);
        Cursor.visible = true;
    }
    public void SetBuildObject(MachineBase obj, int cost)
    {
        buildCost = cost;
        machineToPlace = obj;
        curserObject.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<SpriteRenderer>().sprite;
        currentMode = GameState.Build;
        building = true;
    }

    public void ClearBuildObject()
    {
        buildCost = 0;
        machineToPlace = null;
        curserObject.GetComponent<SpriteRenderer>().sprite = null;
        currentMode = GameState.Pause;
        building = false;
    }
    private void UpdateMachineStates()
    {
        foreach (InputOutputMachine machine in machines)
        {
            machine.StageUpdate();
        }
        foreach (InputOutputMachine machine in machines)
        {
            machine.StageUpdateFinished();
        }
    }

    /// <summary>
    /// Returns true if the player has enough money to expand
    /// </summary>
    /// <returns></returns>
    public bool CanBuyWall()
    {
        return wallValue <= currentMoney;
    }

    /// <summary>
    /// Apply changes to money and wall values
    /// </summary>
    public void BuyWall()
    {
        buyNumber++;
        currentMoney -= wallValue;
        wallValue = Mathf.FloorToInt(Mathf.Pow((float)buyNumber, wallValueFactorRange));
    }

    public void AddMachine(InputOutputMachine machine)
    {
        machines.Add(machine);
    }

    public void RemoveMachine(InputOutputMachine machine)
    {
        machines.Remove(machine);
    }
}
