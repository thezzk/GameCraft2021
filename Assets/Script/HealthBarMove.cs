using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarMove : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
        Vector3 newPos = player.position;
        newPos.x += 1.5f;
        newPos.z += 2.6f;

        transform.position = newPos;
    }
}
