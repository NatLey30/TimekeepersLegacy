using DG.Tweening.Core.Easing;
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
    private string[] wordsAvailable = { "PLEASE", "SMALL", "SALE", "SALES", "LESS", 
        "SELL", "ELSE", "PLUS", "SAMPLE", "MALE", "APPLE", "PALM", "SAMPLES", "SLEEP", "APPEAL", 
        "PLASMA", "PULL", "PULSE", "LEASE", "USUAL", "SEAL", "MALL", "LAMP", "MEAL", "APPEALS", 
        "MEALS", "LAMPS", "SALEM", "MAPLE", "SELLS", "MALES", "SPELL", "PAMELA", "SMELL", "PALE", 
        "SLEEPS", "SEALS", "USELESS", "LEAP", "PAULA", "LEMMA", "MALLS", "PALAU", "SALSA", "ALLE", 
        "APPLES", "ASLEEP", "SPELLS", "PULP", "SLAM", "PULLS", "PEEL", "MAMMALS", "SEAMLESS", "PLEA", 
        "LEASES", "PLUMP", "ALMA", "AMPLE", "PALMS", "LAME", "PLUM", "LUMP", "PALS", "ALPS", "ALAS", 
        "SMELLS", "ELEM", "PSALMS", "ELLE", "PSALM", "MULE", "SLAP", "EMULE", "PULSES", "LAPS", "LAMA", 
        "APPLAUSE", "LUPUS", "LAPSE", "SALLE", "LESSEE", "ALSA", "PLUME", "MAMMAL", "ALLELE", "ALLES", 
        "MEASLES", "PLEAS", "LEES", "ALUM", "LAPEL", "SELMA", "LEAPS", "MELEE", "LLAMA", "ALLELES", 
        "SLAMS", "ALPES", "MULES", "PELL", "SLUMP", "PAULS", "APPELLEE", "MUSSELS", "PLASMAS", "SUPPLE",
        "SALA", "EASEL", "PAPAL", "EELS", "MULL", "SLEEPLESS", "LUMPS", "SLUM", "PALL", "LUAU", "PLEASE",
        "PLUMS", "MUSSEL", "MAPLES", "SLUMS", "MAUL", "PEELS", "PLUMES", "EASELS", "ELMS", "LULL", "LAPSES",
        "LLAMAS", "LEMME", "SLAPS", "ALUMS", "PLUSES", "ELAPSE" };

    [SerializeField] private Canvas canvasStart;
    [SerializeField] private Canvas canvasFin;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI gameEnd;

    private GameManager gameManager;
    private bool win;


    // Start is called before the first frame update
    void Start()
    {
        // Get GameManager
        gameManager = FindObjectOfType<GameManager>();

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
            win  = true;
        }
        else if (numberTries == 0)
        {
            EndGame("Game Over");
            win = false;
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

    public void ReturnToRunningScene()
    {
        gameManager.ReturnToRunningScene(win);
    }
}
