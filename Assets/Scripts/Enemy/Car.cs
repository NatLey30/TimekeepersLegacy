using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float carSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        MoveCar();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * carSpeed * Time.deltaTime);
    }

    private void MoveCar()
    {
        // Move the car to the left
        transform.Translate(Vector3.right * carSpeed * Time.deltaTime);
    }
}
