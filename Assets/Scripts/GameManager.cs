using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //[SerializeField]  // Reference to the player's GameObject

    private List<string> sceneNames = new List<string>();
    private string currentScene;
    private Material material;

    public Material Material
    {
        get { return material; }
        set { material = value; }
    }

    // Start is called before the first frame update
    private void Start()
    {
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
        SceneManager.LoadScene("SceneDystopicFuture");
    }

    public void TransitionToRandomScene()
    {
        currentScene = SceneManager.GetActiveScene().name;

        sceneNames.Remove(currentScene);

        // Randomly choose a scene from the available scenes.
        int randomIndex = Random.Range(0, sceneNames.Count);
        string sceneToLoad = sceneNames[randomIndex];

        // Load the chosen scene.
        SceneManager.LoadScene(sceneToLoad);
    }
}
