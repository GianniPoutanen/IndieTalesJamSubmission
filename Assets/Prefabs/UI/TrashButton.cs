using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashButton : MonoBehaviour
{
    public void OnClick()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().currentMode = GameManager.GameState.Trash;
        this.transform.parent.gameObject.SetActive(false);
    }
}
