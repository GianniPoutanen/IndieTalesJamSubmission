using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicalNoteIcon : MonoBehaviour
{
    public GameObject image;

    public Sprite On;
    public Sprite Off;

    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.musicOn)
        {
            image.GetComponent<Image>().sprite = On;
        }
        else
        {
            image.GetComponent<Image>().sprite = Off;
        }
    }
}
