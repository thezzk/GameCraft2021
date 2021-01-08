using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    [SerializeField] GameObject coinPref;
    [SerializeField] Terrain genTerrain;
    [SerializeField] int maxCoin;
    [SerializeField] GameObject coinGenPoints;
    [SerializeField] GameObject healthPackPref;
    [SerializeField] int maxHealthPack;
    [SerializeField] GameObject healthPackGenPoints;
    [SerializeField] float GameTime = 50f;   
    [HideInInspector] public bool gameRunning = true;
    [HideInInspector] public float currentGameTime;

    private int coinCnt = 0;
    private int healthPackCnt = 0;


    IEnumerator waitAndGenCoin()
    {
        while (gameRunning)
        {
            yield return new WaitForSeconds(1f);
            if (coinCnt < maxCoin)
            {
                int genPointIndex = Random.Range(0, coinGenPoints.transform.childCount);
                Transform genPointTran = coinGenPoints.transform.GetChild(genPointIndex);
                var coin = Instantiate(coinPref, genPointTran.position, Quaternion.identity);
                coin.transform.GetChild(0).GetComponent<Coin>().onGainedCoin += DecCoinCnt;
                coinCnt++;
            }
            if (healthPackCnt < maxHealthPack)
            {
                int genPointIndex = Random.Range(0, healthPackGenPoints.transform.childCount);
                Transform genPointTran = healthPackGenPoints.transform.GetChild(genPointIndex);
                var healthPack = Instantiate(healthPackPref, genPointTran.position, Quaternion.identity);
                healthPack.GetComponent<HealthPack>().onGainedHealthPack += DecHealthPackCnt;
                healthPackCnt++;
            }

        }
    }

    public void DecCoinCnt()
    {
        Debug.Log("coin --");
        coinCnt--;
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
    }


}
