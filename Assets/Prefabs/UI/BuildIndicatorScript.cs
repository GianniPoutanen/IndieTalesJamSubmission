using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildIndicatorScript : MonoBehaviour
{
    private GameManager gm;
    public GameObject indicator;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.currentMode == GameManager.GameState.Play)
        {
            indicator.SetActive(true);
        }
        else
        {
            indicator.SetActive(false);
        }
    }
}
