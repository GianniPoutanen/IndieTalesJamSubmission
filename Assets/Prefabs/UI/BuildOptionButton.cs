using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildOptionButton : MonoBehaviour
{
    // Cost and Machine
    public MachineBase machine;
    public UnlockList.Unlocks unlock;

    public Sprite[] numbers;
    private string valueText;
    private Queue<GameObject> cullNumbers = new Queue<GameObject>();
    public GameObject numberImage;

    public int imageNumGap;
    public int yPos;

    public void OnClick()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().CanBuildMachine(machine.cost))
            GameObject.Find("GameManager").GetComponent<GameManager>().SetBuildObject(machine, machine.cost);
        this.transform.parent.gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        valueText = machine.cost.ToString();
        UpdateImageValueAndPosition();
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
            number.GetComponent<RectTransform>().anchoredPosition = new Vector2(imageNumGap * (i + 1), yPos);
        }
    }
}
