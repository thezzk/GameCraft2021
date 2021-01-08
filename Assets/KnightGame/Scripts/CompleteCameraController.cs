using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour {

    public GameObject player;        //Public variable to store a reference to the player game object


    private Vector3 offset;            //Private variable to store the offset distance between the player and camera

    private bool shaking = false;
    // Use this for initialization
    void Start () 
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate () 
    {
        if (shaking == false)
        {
            // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
            transform.position = player.transform.position + offset;
        }
    }
    public void ShakeCamera()
    {
        shaking = true;
        StartCoroutine("ShakeRoutine");
    }

    IEnumerator ShakeRoutine()
    {
        //get the camera position, and record it down
        Vector3 originalPosition = transform.position;

        float elapsed = 0f;

        while (elapsed < 0.2f)
        {
            //change the camera position offset by 0.3
            float x = Random.Range(-1f, 1f) * 0.3f;

            float y = Random.Range(-1f, 1f) * 0.3f;

            transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
            elapsed += Time.deltaTime;
            yield return 0;
        }

        shaking = false;
        transform.position = originalPosition;
    }
}