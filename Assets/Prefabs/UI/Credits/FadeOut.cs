using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public float speed = 0.001f;
    public bool fadeOut = false;
    public Image image;
    // Update is called once per frame
    void Update()
    {
        if (fadeOut)
        {
            if ((image.color.a - ((speed * (1f / image.color.a) * Time.deltaTime))) > 0 )
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - ((speed * (1f / image.color.a) * Time.deltaTime)));
            }
            else
            {
                Destroy(this.gameObject);
            }

        }
    }
}
