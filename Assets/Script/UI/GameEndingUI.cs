using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndingUI : MonoBehaviour
{
    [SerializeField] GameObject drawLabel;
    [SerializeField] GameObject redWinLabel;
    [SerializeField] GameObject blueWinLabel;
    [SerializeField] PlayerControl redPlayer;
    [SerializeField] PlayerControl bluePlayer;

    void Update()
    {
         //TODO, if any key, reload scene
    }

    public void UpdateRoundResult()
    {
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
    }
}
