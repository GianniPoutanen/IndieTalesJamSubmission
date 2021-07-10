using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerePositioning : MonoBehaviour
{
    public int zOffset;
    public GameObject player;
    public float speed;
    public float speedFactor;

    // Update is called once per frame
    void Update()
    {
        float smoothStep = speed + (Vector2.Distance(player.transform.position, this.transform.position) * speedFactor);
        this.transform.position = (Vector3)Vector2.MoveTowards(this.transform.position, player.transform.position, smoothStep) + new Vector3(0,0,zOffset);
    }
}
