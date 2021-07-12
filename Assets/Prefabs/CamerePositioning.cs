using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerePositioning : MonoBehaviour
{
    public int zOffset;
    public GameObject followObject;
    public float speed;
    public float speedFactor;

    public bool goTo;

    public Vector2 followOffset;
    private Vector2 threshold;

    private void Start()
    {
        threshold = CalculateThreshold();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        threshold = CalculateThreshold();
        if (goTo)
        {
            float smoothStep = speed + (Vector2.Distance(followObject.transform.position, this.transform.position) * speedFactor);
            this.transform.position = (Vector3)Vector2.MoveTowards(this.transform.position, followObject.transform.position, smoothStep) + new Vector3(0, 0, zOffset);
        }
        else
        {
            Vector2 follow = followObject.transform.position;
            float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
            float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

            Vector3 newPos = this.transform.position;
            if (Mathf.Abs(xDifference) >= threshold.x)
            {
                newPos = new Vector3(follow.x, newPos.y, newPos.z);
            }
            if (Mathf.Abs(yDifference) >= threshold.y)
            {
                newPos = new Vector3(newPos.x, follow.y, newPos.z);
            }

            float smoothStep = speed + (Vector2.Distance(followObject.transform.position, this.transform.position) * speedFactor);
            this.transform.position = (Vector3)Vector2.MoveTowards(this.transform.position, newPos, smoothStep) + new Vector3(0, 0, zOffset);

        }
    }

    private Vector3 CalculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t = new Vector2(t.x - followOffset.x, t.y - followOffset.y);
        return t;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 border = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}

