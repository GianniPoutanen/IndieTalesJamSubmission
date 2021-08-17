using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurserMovement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = new Vector3(0, 0, -10);
    }
}
