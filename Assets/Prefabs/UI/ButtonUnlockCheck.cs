using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUnlockCheck : MonoBehaviour
{
    private GameManager gm;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        this.gameObject.SetActive(gm.currentMode != GameManager.GameState.Play);
    }

    private void OnEnable()
    {
        foreach(Transform child in this.transform)
        {
            BuildOptionButton button = child.GetComponent<BuildOptionButton>();
            if (button != null)
            {
                button.gameObject.SetActive(gm.GetComponent<UnlockList>().IsUnlocked(button.unlock));
            }
        }
    }
}
