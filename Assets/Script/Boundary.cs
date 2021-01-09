using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Boundary : MonoBehaviour
{
    [SerializeField] float resetTime = 3f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControl>() != null)
        {
            other.GetComponent<NavMeshAgent>().Warp(new Vector3(104.12f, -104.4f, 82.32f));
            other.GetComponent<PlayerControl>().resetTime = resetTime;
        }
    }
}
