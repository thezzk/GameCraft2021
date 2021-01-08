using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetRotateToEnemy : MonoBehaviour
{

    //define who to look at
    private Transform target; //transform is the x y z





    //when we collide wigth other object

    //if the other object is tagged as enemy

    //tell the pet that this object is the target


    //Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate to look at target every frame
        transform.LookAt(target);

    }
    //when we collide with other object
    private void OnTriggerStay(Collider other)
    {
        //if the other object is tagged as enemy
        if (other.tag == "Enemy" || other.tag == "Boss")
        {
            //tell the pet that this object is the target
            target = other.transform;
        }
    }
}
