using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Speed of target movement
    [SerializeField] private Texture2D deer1;
    [SerializeField] private Texture2D deer2;


    private float leftBoundary = 0f; // Left boundary
    private float rightBoundary = 6f; // Right boundary
    private bool movingRight = false;
    private Vector3 direction = Vector3.right;

    private bool deer = true;

    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, 180, 0);
        renderer = GetComponent<Renderer>();
        ChangeTexture();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTarget();
        ChangeTexture();

    }

    void MoveTarget()
    {
        // direction = movingRight ? Vector3.right : Vector3.left;

        // Move the target
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the target reaches the boundaries.
        if (transform.position.x >= rightBoundary && movingRight)
        {
            // Change direction to left
            // With the rotation is enough
            movingRight = false;
            transform.Rotate(0, 180, 0);
        }
        else if (transform.position.x <= leftBoundary && !movingRight)
        {
            // Change direction to right.
            // With the rotation is enough
            movingRight = true;
            transform.Rotate(0, -180, 0);
        }
    }
 
    private void ChangeTexture()
    {
        Material material = renderer.material;

        material.mainTexture = deer ? deer2 : deer1;

        // Toggle 'deer' variable
        deer = !deer;
    }
}
