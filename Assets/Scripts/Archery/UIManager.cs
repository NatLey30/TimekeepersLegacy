using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject target;

    [SerializeField] private Canvas canvasStart;
    [SerializeField] private Canvas canvasFin;
    [SerializeField] private TextMeshProUGUI gameEnd;

    [SerializeField] private BowController bowController;

    private GameManager gameManager;

    private int numberArrows;
    private bool win;

    // Start is called before the first frame update
    void Start()
    {
        // Get GameManager
        gameManager = FindObjectOfType<GameManager>();

        canvasFin.enabled = false;
        bow.SetActive(false);
        target.SetActive(false);

        // Subscribe to the ArrowController's collision event
        ArrowController.OnArrowCollision += HandleArrowCollision;
    }

    // Update is called once per frame
    void Update()
    {
        numberArrows = bowController.NumberArrows;
    }

    public void StartGame()
    {
        canvasStart.enabled = false;
        bow.SetActive(true);
        target.SetActive(true);
    }

    // Handle the arrow collision event
    private void HandleArrowCollision(bool hit)
    {
        if (hit)
        {
            // Handle arrow hit.
            EndGame("You Win");
            win = true;
        }
        else
        {
            // Handle arrow miss.
            if (numberArrows == 0)
            {
                EndGame("GameOver");
                win = false;
            }
        }
    }

    // Unsubscribe from the event when this script is disabled or destroyed
    private void OnDisable()
    {
        ArrowController.OnArrowCollision -= HandleArrowCollision;
    }

    private void EndGame(string end)
    {
        gameEnd.text = end;

        canvasFin.enabled = true;
        bow.SetActive(false);
        target.SetActive(false);
    }

    public void ReturnToRunningScene()
    {
        gameManager.ReturnToRunningScene(win);
    }
}
