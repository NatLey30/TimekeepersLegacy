using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManagerPyramid : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private Canvas canvasStart;
    [SerializeField] private Canvas canvasFin;
    [SerializeField] private TextMeshProUGUI gameEnd;


    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get GameManager
        gameManager = FindObjectOfType<GameManager>();

        canvasFin.enabled = false;
        player.SetActive(false);

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
        player.SetActive(true);
    }

    // Handle the arrow collision event
    private void HandleCollision()
    {
        EndGame("You Win");
    }

    // Unsubscribe from the event when this script is disabled or destroyed
    private void OnDisable()
    {
        PlayerControllerPyramid.OnSarcophagusCollision -= HandleCollision;
    }

    private void EndGame(string end)
    {
        gameEnd.text = end;

        canvasFin.enabled = true;
        player.SetActive(false);
    }

    public void ReturnToRunningScene()
    {
        gameManager.ReturnToRunningScene();
    }
}
