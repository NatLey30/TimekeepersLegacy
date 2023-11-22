using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerControllerPyramid : MonoBehaviour
{
 
    private float moveSpeed = 5f;
    float rotationAmount = 90f;

    private Camera camera;
    Transform cameraFollow;

    // Delegate and an event for collision events
    public delegate void GroundCollisionEvent();
    public static event GroundCollisionEvent OnSarcophagusCollision;


    // Start is called before the first frame update
    void Start()
    {
        // Get Camera
        camera = Camera.main;
        cameraFollow = camera.transform;
    }

    // Update is called once per frame
   
    void Update()
    {
        // Move the player left and right
        float moveY = Input.GetAxis("Vertical");
        transform.Translate(Vector2.up * moveY * moveSpeed * Time.deltaTime);

        float moveX = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * moveX * moveSpeed * Time.deltaTime);

        // Update camera position to follow the player
        if (cameraFollow != null)
        {
            cameraFollow.position = new Vector3(transform.position.x, transform.position.y + 2f, cameraFollow.position.z);
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Only when colliding with objects with Ground tag, can the player jump
        if (collision.gameObject.tag == "Sarcophagus")
        {
            Debug.Log("You Win");
            OnSarcophagusCollision?.Invoke();
        }
    }
}
