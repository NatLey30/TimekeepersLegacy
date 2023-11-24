using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer_Pyramid : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeLeftText;

    private float targetTime = 40.0f;
    private UIManagerPyramid manager;

    // Start is called before the first frame update
    void Start()
    {
        // Assign the UIManagerPyramid script
        manager = FindObjectOfType<UIManagerPyramid>();
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;

        timeLeftText.text = "Time left: " + targetTime.ToString();

        if (targetTime <= 0.0f)
        {
            // Call the EndGame method
            manager.EndGame("Time's up!", false);
        }

    }

}
