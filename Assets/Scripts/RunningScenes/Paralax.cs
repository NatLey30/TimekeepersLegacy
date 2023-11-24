using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class Paralax : MonoBehaviour
{
    [Header("Parameters")]
    public float parallaxSpeed = 1f;

    private Material material;
    private Vector2 originalOffset;

    void Start()
    {
        // Get the material from the Renderer
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        material = renderer.material;

        // Store the original offset for reference
        originalOffset = material.mainTextureOffset;
    }

    void Update()
    {
        // Calculate the parallax offset based on the camera's movement
        float parallaxOffset = Camera.main.transform.position.x * parallaxSpeed;

        // Apply the offset to the material's main texture
        Vector2 offset = originalOffset + new Vector2(parallaxOffset, 0);
        material.mainTextureOffset = offset;
    }
}
