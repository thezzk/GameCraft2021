using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CntDownDisplay : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI cntDownTxt = GetComponent<TextMeshProUGUI>();
        cntDownTxt.text = "Time left: " + string.Format("{0:0.0}", FindObjectOfType<GameMgr>().currentGameTime);
    }
}
