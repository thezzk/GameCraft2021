﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] public Healthbar healthBar;
    [SerializeField] string horizontalAxisName;
    [SerializeField] string verticalAxisName;
    [SerializeField] KeyCode waveBtnKeyCode;
    [SerializeField] KeyCode fireBtnKeyCode;
    [SerializeField] KeyCode rushBtnKeyCode;
    public float waveColdTime;
    public float fireColdTime;
    public float rushColdTime;

    [HideInInspector] public float waveColdDown = 0;
    [HideInInspector] public float fireColdDown = 0;
    [HideInInspector] public float rushColdDown = 0;



    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject coinBullet;
    [SerializeField] GameObject waveEffect;

    [SerializeField] GameObject coinPref;

    [SerializeField] GameObject GameManager;


    public int coinNum;
    [HideInInspector] public float resetTime = 0f;

    Vector3 movement;
    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        GameManager = GameObject.FindGameObjectWithTag("Manager");
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<GameMgr>().gameRunning) return;

        if (resetTime > Mathf.Epsilon)
        {
            if (GetComponent<Animation>().clip == null)
            {
                GetComponent<Animation>().Play("PlayerBlink");
            }
            resetTime -= Time.deltaTime;
            return;
        }

        if (GetComponent<Animation>().clip != null)
        {
            GetComponent<Animation>().clip = null;
        }
        float horizontalInput = Input.GetAxis(horizontalAxisName);
        float verticalInput = Input.GetAxis(verticalAxisName);

        movement.Set(horizontalInput, 0f, verticalInput);

        agent.Move(movement * Time.deltaTime * agent.speed);
        agent.SetDestination(transform.position + movement);

        if (Input.GetKeyDown(waveBtnKeyCode) && waveColdDown < Mathf.Epsilon)
        {
            Wave();
            waveColdDown = waveColdTime;
        }
        if (Input.GetKeyDown(fireBtnKeyCode) && fireColdDown < Mathf.Epsilon)
        {
            if (Fire())
                fireColdDown = fireColdTime;
        }
        if (Input.GetKeyDown(rushBtnKeyCode) && rushColdDown < Mathf.Epsilon)
        {
            Rush();
            rushColdDown = rushColdTime;
        }


        if (healthBar.health < 1f)
        {
            int coinsLost = Mathf.FloorToInt(coinNum * 0.5f);
            for (int i = 0; i < coinsLost; i++)
            {
                var coin = Instantiate(coinPref, transform.position + new Vector3(Random.Range(0, 3f), 0f, Random.Range(0, 3f)), Quaternion.identity);
                coin.transform.GetChild(0).GetComponent<Coin>().onGainedCoin += GameManager.GetComponent<GameMgr>().DecCoinCnt;
            }

            healthBar.GainHealth(100f);
            coinNum -= coinsLost;
            GetComponent<NavMeshAgent>().Warp(new Vector3(104.12f, -104.4f, 82.32f));
            resetTime = 3f;
        }


        // if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        // {
        //     transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        // }
        UpdateColdDown();

    }

    void UpdateColdDown()
    {
        rushColdDown -= Time.deltaTime;
        rushColdDown = Mathf.Max(0, rushColdDown);
        waveColdDown -= Time.deltaTime;
        waveColdDown = Mathf.Max(0, waveColdDown);
        fireColdDown -= Time.deltaTime;
        fireColdDown = Mathf.Max(0, fireColdDown);
    }

    void Rush()
    {
        GameObject[] objArray = { gameObject };
        var skill = new RushSkill();
        skill.begin(objArray);
        StartCoroutine(skill.running());

    }
    void Wave()
    {
        GameObject[] objArray = { gameObject, waveEffect };
        var skill = new WaveSkill();
        skill.begin(objArray);
        StartCoroutine(skill.running());

    }

    bool Fire()
    {
        GameObject[] objArray = { gameObject, coinBullet, firePoint };
        var skill = new LaunchCoinSkill();
        bool retval = skill.begin(objArray);
        skill.end();
        return retval;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(this.transform.position, WaveSkill.range);
    }

    public void gainCoin()
    {
        coinNum += 1;
    }
    public void gainHealthPack()
    {
        healthBar.SetHealth(healthBar.health + 30);
        if (healthBar.health > 100f)
        {
            healthBar.SetHealth(100f);
        }
    }

}
