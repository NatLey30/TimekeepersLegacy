using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private float maxRotationAngle = 70f;
    private float minRotationAngle = -70f;
    private float rotationSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        RotateContinuously();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RotateContinuously()
    {
        // Rotates to one side
        transform.parent.DORotate(new Vector3(0f, 0f, maxRotationAngle), rotationSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            // Rotación to the ather side once completing the previous rotation
            transform.parent.DORotate(new Vector3(0f, 0f, minRotationAngle), rotationSpeed).SetEase(Ease.Linear).OnComplete(() =>
            {
                // Recursive call to rotate continously
                RotateContinuously();
            });
        });
    }

}
