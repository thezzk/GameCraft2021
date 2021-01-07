using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CntDownDisplay : MonoBehaviour
{
   

    // Update is called once per frame
    void Update()
    {
        Text cntDownTxt = GetComponent<Text>();
        cntDownTxt.text = string.Format("{0:0.0}", FindObjectOfType<GameMgr>().currentGameTime); 
    }
}
