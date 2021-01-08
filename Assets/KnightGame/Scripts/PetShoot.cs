using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetShoot : MonoBehaviour
{
    // define/reference the bullet game object
    public GameObject bullet;
    // define bullet shooting interval
    private float bulletInterval = 0.3f;

    //spawn bullet object

    //at every shooting interval
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("BulletRoutine");
    }
    //cannot start IEnumerator with void as yield needs to return some information
    IEnumerator BulletRoutine()
    {
        while (true)
        {
            //spawn bullet object
            //at every shooting interval
            Instantiate(bullet, transform.position, transform.rotation); //Instantiate bullets at the chicken position
            yield return new WaitForSeconds(bulletInterval); //pause for 0.3 seconds
        }
    }
}
