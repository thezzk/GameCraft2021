using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    
    [SerializeField] Canvas gamingUI;
    private GameMgr gameMgr;

    private void Start() 
    {
        gameMgr = FindObjectOfType<GameMgr>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            gamingUI.gameObject.SetActive(true);
            gameObject.SetActive(false);
            gameMgr.startGame();
        }
    }
}
