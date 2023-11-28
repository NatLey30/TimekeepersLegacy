using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller_buttons : MonoBehaviour
{
    [SerializeField] private RawImage objectEgypt;
    [SerializeField] private RawImage objectMiddleAges;
    [SerializeField] private RawImage objectPresent;
    [SerializeField] private RawImage objectPrehistory;
    [SerializeField] private RawImage objectJapan;

    [SerializeField] private Canvas pause;

    private GameManager gameManager;
    private GameObject player;
    private GameObject enemy;

    private bool objectEgyptDone = false;
    private bool objectMiddleAgesDone = false;
    private bool objectPresentDone = false;
    private bool objectPrehistoryDone = false;
    private bool objectJapanDone = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get GameManager
        gameManager = FindObjectOfType<GameManager>();

        // Get Player
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        pause.enabled = false;

        ChangeTransparency(objectEgypt, 0.2f);
        ChangeTransparency(objectMiddleAges, 0.2f);
        ChangeTransparency(objectPresent, 0.2f);
        ChangeTransparency(objectPrehistory, 0.2f);
        ChangeTransparency(objectJapan, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        objectEgyptDone = gameManager.ObjectEgyptDone;
        objectMiddleAgesDone = gameManager.ObjectMiddleAgesDone;
        objectPresentDone = gameManager.ObjectPresentDone;
        objectPrehistoryDone= gameManager.ObjectPrehistoryDone;
        objectJapanDone= gameManager.ObjectJapanDone;

        if (objectEgyptDone)
        {
            ChangeTransparency(objectEgypt, 1f);
        }

        if (objectMiddleAgesDone)
        {
            ChangeTransparency(objectMiddleAges, 1f);
        }

        if (objectPresentDone)
        {
            ChangeTransparency(objectPresent, 1f);
        }

        if (objectPrehistoryDone)
        {
            ChangeTransparency(objectPrehistory, 1f);
        }

        if (objectJapanDone)
        {
            ChangeTransparency(objectJapan, 1f);
        }
    }

    private void ChangeTransparency(RawImage obj, float alpha)
    {
        // Get the current color
        Color currentColor = obj.color;

        // Set the new alpha value
        currentColor.a = alpha;

        // Update the image color with the new alpha
        obj.color = currentColor;
    }

    public void GoToMenu()
    {
        gameManager.EndGame();
    }
    public void PauseGame()
    {
        pause.enabled = true;
        player.SetActive(false);
        enemy.SetActive(false);
    }

    public void ContinueGame()
    {
        pause.enabled = false;
        player.SetActive(true);
        enemy.SetActive(true);
    }
}
