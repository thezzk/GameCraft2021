﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    [SerializeField] Terrain genTerrain;
    [SerializeField] GameObject coinPref;
    [SerializeField] int maxCoin;
    [SerializeField] GameObject coinGenPoints;
    [SerializeField] GameObject goldCoinPref;
    [SerializeField] int maxGoldCoin;
    [SerializeField] GameObject goldCoinGenPoints;
    [SerializeField] GameObject healthPackPref;
    [SerializeField] int maxHealthPack;
    [SerializeField] GameObject healthPackGenPoints;
    [SerializeField] float GameTime = 50f;
    [SerializeField] GameObject[] leftTurrets;
    [HideInInspector] public bool gameRunning = true;
    [HideInInspector] public float currentGameTime;

    private int coinCnt = 0;
    private int goldCoinCnt = 0;
    public int goldCoinDelay2 = 10;
    public int coinRushDelay = Random.Range(10,30);
    private int healthPackCnt = 0;


    IEnumerator waitAndGenCoin()
    {
        coinRushDelay = Random.Range(10,30);
        while (gameRunning)
        {
            yield return new WaitForSeconds(1f);
            if (coinRushDelay == 0 ) {
                Debug.Log(coinRushDelay);
                Debug.Log("Coin Rush!!");
                int i = 0;
                while (i < 30) {
                    int genPointIndex = Random.Range(0, coinGenPoints.transform.childCount);
                    Transform genPointTran = coinGenPoints.transform.GetChild(genPointIndex);
                    var coin = Instantiate(coinPref, genPointTran.position, Quaternion.identity);
                    coin.transform.GetChild(0).GetComponent<Coin>().onGainedCoin += DecCoinCnt;
                    coinCnt++; 
                    i++;                   
                }
                   
            }            
            else if (coinCnt < maxCoin)
            {

                int genPointIndex = Random.Range(0, coinGenPoints.transform.childCount);
                Transform genPointTran = coinGenPoints.transform.GetChild(genPointIndex);
                var coin = Instantiate(coinPref, genPointTran.position, Quaternion.identity);
                coin.transform.GetChild(0).GetComponent<Coin>().onGainedCoin += DecCoinCnt;
                coinCnt++;
            }
            if (goldCoinDelay2 == 0 && goldCoinCnt < maxGoldCoin)
            {
                int genPointIndex = Random.Range(0, goldCoinGenPoints.transform.childCount);
                Transform genPointTran = goldCoinGenPoints.transform.GetChild(genPointIndex);
                var goldCoin = Instantiate(goldCoinPref, genPointTran.position, Quaternion.identity);
                goldCoin.transform.GetChild(0).GetComponent<GoldCoin>().onGainedCoin += DecGoldCoinCnt;
                goldCoinCnt++;
                
            }
            if (goldCoinDelay2 == 0) {
                goldCoinDelay2 = 10;
            }
            if (coinRushDelay == 0) {
                coinRushDelay = Random.Range(10,30);
            }
            
            if (healthPackCnt < maxHealthPack)
            {
                int genPointIndex = Random.Range(0, healthPackGenPoints.transform.childCount);
                Transform genPointTran = healthPackGenPoints.transform.GetChild(genPointIndex);
                var healthPack = Instantiate(healthPackPref, genPointTran.position, Quaternion.identity);
                healthPack.GetComponent<HealthPack>().onGainedHealthPack += DecHealthPackCnt;
                healthPackCnt++;
            }
            goldCoinDelay2--;
            //Debug.Log(goldCoinDelay2);
            coinRushDelay--;
            Debug.Log(coinRushDelay);

        }
    }

    public void DecCoinCnt()
    {
        Debug.Log("coin --");
        coinCnt--;
    }
    public void DecGoldCoinCnt()
    {
        Debug.Log("Gold coin --");
        goldCoinCnt--;
    }

    private void DecHealthPackCnt()
    {
        Debug.Log("Health pack --");
        healthPackCnt--;
    }
    private void Start()
    {
        StartCoroutine(waitAndGenCoin());
        currentGameTime = GameTime;
    }

    private void Update()
    {
        if (gameRunning)
        {
            currentGameTime -= Time.deltaTime;
        }
        if (currentGameTime < 0)
        {
            currentGameTime = 0;
            gameRunning = false;
        }
        bool canShootLeftTurrets = true;
        for (int i = 0; i < leftTurrets.Length; i++)
        {
            if (leftTurrets[i].GetComponent<Animator>().GetInteger("state") != 3)
            {
                canShootLeftTurrets = false;
            }
        }

        int shouldStartShootingLeftTurrets = Random.Range(0, 2);

        if (canShootLeftTurrets && shouldStartShootingLeftTurrets == 0)
        {
            int chosenLeftTurrets = Random.Range(0, 3);
            Debug.Log("chosen: " + chosenLeftTurrets.ToString());
            int[] chosen = new int[] { };
            if (chosenLeftTurrets == 0)
            {
                chosen = new int[] { 0, 2, 4, 6, 8 };
            }
            else if (chosenLeftTurrets == 1)
            {
                chosen = new int[] { 1, 3, 5, 7 };
            }
            else if (chosenLeftTurrets == 2)
            {
                chosen = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            }

            for (int i = 0; i < chosen.Length; i++)
            {
                int index = chosen[i];
                Debug.Log(index);
                if (leftTurrets[index].GetComponent<Animator>().GetInteger("state") == 3)
                {
                    leftTurrets[index].GetComponent<Animator>().SetInteger("state", 1);
                }
            }
        }

    }


}
