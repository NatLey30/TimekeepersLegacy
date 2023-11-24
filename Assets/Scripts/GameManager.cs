using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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

public class GameManager : MonoBehaviour
{
    PlayerData playerData = new PlayerData();

    [SerializeField] private GameObject playerPrefab;

    private GameObject player;
    private Material material;

    private List<string> sceneNames = new List<string>();
    private string currentScene;

    public Material Material
    {
        get { return material; }
        set { material = value; }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Get Player
        // player = FindObjectOfType<PlayerMovement>();
        player = GameObject.FindGameObjectWithTag("Player");

        // We dont want the game manager to be destrayed when changing scenes
        DontDestroyOnLoad(gameObject);

        currentScene = SceneManager.GetActiveScene().name;

        //sceneNames.Add("SceneAmericanWildWest");
        sceneNames.Add("SceneDystopicFuture");
        sceneNames.Add("SceneEgypt");
        //sceneNames.Add("SceneMiddleAges");
        //sceneNames.Add("ScenePrehistory");
        //sceneNames.Add("ScenePresent");
        //sceneNames.Add("SceneQinDinasty");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        // Start the game on the same scene every time
        ChangeScene("SceneDystopicFuture", true);
    }

    public void TransitionToRandomScene()
    {
        List<string> sceneNamesCopy = sceneNames;

        currentScene = SceneManager.GetActiveScene().name;

        sceneNamesCopy.Remove(currentScene);

        // Randomly choose a scene from the available scenes.
        int randomIndex = Random.Range(0, sceneNamesCopy.Count);
        string sceneToLoad = sceneNamesCopy[randomIndex];

        // Load the chosen scene.
        ChangeScene(sceneToLoad, true);
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

            // Save current scene mame
            currentScene = SceneManager.GetActiveScene().name;

            // Load the Pyramid Exploration Minigame scene
            ChangeScene("PyramidExploration", false);
        }
        /*
        else if (num == 2)
        {

        }
        else if (num == 3)
        {

        }
        else if(num == 4)
        {

        }
        else
        {

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
        }
    }

    private IEnumerator DelayedFindPlayer()
    {
        // Wait for the next frame
        yield return null;

        // Get Player in the new Scene
        player = GameObject.FindGameObjectWithTag("Player");
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

    public void ReturnToRunningScene()
    {
        // Load the last scene
        ChangeScene(currentScene, true);

        // Lets disable the object for the minigame+
        // objectMinigame.SetActive(false);

        // Load the player's position previously saved
        // We have to delay a little bit to find the player object first
        StartCoroutine(DelayedLoadPlayerPosition());

    }

    private IEnumerator DelayedLoadPlayerPosition()
    {
        // Wait for the next frame
        yield return null;

        // Get Player in position
        LoadPlayerPosition();
    }

    public void EndGame()
    {

    }

    public void PauseGame()
    {

    }
}
