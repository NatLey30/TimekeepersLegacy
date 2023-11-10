using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] letterButtons;
    [SerializeField] private Button[] buttons;

    [SerializeField] private TextMeshProUGUI writting;
    [SerializeField] private TextMeshProUGUI wordsFound;
    [SerializeField] private TextMeshProUGUI tries;

    private List<string> words = new List<string>();
    private int numberTries = 5;

    private string[] letters = { "A", "P", "E", "S", "U", "M", "L"};
    private string[] wordsAvailable = { "LAMP", "SLUMP", "APPLE"};

    [SerializeField] private Canvas canvasStart;
    [SerializeField] private Canvas canvasFin;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI gameEnd;


    // Start is called before the first frame update
    void Start()
    {
        canvasFin.enabled = false;
        canvas.enabled = false;

        for (int i = 0; i < letterButtons.Length; i++)
        {
            letterButtons[i].text = letters[i];
        }

        wordsFound.text = "";
        tries.text = "TRIES LEFT: " + numberTries.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (words.Count == 3)
        {
            EndGame("You Win");
        }
        else if (numberTries == 0)
        {
            EndGame("Game Over");
        }
    }
    public void StartGame()
    {
        canvasStart.enabled = false;
        canvas.enabled = true;
    }

    public void WriteLetter(int buttonIndex)
    {
        int index = buttonIndex - 1;
        string letter = letterButtons[index].text;
        writting.text += letter;
    }
    public void Delete()
    {
        writting.text = "";
    }

    public void Submit()
    {
        numberTries -= 1;
        tries.text = "TRIES LEFT: " + numberTries.ToString();

        string word = writting.text;

        if (wordsAvailable.Contains(word) && !words.Contains(word))
        {
            words.Add(word);
            wordsFound.text += word + " ";
        }

        writting.text = "";
    }

    private void EndGame(string end)
    {
        gameEnd.text = end;

        canvas.enabled = false;
        canvasFin.enabled = true;
    }
}
