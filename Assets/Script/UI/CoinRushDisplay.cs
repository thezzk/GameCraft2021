using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRushDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setCoinRushFalse()
    {
        GetComponent<Animator>().SetBool("rush", false);
    }
}
