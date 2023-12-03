using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObject : MonoBehaviour
{

    [SerializeField] private GameObject objectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        objectPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        objectPrefab.SetActive(true);
    }

    void OnMouseUp()
    {
        objectPrefab.SetActive(false);
    }
}
