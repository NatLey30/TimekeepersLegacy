using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementRight : MonoBehaviour
{
    private float moveSpeed = 5f;
    private Vector3 movementDirection;
    private float screenEdgeRight = 9f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveCar();

        if (transform.position.x > screenEdgeRight)
        {
            // Delete the car when it goes off-screen
            Destroy(gameObject);
        }
    }

    private void MoveCar()
    {
        movementDirection = Vector3.right;
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
    }
}
