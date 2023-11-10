using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    private float parallaxEffect = 0.5f; // Adjust this value to control the parallax speed.

    [SerializeField] private Transform cam;
    [SerializeField] private Vector3 previousCamPosition;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        previousCamPosition = cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = cam.position - previousCamPosition;
        transform.position += new Vector3(delta.x * parallaxEffect, delta.y * parallaxEffect, 0);
        previousCamPosition = cam.position;
    }

}
