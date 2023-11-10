using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;

    // Delegate and an event for collision events
    public delegate void CarCollisionEvent(bool car);
    public static event CarCollisionEvent OnCollision;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player left and right
        float moveY = Input.GetAxis("Vertical");
        transform.Translate(Vector2.up * moveY * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            OnCollision?.Invoke(true);
        }

        if (collision.gameObject.CompareTag("Fin"))
        {
            OnCollision?.Invoke(false);
        }
    }
}
