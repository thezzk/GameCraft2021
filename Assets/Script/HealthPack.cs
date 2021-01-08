using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
   public event Action onGainedHealthPack; 

   private void OnTriggerEnter(Collider other) 
   {
       Debug.Log("Health pack ++");
       onGainedHealthPack();
       Destroy(this.gameObject);
       other.GetComponent<PlayerControl>().gainHealthPack();
   }
 
}
