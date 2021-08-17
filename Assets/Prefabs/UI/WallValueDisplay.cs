using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallValueDisplay : MonoBehaviour
{
    public int value = -1;
    private string valueText;
    public int imageNumGap;
    public int yPos;
    public int xPos;
    private GameManager gm;
    public Sprite[] numbers;

    private Queue<GameObject> cullNumbers;

    public GameObject numberImage;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        cullNumbers = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (value != gm.currentMoney)
        {
            value = gm.wallValue;
            valueText = value.ToString();
            UpdateImageValueAndPosition();
        }
    }

    private void UpdateImageValueAndPosition()
    {
        while (cullNumbers.Count > 0)
        {
            GameObject.Destroy(cullNumbers.Dequeue());
        }

        for (int i = 0; i < valueText.Length; i++)
        {
            GameObject number = Instantiate(numberImage);
            int spriteNumber = valueText[(valueText.Length - 1) - i] - '0';
            number.transform.SetParent(this.transform);
            cullNumbers.Enqueue(number);
            number.GetComponent<Image>().sprite = numbers[spriteNumber];
            number.GetComponent<RectTransform>().anchoredPosition = new Vector2(-imageNumGap * (i + 1) + xPos, yPos);
        }
    }
}
