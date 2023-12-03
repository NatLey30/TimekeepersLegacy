using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CotrollerMenu : MonoBehaviour
{
    [SerializeField] private Canvas instructions;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get GameManager
        gameManager = FindObjectOfType<GameManager>();
        instructions.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        // Start the game on the same scene every time
        gameManager.StartGame();
    }

    public void ShowInstructions()
    {
        instructions.enabled = true;
    }

    public void GoBack() 
    {
        instructions.enabled = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
