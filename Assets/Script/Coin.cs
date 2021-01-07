using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
   public event Action onGainedCoin; 

   private void OnTriggerEnter(Collider other) 
   {
       onGainedCoin();
       Destroy(this.gameObject);
       other.GetComponent<PlayerControl>().gainCoin();
   }
 
}
