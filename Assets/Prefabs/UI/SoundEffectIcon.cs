using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoundEffectIcon : MonoBehaviour
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
        if (gm.soundEffectsOn)
        {
            image.GetComponent<Image>().sprite = On;
        }
        else
        {
            image.GetComponent<Image>().sprite = Off;
        }
    }
}
