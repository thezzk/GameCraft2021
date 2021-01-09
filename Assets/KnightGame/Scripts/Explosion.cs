using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //define bomb damage

    int bombDamage = 5;
    //detect object that collide with explosion

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Boss")
        {   
            //minus bomb damage to the enemy hp
            other.GetComponent<EnemyHP>().MinusHp(bombDamage);
        } 
    }

   
}
