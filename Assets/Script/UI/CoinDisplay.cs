using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField] PlayerControl player;

    // Update is called once per frame
    void Update()
    {
        Text coinTxt = GetComponent<Text>();
        coinTxt.text = player.coinNum.ToString();
        
    }
}
