using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private Material originalMaterial;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Store the original material of the player.
        originalMaterial = GetComponent<Renderer>().material;

        // Get GameManager
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown ()
    {
        enemy.GetComponent<Renderer>().material = originalMaterial;
        gameManager.Material = originalMaterial;
    }
}
