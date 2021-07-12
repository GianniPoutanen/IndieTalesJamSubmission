using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnAnimationEnd : MonoBehaviour
{
    public void DestroyMe()
    {
        this.gameObject.SetActive(false);
        GameObject.Destroy(this.gameObject);
    }
}
