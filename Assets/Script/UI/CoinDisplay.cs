using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField] PlayerControl player;

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI coinTxt = GetComponent<TextMeshProUGUI>();
        coinTxt.text = "Coin: " + player.coinNum.ToString();

    }
}
