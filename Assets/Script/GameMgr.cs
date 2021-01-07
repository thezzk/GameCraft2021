using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    [SerializeField] GameObject coinPref;
    [SerializeField] Terrain genTerrain;
    [SerializeField] int maxCoin;
    [SerializeField] GameObject coinGenPoints;
    [SerializeField] float GameTime = 50f;   
    [HideInInspector] public bool gameRunning = true;
    [HideInInspector] public float currentGameTime;

    private int coinCnt = 0;
    

    IEnumerator waitAndGenCoin()
    {
        while(gameRunning)
        {
            yield return new WaitForSeconds(3f);
            if(coinCnt < maxCoin)
            {
                int genPointIndex = Random.Range(0, coinGenPoints.transform.childCount);
                Transform genPointTran = coinGenPoints.transform.GetChild(genPointIndex);
                var coin = Instantiate(coinPref, genPointTran.position, Quaternion.identity);
                coin.GetComponent<Coin>().onGainedCoin += DecCoinCnt;
                coinCnt++;
            }

        }
    }

    private void DecCoinCnt()
    {
        //Debug.Log("coin --");
        coinCnt--;
    }

    private void Start() 
    {
        StartCoroutine(waitAndGenCoin());
        currentGameTime = GameTime;
    }

    private void Update() 
    {
        if(gameRunning)
        {
            currentGameTime -= Time.deltaTime;
        }
        if(currentGameTime < 0)
        {
            currentGameTime = 0;
            gameRunning = false;
        }
    }

    
}
