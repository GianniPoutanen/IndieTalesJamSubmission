using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUnlockChecker : MonoBehaviour
{
    private GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gm.currentMode == GameManager.GameState.Play)
            this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        foreach (Transform child in this.transform)
        {
            BuildOptionButton button = child.GetComponent<BuildOptionButton>();
            if (button != null)
            {
                if (gm.GetComponent<UnlockList>().IsUnlocked(button.unlock) || button.unlock == UnlockList.Unlocks.none)
                {
                    button.gameObject.SetActive(true);
                }
                else
                {
                    button.gameObject.SetActive(false);
                }
            }
        }
    }
}
