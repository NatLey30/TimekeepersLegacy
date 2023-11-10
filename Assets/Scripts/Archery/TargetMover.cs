using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Speed of target movement

    private float leftBoundary = -1f; // Left boundary
    private float rightBoundary = 6f; // Right boundary
    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTarget();
    }

    void MoveTarget()
    {
        // Determine the direction of movement
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;

        // Move the target
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the target reaches the boundaries.
        if (movingRight && transform.position.x >= rightBoundary)
        {
            // Change direction to left.
            movingRight = false;
        }
        else if (!movingRight && transform.position.x <= leftBoundary)
        {
            // Change direction to right.
            movingRight = true;
        }
    }
}
