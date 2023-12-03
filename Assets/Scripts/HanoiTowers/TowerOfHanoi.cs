using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using TMPro;

public class TowerOfHanoi : MonoBehaviour
{
    [SerializeField] private GameObject timer;

    [SerializeField] private Canvas canvasStart;
    [SerializeField] private Canvas canvasFin;
    [SerializeField] private Canvas canvasTimer;
    [SerializeField] private TextMeshProUGUI gameEnd;

    // Prefab for the disk
    [SerializeField] private GameObject diskPrefab;

    // List of towers
    [SerializeField] private List<Transform> towers;

    // List to represent the disks on each tower
    private List<List<GameObject>> towerDisks;

    // Selected tower index
    private int selectedTowerIndex = -1;
    private GameManager gameManager;
    private bool win;

    void Start()
    {
        canvasFin.enabled = false;
        canvasTimer.enabled = false;
        timer.SetActive(false);

        // Get GameManager
        gameManager = FindObjectOfType<GameManager>();

        towerDisks = new List<List<GameObject>>();
        for (int i = 0; i < towers.Count; i++)
        {
            towerDisks.Add(new List<GameObject>());
        }

        FindDisksInScene();

    }

    void FindDisksInScene()
    {
        // Iterate through each tower
        for (int i = 0; i < towers.Count; i++)
        {
            // Clear the list for safety
            towerDisks[i].Clear();

            // Iterate through child objects of the tower
            foreach (Transform child in towers[i])
            {
                // Check if the child has a collider (disk)
                Collider2D collider = child.GetComponent<Collider2D>();
                if (collider != null)
                {
                    // Add the disk to the towerDisks list
                    towerDisks[i].Add(child.gameObject);
                }
            }
        }
    }

    void Update()
    {
        // Check for input (click) and handle tower selection
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null)

            {
                Transform clickedTower = hit.collider.transform;


                // If no tower is selected, select the clicked tower
                if (selectedTowerIndex == -1)
                {
                    selectedTowerIndex = towers.IndexOf(clickedTower);
                }
                else
                {
                    // If a tower is already selected, move the top disk to the clicked tower
                    MoveDisk(selectedTowerIndex, towers.IndexOf(clickedTower));
                    selectedTowerIndex = -1;
                }
            }
        }
    }

    public void StartGame()
    {
        canvasStart.enabled = false;
        canvasTimer.enabled = true;
        timer.SetActive(true);
    }

    void MoveDisk(int fromTower, int toTower)
    {
        if (fromTower == toTower || towerDisks[fromTower].Count == 0)
        {
            // Invalid move
            selectedTowerIndex = -1;
            return;
        }

        int diskIndex = towerDisks[fromTower].Count - 1;
        GameObject diskToMove = towerDisks[fromTower][diskIndex];

        if (towerDisks[toTower].Count == 0 || diskToMove.name == "TopDisc" || (diskToMove.name == "MiddleDisc" && towerDisks[toTower][towerDisks[toTower].Count - 1].name == "BottomDisc"))
        {
            // Move the disk
            towerDisks[fromTower].RemoveAt(diskIndex);
            towerDisks[toTower].Add(diskToMove);

            // Animate the movement
            StartCoroutine(MoveDiskAnimation(diskToMove.transform, towers[toTower].position + Vector3.up * towerDisks[toTower].Count * 0.4f));

            // Set the disk's parent to the new tower
            diskToMove.transform.SetParent(towers[toTower]);
        }

        if (towerDisks[2].Count == 3)
        {
            EndGame("You Win");
            win = true;
        }
    }

    IEnumerator MoveDiskAnimation(Transform diskTransform, Vector3 targetPosition)
    {
        float duration = 1.0f;
        float elapsedTime = 0f;
        Vector3 startPosition = diskTransform.position;

        while (elapsedTime < duration)
        {
            diskTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the disk is precisely at the target position
        diskTransform.position = targetPosition;
    }

    public void EndGame(string end)
    {
        gameEnd.text = end;

        canvasFin.enabled = true;
        canvasTimer.enabled = false;
        timer.SetActive(false);
    }

    public void ReturnToRunningScene()
    {
        gameManager.ReturnToRunningScene(win);
    }
}


    
