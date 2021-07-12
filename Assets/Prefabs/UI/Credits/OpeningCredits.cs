using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningCredits : MonoBehaviour
{
    public Text messageText;
    private int charIndex;

    private float timer;
    public float timerPerChar;

    private int stringIndex = 0;

    [SerializeField]
    [TextArea(4, 20)]
    public string[] strings;
    public float[] stringTimes;

    public bool writing = true;

    private void Start()
    {
        messageText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!(this.transform.parent.gameObject.GetComponent<FadeOut>().fadeOut == true))
        {
            if (writing)
            {
                HandleMessageScroll();
            }
            else
            {
                if (timer <= 0)
                {
                    if (strings.Length == stringIndex + 1)
                    {
                        this.transform.parent.gameObject.GetComponent<FadeOut>().fadeOut = true;
                        if (GameObject.Find("GameManager").GetComponent<GameManager>().currentMode == GameManager.GameState.Credits)
                            GameObject.Find("GameManager").GetComponent<GameManager>().currentMode = GameManager.GameState.Play;
                    }
                    else
                    {
                        stringIndex++;
                        messageText.text = "";
                        writing = true;
                    }
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }
        }
        else
        {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().currentMode == GameManager.GameState.Credits)
                GameObject.Find("GameManager").GetComponent<GameManager>().currentMode = GameManager.GameState.Play;
        }
    }

    private void HandleMessageScroll()
    {
        if (messageText != null)
        {
            timer -= Time.deltaTime;
            if (messageText.text != strings[stringIndex])
            {
                if (timerPerChar > 0)
                {
                    if (timer <= 0f)
                    {
                        timer = timerPerChar;
                        charIndex++;
                        if (strings[stringIndex][messageText.text.Length] == ' ')
                            messageText.text += strings[stringIndex][messageText.text.Length];
                        messageText.text += strings[stringIndex][messageText.text.Length];
                    }
                }
                else
                {
                    messageText.text = strings[stringIndex];
                }
            }
            else
            {
                writing = false;
                timer = stringTimes[stringIndex];
            }
        }
    }
}
