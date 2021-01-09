﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //so navmeshagent will work

public class PetToPlayer : MonoBehaviour
{
    //define knight transform position for enemy to follow
    private Transform goal;


    //define enemy NavMesh (itself)
    public NavMeshAgent pet;


    // Start is called before the first frame update
    void Start()
    {
        //find the knight gameobject and define knight's transform as the "goal"
        goal = GameObject.Find("Hero_01_Knight").transform;
        //tell navmesh agent to go to goal's transform position
        pet.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        pet.destination = goal.position;
    }
}
