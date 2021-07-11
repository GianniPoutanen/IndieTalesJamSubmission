using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnAnimationEnd : MonoBehaviour
{
    public void DestroyMe()
    {
        GameObject.Destroy(this.gameObject);
    }
}
