using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //define the explosion particle prefab
    public GameObject explosionEffect;

    //reference to the Aura when player van use skill
    public GameObject readySkill;
    //duration to use skill
    private float skillCoolDown = 5f;

    //record time to track time player can use skill
    private float canUseSkill = 0f;

    //reference the main camera with the script to shake camera
    private GameObject shakeCamera;

    private void Start()
    {
        shakeCamera = GameObject.Find("Main Camera");
            
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
            
        }

        if (Time.time > canUseSkill)
        {
            readySkill.SetActive(true);
        }
    }

    private void Attack()
    {
        //if game time is more than canUseSkill time
        if (Time.time > canUseSkill)
        {
            shakeCamera.GetComponent<CompleteCameraController>().ShakeCamera();
            readySkill.SetActive(false); //turn off the aura so u cannot see it
            //spawn explosion
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            //update canUseSkill time by taking current game time + skillCoolDown time
            canUseSkill = Time.time + skillCoolDown;
        }
    }
}
