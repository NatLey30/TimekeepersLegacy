using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BullController : MonoBehaviour
{
    private float maxRotationAngle = 20f;
    private float minRotationAngle = -20f;

    private Rigidbody2D rigidBody;

    // The time interval for changing rotation speed
    private float changeSpeedInterval = 2f;
    private float currentRotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        // Adjust centre of mass
        Vector2 centerOfMass = new Vector2(0f, -1.5f);
        rigidBody.centerOfMass = centerOfMass;

        // Generate a random rotation speed between 1 and 5
        float randomRotationSpeed = Random.Range(0.4f, 2f);

        RotateBullContinuously(randomRotationSpeed);
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if it's time to change rotation speed
        changeSpeedInterval -= Time.deltaTime;
        if (changeSpeedInterval <= 0f)
        {
            // Change the rotation speed
            SetRandomRotationSpeed();
        }
    }

    void RotateBullContinuously(float rotationSpeed)
    {
        // Rotates to one side
        transform.DORotate(new Vector3(0f, 0f, maxRotationAngle), rotationSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            // Rotación to the ather side once completing the previous rotation
            transform.DORotate(new Vector3(0f, 0f, minRotationAngle), rotationSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                // Recursive call to rotate continously
                RotateBullContinuously(rotationSpeed);
            });
        });
    }

    void SetRandomRotationSpeed()
    {
        // Set a new random rotation speed
        currentRotationSpeed = Random.Range(0.25f, 2f); // Adjust the range as needed
    }

}
