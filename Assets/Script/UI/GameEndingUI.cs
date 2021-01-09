using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameEndingUI : MonoBehaviour
{
    [SerializeField] GameObject drawLabel;
    [SerializeField] GameObject redWinLabel;
    [SerializeField] GameObject blueWinLabel;
    [SerializeField] PlayerControl redPlayer;
    [SerializeField] PlayerControl bluePlayer;
    [SerializeField] GameObject redScoreTxt;
    [SerializeField] GameObject blueScoreTxt;
    [SerializeField] GameObject byALotLabel;
    [SerializeField] int byALotThreshould = 50;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void UpdateRoundResult()
    {
        redScoreTxt.GetComponent<Text>().text = redPlayer.coinNum.ToString();
        blueScoreTxt.GetComponent<Text>().text = bluePlayer.coinNum.ToString();

        if(redPlayer.coinNum > bluePlayer.coinNum)
        {
            drawLabel.SetActive(false);
            redWinLabel.SetActive(true);
            blueWinLabel.SetActive(false);
        }
        else if(bluePlayer.coinNum > redPlayer.coinNum)
        {
            drawLabel.SetActive(false);
            redWinLabel.SetActive(false);
            blueWinLabel.SetActive(true);
        }
        else
        {
            drawLabel.SetActive(true);
            redWinLabel.SetActive(false);
            blueWinLabel.SetActive(false);
        }
        if(Mathf.Abs(redPlayer.coinNum - bluePlayer.coinNum) >= 50)
        {
            byALotLabel.SetActive(true);
        }
        else
        {
            byALotLabel.SetActive(false);
        }

    }
}
