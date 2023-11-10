using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BowController : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject point;
    [SerializeField] private TextMeshProUGUI numberArrowsText;


    private float forceArrow = 15f;
    private Vector2 direction;

    GameObject[] points;
    private int numberPoints = 15;
    private float spacePoints = 0.05f;

    private int numberArrows = 10;


    public int NumberArrows
    {
        get{ return numberArrows;}
    }

    // Start is called before the first frame update
    void Start()
    {
        numberArrowsText.text = "ARROWS: " + numberArrows.ToString();

        points =  new GameObject[numberPoints]; ;
        for (int i = 0; i < numberPoints; i++){
            points[i] = Instantiate(point, transform.position, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the bow based on mouse
        RotateBowTowardsMouse();

        if (Input.GetMouseButtonDown(0) && numberArrows != 0)
        {
            ShootArrow();
        }

        for (int i = 0; i < numberPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spacePoints);
        }
    }


    void ShootArrow()
    {
        numberArrows -= 1;
        numberArrowsText.text = "ARROWS: " + numberArrows.ToString();

        // Create a new arrow instance.
        GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);

        // Apply force to the arrow in the forward direction.
        Rigidbody2D rigidBodyArrow = arrow.GetComponent<Rigidbody2D>();
        rigidBodyArrow.velocity = transform.right * forceArrow;
    }

    void RotateBowTowardsMouse()
    {
        Vector2 bowPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - bowPosition;

        transform.right = direction;
    }

    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)transform.position + direction.normalized*forceArrow*t + 0.9f*Physics2D.gravity*t*t;
        return position;
    }
}
