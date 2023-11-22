using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_RodeoBull : MonoBehaviour
{
    private float speed = 0.2f;
    private bool isFacingRight = true;
    private bool onBull = true;

    private Rigidbody2D rigidBody;
    private BullController bullController;

    // Delegate and an event for collision events
    public delegate void GroundCollisionEvent();
    public static event GroundCollisionEvent OnCollision;

    // Start is called before the first frame update
    void Start()
    {
        // Get Rigid Body from the object assigned to the script
        rigidBody = GetComponent<Rigidbody2D>();

        // Adjust centre of mass
        Vector2 centerOfMass = new Vector2(0f, -1.5f);
        rigidBody.centerOfMass = centerOfMass;

        // Find the BullController script in the scene
        bullController = FindObjectOfType<BullController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Fixed Update is executed at a specific rate defined by the editor
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector2 movementForce = new Vector2(horizontal * speed, 0);
        rigidBody.velocity += movementForce;


        // Calculate the force based on the bull's rotation
        if (onBull) {
            float bullRotation = bullController.transform.eulerAngles.z;
            Vector2 bullDirection = new Vector2(Mathf.Cos(bullRotation), Mathf.Sin(bullRotation));

            rigidBody.velocity += bullDirection * 1.2f;
        }

        // Call flip method
        Flip();

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
            Debug.Log("GameOver");
            OnCollision?.Invoke();
        }

        // Check if character on bull
        if (collision.gameObject.tag == "Bull")
        {
            onBull = true;
            Debug.Log("YES");
        }
        else
        {
            onBull = false;
            Debug.Log("NO");
        }
    }

    
}
