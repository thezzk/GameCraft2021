using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetBullet : MonoBehaviour
{
    //define damage of bullet, int
    public int damage = 2;
    //reference the particle for hitspark
    public GameObject hitspark;
    //define speed of bullet, float
    float speed = 10.5f;
    // Update is called once per frame

    void Update()
    {
        //move bullet forward at "speed"
        transform.Translate(Vector3.forward * speed * Time.deltaTime); //move forward in a z direction only
    }


    //when we collide with another object
    private void OnTriggerEnter(Collider other)
    {
        //if the other object is tagged as enemy
        if (other.tag == "Enemy" || other.tag == "Boss")
        {
            Instantiate(hitspark, transform.position, Quaternion.identity);
            //get the script component in the other object
            other.GetComponent<EnemyHP>().MinusHp(damage); // Get the EnemyHP script, then call the script's function Minushp with the argument damage
            //call the script with my "damage"

            //remove bullet from scene        
            Destroy(this.gameObject);

        }

    }
    //if the other object is tagged as "Enemy"
}
