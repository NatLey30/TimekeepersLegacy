using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 5f;

    private Transform player;

    private Camera mainCamera;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = Camera.main;

        // Get GameManager
        gameManager = FindObjectOfType<GameManager>();

        GetComponent<Renderer>().material = gameManager.Material;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the player's position
        Vector3 playerPosition = player.position;

        // Check if the enemy is falling behind
        // It has to be in sight of the camera
        if (transform.position.x+14f < playerPosition.x)
        {
            // Accelerate to catch up
            speed += 0.25f;
        }
        else
        {
            // Reset speed to its base value
            speed = 9f;
        }

        // Move towards the player
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, playerPosition.y, transform.position.z);


    }
}
