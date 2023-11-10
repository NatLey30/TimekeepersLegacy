using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManagerPyramid : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private Canvas canvasStart;
    //[SerializeField] private Canvas canvasFin;
    //[SerializeField] private TextMeshProUGUI gameEnd;

    // Start is called before the first frame update
    void Start()
    {
        //canvasFin.enabled = false;
        player.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        canvasStart.enabled = false;
        player.SetActive(true);
    }
}
