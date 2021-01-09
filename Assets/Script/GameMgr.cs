using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameState
{
    Prepare,
    Running,
    End
}

public class GameMgr : MonoBehaviour
{
    [SerializeField] GameObject coinRushLabel;
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

    [SerializeField] GameObject gamingUI;
    [SerializeField] GameObject endingUI;

    [HideInInspector] public bool gameRunning = false;
    [HideInInspector] public float currentGameTime;

    private int coinCnt = 0;
    private int goldCoinCnt = 0;
    public int goldCoinDelay2 = 10;
    public int coinRushDelay;
    private int healthPackCnt = 0;

    private Coroutine spawnCoinCoroutine;

    [HideInInspector] GameState gameState;

    IEnumerator waitAndGenCoin()
    {
        coinRushDelay = Random.Range(10, 100);
        while (gameRunning)
        {
            yield return new WaitForSeconds(1f);
            if (coinRushDelay == 0)
            {
                coinRushLabel.GetComponent<Animator>().SetBool("rush", true);
                int i = 0;
                while (i < 30)
                {
                    int genPointIndex = Random.Range(0, coinGenPoints.transform.childCount);
                    Vector3 genPointTranPos = coinGenPoints.transform.GetChild(genPointIndex).position;
                    genPointTranPos += new Vector3(Random.Range(0.05f, 0.05f), 0f, Random.Range(0.05f, 0.05f));
                    var coin = Instantiate(coinPref, genPointTranPos, Quaternion.identity);
                    coin.transform.GetChild(0).GetComponent<Coin>().onGainedCoin += DecCoinCnt;
                    coinCnt++;
                    i++;
                }

            }
            else if (coinCnt < maxCoin)
            {

                int genPointIndex = Random.Range(0, coinGenPoints.transform.childCount);
                Vector3 genPointTranPos = coinGenPoints.transform.GetChild(genPointIndex).position;
                genPointTranPos += new Vector3(Random.Range(0.05f, 0.05f), 0f, Random.Range(0.05f, 0.05f));
                var coin = Instantiate(coinPref, genPointTranPos, Quaternion.identity);
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
            if (goldCoinDelay2 == 0)
            {
                goldCoinDelay2 = 10;
            }
            if (coinRushDelay == 0)
            {
                coinRushDelay = Random.Range(10, 100);
            }

            if (healthPackCnt < maxHealthPack)
            {
                int genPointIndex = Random.Range(0, healthPackGenPoints.transform.childCount);
                Vector3 genPointTranPos = healthPackGenPoints.transform.GetChild(genPointIndex).position;
                genPointTranPos += new Vector3(Random.Range(0.05f, 0.05f), 0f, Random.Range(0.05f, 0.05f));
                var healthPack = Instantiate(healthPackPref, genPointTranPos, Quaternion.identity);
                healthPack.GetComponent<HealthPack>().onGainedHealthPack += DecHealthPackCnt;
                healthPackCnt++;
            }
            goldCoinDelay2--;
            //Debug.Log(goldCoinDelay2);
            coinRushDelay--;

        }
    }

    public void DecCoinCnt()
    {
        coinCnt--;
    }
    public void DecGoldCoinCnt()
    {
        goldCoinCnt--;
    }

    private void DecHealthPackCnt()
    {
        healthPackCnt--;
    }

    public void startGame()
    {
        gameRunning = true;
        gameState = GameState.Running;
        spawnCoinCoroutine = StartCoroutine(waitAndGenCoin());
    }

    private void Awake() 
    {
        gameRunning  =false;
        gameState = GameState.Prepare;    
    }

    private void Start()
    {
        coinRushDelay = Random.Range(10, 100); ;
        currentGameTime = GameTime;

    }

    private void Update()
    {
        if (gameRunning)
        {
            currentGameTime -= Time.deltaTime;
        }
        else
        {
            if(spawnCoinCoroutine != null)
                StopCoroutine(spawnCoinCoroutine);    
            return;
        }

        if (currentGameTime < 0)
        {
            currentGameTime = 0;
            gameRunning = false;
            if(gameState == GameState.Running)
            {
                gameState = GameState.End;
                gamingUI.SetActive(false);
                endingUI.SetActive(true);
                endingUI.GetComponent<GameEndingUI>().UpdateRoundResult();
            }

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
                if (leftTurrets[index].GetComponent<Animator>().GetInteger("state") == 3)
                {
                    leftTurrets[index].GetComponent<Animator>().SetInteger("state", 1);
                }
            }
        }

    }


}
