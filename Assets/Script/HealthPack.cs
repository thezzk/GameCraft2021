using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public event Action onGainedHealthPack;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerControl>();
        if (player != null)
        {
            onGainedHealthPack();
            Destroy(this.gameObject);
            other.GetComponent<PlayerControl>().gainHealthPack();
        }
    }

}
