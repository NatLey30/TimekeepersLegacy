using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    // Delegate and an event for collision events
    public delegate void ArrowCollisionEvent(bool hit);
    public static event ArrowCollisionEvent OnArrowCollision;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(rigidBody.velocity.y, rigidBody.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            // Handle arrow hitting the target.
            OnArrowCollision?.Invoke(true); // Notify that the arrow hit the target
        }
        else
        {
            // Handle arrow miss.
            OnArrowCollision?.Invoke(false); // Notify that the arrow missed.
        }
        Destroy(gameObject);
    }
}
