using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject carPrefabLeft;
    [SerializeField] private GameObject carPrefabRight;

    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private Transform spawnPoint3;

    private Transform[] spawnPoints = new Transform[3]; // Array of lane positions
    private float spawnInterval = 0.7f; // Time interval between spawns
    private float nextSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints[0] = spawnPoint1;
        spawnPoints[1] = spawnPoint2;
        spawnPoints[2] = spawnPoint3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnCar();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnCar()
    {
        // Randomly choose a lane to spawn the car
        int laneIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[laneIndex];

        if (laneIndex == 1)
        {
            // Instantiate the car prefab at the chosen spawn point
            GameObject car = Instantiate(carPrefabRight, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            // Instantiate the car prefab at the chosen spawn point
            Instantiate(carPrefabLeft, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
