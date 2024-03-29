using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UIManagerCrossRoad : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawner;

    [SerializeField] private Canvas canvasStart;
    [SerializeField] private Canvas canvasFin;
    [SerializeField] private TextMeshProUGUI gameEnd;

    private GameManager gameManager;
    private bool win;

    // Start is called before the first frame update
    void Start()
    {
        // Get GameManager
        gameManager = FindObjectOfType<GameManager>();

        canvasFin.enabled = false;
        player.SetActive(false);
        spawner.SetActive(false);

        // Subscribe to the CarController's collision event
        PlayerController.OnCollision += HandleCarCollision;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        canvasStart.enabled = false;
        player.SetActive(true);
        spawner.SetActive(true);
    }

    // Handle the arrow collision event
    private void HandleCarCollision(bool car)
    {
        if (car)
        {
            EndGame("GameOver");
            win = false;
        }
        else
        {
            EndGame("You Win");
            win = true;
        }
    }

    // Unsubscribe from the event when this script is disabled or destroyed
    private void OnDisable()
    {
        PlayerController.OnCollision -= HandleCarCollision;
    }

    private void EndGame(string end)
    {

        gameEnd.text = end;

        canvasFin.enabled = true;
        player.SetActive(false);
        spawner.SetActive(false);
    }
    public void ReturnToRunningScene()
    {
        gameManager.ReturnToRunningScene(win);
    }
}
