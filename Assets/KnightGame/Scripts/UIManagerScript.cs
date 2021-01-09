using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //necessary if u want to manage the scenes

public class UIManagerScript : MonoBehaviour
{
    public static UIManagerScript instance; 
    GameObject[] startPlay; //this will be an array of objects that are tagged with "UI"
    GameObject UIelement; //this will be the UI elements that is in the array
    public GameObject gameOverMenu; //gameover canvas
    public GameObject gameWorldMenu; //gameworld canvas

    public int playerScore = 0;

    private void Awake()
    {
        if (instance == null) //if there is no instance of UIManager script, create it. This is to ensure there will not be 2 instances of UIManager script in the same scene
        {
            instance = this;

        }
        else if (instance!=this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; //make sure u unpaused everything

    }

    public void UpdatePlayerScore()
    {
        playerScore += 1;
    }
    public void ButtonReplay()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex); // getactivescene(whichever scene is open at the moment). This mean when button is pressed, it will reload the whole scene
    }

    public void GameOver()
    {
        

        startPlay = GameObject.FindGameObjectsWithTag("UI"); //This array will consist of all gameobjects with the tag UI
        foreach (GameObject UIelement in startPlay)
        {
            UIelement.SetActive(false); //Set all the gameobjects with the tag UI to be inactive (hide all of them)
        }

        gameOverMenu.SetActive(true);

        Invoke("DelayPause", 0.1f);
 

    }
    void DelayPause()
    {
        Time.timeScale = 0; //pause the game
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
