using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestructibleWall : MonoBehaviour
{
    public int health = 100;

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void decreaseHealth()
    {
        health -= 20;
        Debug.Log("Wall Got hit!..");
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
    }
   

}
