using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float DestroySelfCountDown = 1.4f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, DestroySelfCountDown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
