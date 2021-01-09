using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
    public float enemySpawn = 3f; //enemy spawn interval
    public float giftSpawn = 5f; //gift box spawn interval
    public GameObject enemy; //reference enemy object
    public GameObject giftShield; //reference shield gift box
    public GameObject giftPet; //reference pet gift box
    public GameObject boss;//reference boss prefab
    bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("EnemySpawnRoutine");
        StartCoroutine("giftSpawnRoutine");
       
    }
    public void SpawnBoss()
    {
        Instantiate(boss, new Vector3(12, -5, -13), Quaternion.identity);
    }
    IEnumerator EnemySpawnRoutine()
    {
        
        while (!gameOver)
        {
            Instantiate(enemy, new Vector3(12, -5, -13), Quaternion.identity); //Vector3 is where u spawn the enemy can use empty object to find thep osition
            yield return new WaitForSeconds(enemySpawn);
        }
    }

    IEnumerator giftSpawnRoutine()
    {
        while (!gameOver)
        {
            int randomNumber = Random.Range(0, 2); //random number 0 or 1

            if (randomNumber == 0)
            {
                Instantiate(giftPet, new Vector3(0, 0, 0), Quaternion.identity);

            }
            else
            {
                Instantiate(giftShield,  new Vector3(3,0,0), Quaternion.identity);


            }
            yield return new WaitForSeconds(giftSpawn);
        }
    }
}

