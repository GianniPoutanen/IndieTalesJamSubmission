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
        WallBuy,
        Pause
    }
    public GameState currentMode;

    private void Start()
    {
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

        curserObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleMenuEscape();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (buildMenu.activeSelf)
            {
                buildMenu.SetActive(false);
                currentMode = GameState.Play;
            }
            else
            {
                buildMenu.SetActive(true);
                currentMode = GameState.Build;
            }
        }

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
        }
        else if (buildMenu.activeSelf)
        {
            buildMenu.SetActive(false);
        }
        else if (currentMode == GameState.Build)
        {
            currentMode = GameState.Play;
        }
        else if (currentMode == GameState.WallBuy)
        {
            buildMenu.SetActive(true);
            currentMode = GameState.Build;
        }
    }

    public void SetBuyWallMode()
    {
        this.currentMode = GameState.WallBuy;
        buildMenu.SetActive(false);
    }
    public void SetBuildObject(MachineBase obj, int cost)
    {
        buildCost = cost;
        machineToPlace = obj;
        curserObject.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<SpriteRenderer>().sprite;
        building = true;
    }

    public void ClearBuildObject()
    {
        buildCost = 0;
        machineToPlace = null;
        curserObject.GetComponent<SpriteRenderer>().sprite = null;
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
}
