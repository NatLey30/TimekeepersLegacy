using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager_RodeoBull : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bull;

    [SerializeField] private Canvas canvasStart;
    [SerializeField] private Canvas canvasFin;
    [SerializeField] private TextMeshProUGUI gameEnd;

    // Start is called before the first frame update
    void Start()
    {
        canvasFin.enabled = false;
        player.SetActive(false);
        bull.SetActive(false);

        // Subscribe to the CarController's collision event
        PlayerController_RodeoBull.OnCollision += HandleCollision;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        canvasStart.enabled = false;
        player.SetActive(true);
        bull.SetActive(true);
    }

    // Handle the arrow collision event
    private void HandleCollision()
    {
        EndGame("GameOver");
    }

    // Unsubscribe from the event when this script is disabled or destroyed
    private void OnDisable()
    {
        PlayerController_RodeoBull.OnCollision -= HandleCollision;
    }

    private void EndGame(string end)
    {
        gameEnd.text = end;

        canvasFin.enabled = true;
        player.SetActive(false);
        bull.SetActive(false);
    }
}
