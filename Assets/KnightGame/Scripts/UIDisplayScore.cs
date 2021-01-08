using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //So you can use Text
public class UIDisplayScore : MonoBehaviour
{
    private Text scoreText;

   // private UIManagerScript getUIManager;
    // Start is called before the first frame update
    void Start()
    {
        //get the text component on the game object
        scoreText = GetComponent<Text>();

        //getUIManager = GameObject.Find("UIManager").GetComponent<UIManagerScript>();

        //scoreText.text = "Score: " + getUIManager.playerScore; All these are not needed since we created an instance of UIManagerscript, so we can just use it directly
        scoreText.text = "Score: " + UIManagerScript.instance.playerScore;

    }

    // Update is called once per frame
    void Update()
    {
        //get the playerscore from UIManager, and show it on text component
        //by right should use invoke method to update every 0.5 seconds, as update() is called 60 times in one second
        //scoreText.text = "Score: " + getUIManager.playerScore;
        scoreText.text = "Score: " + UIManagerScript.instance.playerScore;
    }
}
