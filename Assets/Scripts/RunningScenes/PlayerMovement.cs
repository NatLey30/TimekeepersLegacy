using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private List<Sprite> animation;
    [SerializeField] private Sprite standingSprite;

    private float speed = 10f;
    private float jumpingPower = 17f;
    private bool isFacingRight = true;
    private bool canJump = true;

    private Rigidbody2D rigidBody;
    private SpriteRenderer renderer;

    private GameManager gameManager;

    private Camera camera;
    private Transform cameraFollow;

    private int currentFrame = 0;
    private float timer = 0f;
    public float frameRate = 12f;

    // Start is called before the first frame update
    void Start()
    {
        // Get Rigid Body from the object assigned to the script
        rigidBody = GetComponent<Rigidbody2D>();

        // Get SpriteRenderer
        renderer = GetComponent<SpriteRenderer>();

        // Get GameManager
        gameManager = FindObjectOfType<GameManager>();

        // Get Camera
        camera = Camera.main;
        cameraFollow = camera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the timer based on the frame rate
        timer += Time.deltaTime;

        // Change sprite every 1/frameRate seconds
        if (timer >= 1f / frameRate && canJump)
        {
            // Increment currentFrame and reset timer
            currentFrame = (currentFrame + 1) % animation.Count;
            timer = 0f;

            // Change the sprite in the SpriteRenderer
            renderer.sprite = animation[currentFrame];
        }

        // If not moving, set the standing sprite
        if (rigidBody.velocity.x == 0 && rigidBody.velocity.y == 0)
        {
            renderer.sprite = standingSprite;
        }

        if (Input.GetButtonDown("Jump") && canJump)
        {
            // Set sprite to the first sprite in the list when jumping
            renderer.sprite = animation[0];

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

        if(collision.gameObject.CompareTag("Die") || collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.EndGame();
        }

        // 1 -> Pyramid Exploration Minigame
        // 2 -> Archery Minigame
        // 3 -> CrossRoad Minigame
        // 4 -> SpellingBee Minigame
        // 5 -> Puzzle Minigame

        if (collision.gameObject.CompareTag("ObjectEgypt"))
        {
            gameManager.ChangeObjectScene(1);
        }

        if (collision.gameObject.CompareTag("ObjectMiddleAges"))
        {
            gameManager.ChangeObjectScene(2);
        }
  
        if (collision.gameObject.CompareTag("ObjectPresent"))
        {
            gameManager.ChangeObjectScene(3);
        }
        if (collision.gameObject.CompareTag("ObjectPrehistory"))
        {
            gameManager.ChangeObjectScene(4);
        }
        
        if (collision.gameObject.CompareTag("ObjectJapan"))
        {
            gameManager.ChangeObjectScene(5);
        }
    }

}
