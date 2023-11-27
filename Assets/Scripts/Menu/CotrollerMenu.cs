using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CotrollerMenu : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get GameManager
        gameManager = FindObjectOfType<GameManager>();
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
}
