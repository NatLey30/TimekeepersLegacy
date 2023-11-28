using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private float maxRotationAngle = 60f;
    private float minRotationAngle = -60f;

    private Transform pivot;

    // Start is called before the first frame update
    void Start()
    {
        // Set the pivot as the parent
        pivot = transform.parent;

        float rotationSpeed = 1f;

        RotateContinuously(rotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RotateContinuously(float rotationSpeed)
    {
        // Rotates to one side
        transform.DORotate(new Vector3(0f, 0f, maxRotationAngle), rotationSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            // Rotación to the ather side once completing the previous rotation
            transform.DORotate(new Vector3(0f, 0f, minRotationAngle), rotationSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                // Recursive call to rotate continously
                RotateContinuously(rotationSpeed);
            });
        });
    }

}
