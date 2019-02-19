using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class NumberManager : MonoBehaviour
{
    protected List<GameObject> buttonObjects = new List<GameObject>();
    protected readonly int buttonCount = 25;
    protected GameObject buttonPrefab;
    protected TextMeshProUGUI buttonText;
    protected Sprite[] sprites;

    protected virtual void Awake()
    {
        LoadAssets();
        GenerateButtons();
    }

    protected abstract void CheckNumber(int index);
    protected abstract void OnGameOver();

    protected void LoadAssets()
    {
        buttonPrefab = Resources.Load<GameObject>("Prefabs/ButtonHolder");
        sprites = Resources.LoadAll<Sprite>("Buttons/");
    }

    void GenerateButtons()
    {
        for (int i = 0; i < buttonCount; i++)
        {
            GameObject obj = Instantiate(buttonPrefab, transform);
            obj.name = i.ToString();
            buttonObjects.Add(obj);
            int index = i;
            buttonObjects[index].GetComponentInChildren<Button>().onClick.AddListener(() => CheckNumber(index));

            buttonText = buttonObjects[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = Random.Range(1, 10).ToString();
            buttonObjects[i].GetComponentInChildren<Image>().sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }

    protected string GetRandomNumber(int min = 1, int max = 10) => Random.Range(min, max).ToString();

    protected void GenerateNumbers(int min = 1, int max = 10)
    {
        for (int i = 0; i < buttonCount; i++)
        {
            buttonText = buttonObjects[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = Random.Range(min, max).ToString();
            buttonObjects[i].GetComponentInChildren<Image>().sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }

    protected void GenerateNumbers(System.Func<int> calculator)
    {
        for (int i = 0; i < buttonCount; i++)
        {
            buttonText = buttonObjects[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = calculator.ToString();
            buttonObjects[i].GetComponentInChildren<Image>().sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }
}
