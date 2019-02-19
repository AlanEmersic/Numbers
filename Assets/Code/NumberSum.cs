using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberSum : NumberManager
{
    [SerializeField] TextMeshProUGUI sumText;
   
    int sum;
    int stageCounter = 1;
    int numbersClicked;
    Dictionary<GameObject, string> buttonsSolution;
    ScoreManager scoreManager;

    public event System.Action GameOverEvent = delegate { };

    protected override void Awake()
    {
        base.Awake();        
        GameOverEvent += OnGameOver;
        buttonsSolution = new Dictionary<GameObject, string>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Start()
    {
        CalculateSum();
    }

    void CalculateSum()
    {
        int numberCount = Random.Range(buttonCount / 5, buttonCount - 5);

        for (int i = 0; i < numberCount; i++)
        {
            int index = Random.Range(0, buttonCount);
            string number = buttonObjects[index].GetComponentInChildren<TextMeshProUGUI>().text;
            if (buttonsSolution.ContainsKey(buttonObjects[index]))
            {
                i--;
                continue;
            }
            buttonsSolution.Add(buttonObjects[index], number);
            sum += int.Parse(number);
        }
        sumText.text = string.Format($"Sum: {sum}");
    }

    protected override void CheckNumber(int index)
    {
        numbersClicked++;
        sum -= int.Parse(buttonObjects[index].GetComponentInChildren<TextMeshProUGUI>().text);
        sumText.text = string.Format($"Sum: {sum}");
        buttonObjects[index].GetComponentInChildren<Button>().interactable = false;
        Color colour = buttonObjects[index].GetComponentInChildren<Image>().color;
        colour.a = 0.5f;
        buttonObjects[index].GetComponentInChildren<Image>().color = colour;

        if (sum == 0)
            OnSumCompleted();
        else if (sum < 0)
            OnGameOver();
    }

    void OnSumCompleted()
    {
        scoreManager.AddScore(numbersClicked * stageCounter);
        stageCounter++;
        sum = 0;
        numbersClicked = 0;
        buttonsSolution.Clear();
        foreach (var button in buttonObjects)
        {
            button.GetComponentInChildren<Image>().color = Color.white;
            button.GetComponentInChildren<Button>().interactable = true;
        }

        GenerateNumbers();
        CalculateSum();
    }

    protected override void OnGameOver()
    {
        GameOverEvent -= OnGameOver;
    }
}
