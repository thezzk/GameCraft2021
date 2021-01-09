using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{

    //define enemy HP
    public int health = 5;
    //define the game manager script
    private GameManagerScript getGameManager;
    //define the ui manager script
    private UIManagerScript getUIManager;
    public GameObject enemyDieEffect;

    // Start is called before the first frame update
    void Start()
    {
        getGameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

        getUIManager = GameObject.Find("UIManager").GetComponent<UIManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //if enemy health is less than or equal to 0
        if (health <= 0)
        {
            //get UI manager to run method to add score
            getUIManager.UpdatePlayerScore();
            //tell game manager to kill one enemy
            getGameManager.killOneEnemy();
            Instantiate(enemyDieEffect, transform.position, Quaternion.identity);
            //destroy enemy
            Destroy(this.gameObject);
        }
    }

    public void MinusHp(int damageTaken)
        {
            //damage taken is based on the PetBullet "damage" value
            health -= damageTaken;
            print("Enemy health is " + health);
        }
}
