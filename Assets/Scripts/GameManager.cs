using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData
{
    private float posX;
    private float posY;
    private float posZ;

    public float PosX {
        get { return posX; }
        set { posX = value; }
    }

    public float PosY
    {
        get { return posY; }
        set { posY = value; }
    }

    public float PosZ
    {
        get { return posZ; }
        set { posZ = value; }
    }
}

public class EnemyData
{
    private float posX;
    private float posY;
    private float posZ;

    public float PosX
    {
        get { return posX; }
        set { posX = value; }
    }

    public float PosY
    {
        get { return posY; }
        set { posY = value; }
    }

    public float PosZ
    {
        get { return posZ; }
        set { posZ = value; }
    }
}

public class GameManager : MonoBehaviour
{
    PlayerData playerData = new PlayerData();
    EnemyData enemyData = new EnemyData();

    [SerializeField] private GameObject objectEgyptPrefab;
    [SerializeField] private GameObject objectMiddleAgesPrefab;
    //[SerializeField] private GameObject objectPresentPrefab;
    [SerializeField] private GameObject objectPrehistoryPrefab;
    [SerializeField] private GameObject objectJapanPrefab;

    private GameObject player;
    private GameObject enemy;
    private GameObject objectEgypt;
    private GameObject objectMiddleAges;
    //private GameObject objectPresent;
    private GameObject objectPrehistory;
    private GameObject objectJapan;
    private Material material;

    private List<string> sceneNames = new List<string>();
    private string currentScene;

    private bool objectEgyptDone = false;
    private bool objectMiddleAgesDone = false;
    private bool objectPresentDone = false;
    private bool objectPrehistoryDone = false;
    private bool objectJapanDone = false;

    public static GameManager Instance { get; private set; }

    public Material Material
    {
        get { return material; }
        set { material = value; }
    }

    public bool ObjectEgyptDone
    {
        get { return objectEgyptDone;}
    }
    public bool ObjectMiddleAgesDone
    {
        get { return objectMiddleAgesDone; }
    }
    public bool ObjectPresentDone
    {
        get { return objectPresentDone; }
    }
    public bool ObjectPrehistoryDone
    {
        get { return objectPrehistoryDone; }
    }
    public bool ObjectJapanDone
    {
        get { return objectJapanDone; }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Get Player
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

        // We dont want the game manager to be destrayed when changing scenes
        DontDestroyOnLoad(gameObject);

        currentScene = SceneManager.GetActiveScene().name;

        sceneNames.Add("SceneEgypt");
        sceneNames.Add("SceneMiddleAges");
        sceneNames.Add("ScenePrehistory");
        //sceneNames.Add("ScenePresent");
        sceneNames.Add("SceneJapan");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void TransitionToRandomScene()
    {
        List<string> sceneNamesCopy = new List<string>(sceneNames);

        currentScene = SceneManager.GetActiveScene().name;

        if (currentScene != "SceneDystopicFuture")
        {
            sceneNamesCopy.Remove(currentScene);
        }

        // Randomly choose a scene from the available scenes.
        int randomIndex = Random.Range(0, sceneNamesCopy.Count);
        string sceneToLoad = sceneNamesCopy[randomIndex];

        // Load the chosen scene.
        ChangeScene(sceneToLoad, true);

        // Place Minigame Object
        StartCoroutine(DelayedPlaceObject(sceneToLoad));


    }

    public void ChangeObjectScene(int num)
    {
        // 1 -> Pyramid Exploration Minigame
        // 2 -> Archery Minigame
        // 3 -> CrossRoad Minigame
        // 4 -> SpellingBee Minigame
        // 5 -> Puzzle Minigame
        if (num == 1)
        {
            // Save player's position too
            SavePlayerPosition();
            SaveEnemyPosition();

            // Save current scene mame
            currentScene = SceneManager.GetActiveScene().name;

            // Load the Pyramid Exploration Minigame scene
            ChangeScene("PyramidExploration", false);
        }
        else if (num == 2)
        {
            // Save player's position too
            SavePlayerPosition();
            SaveEnemyPosition();

            // Save current scene mame
            currentScene = SceneManager.GetActiveScene().name;

            // Load the Pyramid Exploration Minigame scene
            ChangeScene("Archery", false);
        }
        /*
        else if (num == 3)
        {

        }
        */
        else if (num == 4)
        {
            // Save player's position too
            SavePlayerPosition();
            SaveEnemyPosition();

            // Save current scene mame
            currentScene = SceneManager.GetActiveScene().name;

            // Load the Pyramid Exploration Minigame scene
            ChangeScene("SpellingBee", false);
        }
        /*
        else
        {
            // Save player's position too
            SavePlayerPosition();
            SaveEnemyPosition();

            // Save current scene mame
            currentScene = SceneManager.GetActiveScene().name;

            // Load the Pyramid Exploration Minigame scene
            ChangeScene("Puzzle", false);
        }
        */

    }

    private void ChangeScene(string Name, bool find)
    {
        // Load the scene
        SceneManager.LoadScene(Name);

        // Get Player in new Scene
        if (find)
        {
            StartCoroutine(DelayedFindPlayer());
            StartCoroutine(DelayedFindEnemy());
        }
    }

    private IEnumerator DelayedFindPlayer()
    {
        // Wait for the next frame
        yield return null;

        // Get Player in the new Scene
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private IEnumerator DelayedFindEnemy()
    {
        // Wait for the next frame
        yield return null;

        // Get Player in the new Scene
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private IEnumerator DelayedPlaceObject(string sceneToLoad)
    {
        // Wait for the next frame
        yield return null;

        if (sceneToLoad == "SceneEgypt")
        {
            objectEgypt = Instantiate(objectEgyptPrefab);
            objectEgypt.transform.position = new Vector3(80, 0, 0);

            if (objectEgyptDone)
            {
                objectEgypt.layer = LayerMask.NameToLayer("ObjectFound");

                ChangeTransparency(objectEgypt);
            }
        }
        else if (sceneToLoad == "SceneMiddleAges")
        {
            objectMiddleAges = Instantiate(objectMiddleAgesPrefab);
            objectMiddleAges.transform.position = new Vector3(80, 0, 0);

            if (objectMiddleAgesDone)
            {
                objectMiddleAges.layer = LayerMask.NameToLayer("ObjectFound");
                ChangeTransparency(objectMiddleAges);
            }
        }
        /*
        else if (sceneToLoad == "")
        {
            
        }
        */
        else if (sceneToLoad == "")
        {
            objectPrehistory = Instantiate(objectPrehistoryPrefab);
            objectPrehistory.transform.position = new Vector3(80, 0, 0);

            if (objectPrehistoryDone)
            {
                objectPrehistory.layer = LayerMask.NameToLayer("ObjectFound");
                ChangeTransparency(objectPrehistory);
            }
        }
        /*
        else if (sceneToLoad == "SceneJapan")
        {
            objectJapan = Instantiate(objectJapanPrefab);
            objectJapan.transform.position = new Vector3(80, 0, 0);

            if (objectJapanDone)
            {
                objectJapan.layer = LayerMask.NameToLayer("ObjectFound");
                ChangeTransparency(objectJapan);
            }
        }
        */
    }

    private void SavePlayerPosition()
    {
        playerData.PosX = player.transform.position.x;
        playerData.PosY = player.transform.position.y;
        playerData.PosZ = player.transform.position.z;
    }

    private void LoadPlayerPosition()
    {
        // Load the player's position from PlayerPrefs
        float posX = playerData.PosX + 5f;
        float posY = playerData.PosY;
        float posZ = playerData.PosZ;

        // Set the player's position
        player.transform.position = new Vector3(posX, posY, posZ);
    }

    private IEnumerator DelayedLoadPlayerPosition()
    {
        // Wait for the next frame
        yield return null;

        // Get Player in position
        LoadPlayerPosition();
    }

    private void SaveEnemyPosition()
    {
        enemyData.PosX = enemy.transform.position.x;
        enemyData.PosY = enemy.transform.position.y;
        enemyData.PosZ = enemy.transform.position.z;
    }

    private void LoadEnemyPosition()
    {
        // Load the player's position from PlayerPrefs
        float posX = enemyData.PosX + 5f;
        float posY = enemyData.PosY;
        float posZ = enemyData.PosZ;

        // Set the player's position
        enemy.transform.position = new Vector3(posX, posY, posZ);
    }

    private IEnumerator DelayedLoadEnemyPosition()
    {
        // Wait for the next frame
        yield return null;

        // Get Player in position
        LoadEnemyPosition();
    }

    public void ReturnToRunningScene(bool win)
    {
        // Load the last scene
        ChangeScene(currentScene, true);

        // Load the player's position previously saved
        // We have to delay a little bit to find the player object first
        StartCoroutine(DelayedLoadPlayerPosition());
        StartCoroutine(DelayedLoadEnemyPosition());

        // If win we have to deactivate the collider
        if (win)
        {
            if (currentScene == "SceneEgypt")
            {
                objectEgyptDone = true;
            }
            else if (currentScene == "SceneMiddleAges")
            {
                objectMiddleAgesDone = true;
            }
            /*
            else if (currentScene == "")
            {
                
            }
            */
            else if (currentScene == "ScenePrehistory")
            {
                objectPrehistoryDone = true;
            }
            /*
            else if (currentScene == "Japan")
            {
                objectJapanDone = true;
            }
            */
        }

    }

    private void ChangeTransparency(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();

        // Get the current material color
        Color currentColor = renderer.material.color;

        // Set the new alpha value
        currentColor.a = 0.2f;

        // Update the material color with the new alpha
        renderer.material.color = currentColor;
    }





    public void StartGame()
    {
        ChangeScene("SceneDystopicFuture", true);
        currentScene = "SceneDystopicFuture";
    }

    public void EndGame()
    {
        ChangeScene("Menu", false);
        objectEgyptDone = false;
        objectMiddleAgesDone = false;
        objectPresentDone = false;
        objectPrehistoryDone = false;
        objectJapanDone = false;
    }
}
