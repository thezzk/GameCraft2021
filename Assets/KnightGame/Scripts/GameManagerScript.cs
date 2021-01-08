using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //define the enemy killed. 0 when game starts
    private int enemyKilled = 0;
    private bool bossSpawned = false;
    //define the SpawnManagerScript in SpawnManager
    private SpawnManagerScript getSpawnManager;

    public bool gameover = false;

    //define the UI manager
    private UIManagerScript getUIManager;

    // Start is called before the first frame update
    void Start()
    {
        getSpawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerScript>(); //GameObject refers to object in hierarchy (found in your scene), getcomponent is to get the component found inside the gameobject
        getUIManager = GameObject.Find("UIManager").GetComponent<UIManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //if enemy killed is more than 3
        if (enemyKilled > 3 && bossSpawned == false)
        {
            //spawn boss
            getSpawnManager.SpawnBoss(); //Then once you get the script, you can call any functions in the script
            bossSpawned = true;
            //reset the enemy killed variable, so that we can count to 3, to spawn boss again
            enemyKilled = 0;
        }

        if (gameover) {
            getUIManager.GameOver(); //play the GameOver method of UIManagerScript when  gameover is true

        }
        
    }

    public void IsGameOver() //Allows the player to change the gameover value to true
    {
        gameover = true;
    }
    public void killOneEnemy()
    {
        //enemyKilled++;
        enemyKilled += 1;
        //enemyKilled = enemyKilled + 1;
        
    }
}
