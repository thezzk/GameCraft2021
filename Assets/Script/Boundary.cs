using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Boundary : MonoBehaviour
{
    [SerializeField] float resetTime = 3f;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<PlayerControl>() != null)
        {
            other.GetComponent<NavMeshAgent>().Warp(new Vector3(24f, other.gameObject.transform.position.y, 9.3f));
            other.GetComponent<PlayerControl>().resetTime = resetTime;
        }    
    }
}
