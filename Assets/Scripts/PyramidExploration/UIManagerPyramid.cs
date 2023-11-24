using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManagerPyramid : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject timer;

    [SerializeField] private Canvas canvasStart;
    [SerializeField] private Canvas canvasFin;
    [SerializeField] private Canvas canvasTimer;
    [SerializeField] private TextMeshProUGUI gameEnd;


    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get GameManager
        gameManager = FindObjectOfType<GameManager>();

        canvasFin.enabled = false;
        canvasTimer.enabled = false;
        player.SetActive(false);
        timer.SetActive(false);

        // Subscribe to the CarController's collision event
        PlayerControllerPyramid.OnSarcophagusCollision += HandleCollision;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        canvasStart.enabled = false;
        canvasTimer.enabled = true;
        player.SetActive(true);
        timer.SetActive(true);
    }

    // Handle the arrow collision event
    private void HandleCollision()
    {
        EndGame("You Win", true);
    }

    // Unsubscribe from the event when this script is disabled or destroyed
    private void OnDisable()
    {
        PlayerControllerPyramid.OnSarcophagusCollision -= HandleCollision;
    }

    public void EndGame(string end, bool win)
    {
        gameEnd.text = end;

        canvasFin.enabled = true;
        canvasTimer.enabled = false;
        player.SetActive(false);
        timer.SetActive(false);
    }

    public void ReturnToRunningScene()
    {
        gameManager.ReturnToRunningScene();
    }
}
