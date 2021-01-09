using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action onGainedCoin;
    [SerializeField] public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onGainedCoin();
            Destroy(this.gameObject);
            other.GetComponent<PlayerControl>().gainCoin();
        }
    }

    public void startSpinning()
    {
        anim.SetBool("spin", true);
    }

}
