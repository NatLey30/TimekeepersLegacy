using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Variables assigned in unity
    // [SerializeField] private Transform cameraFollow;

    private float speed = 8f;
    private float jumpingPower = 15f;
    private bool isFacingRight = true;
    private bool canJump = true;

    private Rigidbody2D rigidBody;
    private GameManager gameManager;

    private Camera camera;
    Transform cameraFollow;

    // Start is called before the first frame update
    void Start()
    {
        // Get Rigid Body from the object assigned to the script
        rigidBody = GetComponent<Rigidbody2D>();

        // Get GameManager
        gameManager = FindObjectOfType<GameManager>();

        // Get Camera
        camera = Camera.main;
        cameraFollow = camera.transform;

        // Get Material
        GetComponent<Renderer>().material = gameManager.Material;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump") && canJump)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower);
            canJump = false;
        }
    }

    // Fixed Update is executed at a specific rate defined by the editor
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(horizontal * speed, rigidBody.velocity.y, 0);
        rigidBody.velocity = movement;

        // Call flip method
        Flip();

        // Update camera position to follow the player
        if (cameraFollow != null)
        {
            cameraFollow.position = new Vector3(transform.position.x, transform.position.y+2f, cameraFollow.position.z);
        }

    }

    private void Flip()
    {
        // Changes direction (left/right)
        if ((isFacingRight && rigidBody.velocity.x < 0f) || (!isFacingRight && rigidBody.velocity.x > 0f)) 
        {
            isFacingRight = !isFacingRight;
            Vector3 localscale = transform.localScale;
            localscale.x *= -1f;
            transform.localScale = localscale;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Only when colliding with objects with Ground tag, can the player jump
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }

        if (collision.gameObject.CompareTag("Limit"))
        {
            gameManager.TransitionToRandomScene();
        }
    }

}
