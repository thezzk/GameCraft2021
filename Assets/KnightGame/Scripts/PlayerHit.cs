using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    //define the animator component in Knight Game Object
    private Animator knightAnimator;
    bool shield = false;
    private float shieldDuration = 5.5f;
    public GameObject shieldEffect;
    public Animator block;

    //reference the Pet Chicken prebfab
    public GameObject Pet;

    //reference audio clips for pplayer
    public AudioClip playerDied;
    public AudioClip collectGift;

    //audio source to play the sounds
    private AudioSource audioSourcePlayer;

    //define the game manager script in GameManager
    private GameManagerScript getGameManager;

    // Start is called before the first frame update
    void Start()
    {
        getGameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        audioSourcePlayer = GetComponent<AudioSource>();
        //find the gameobject Mod_Knight_New, and get the animator component inside it
        knightAnimator = GameObject.Find("Hero_01_Knight/Mod_Knight_New").GetComponent<Animator>();
        block = GameObject.Find("Block_A_32").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DelayDeath()
    {
        //tell game manager script to run IsGameOver method
        getGameManager.IsGameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if object is tagged as GiftShield
        if (other.gameObject.tag == "GiftShield") //Take not g is in small caps, as compared to above!
       {
            //change audio clip to gift clip
            audioSourcePlayer.clip = collectGift;
            //play the audio clip
            audioSourcePlayer.Play(0);
            //destroy gift box
            Destroy(other.gameObject);
            //set active the shield
            shieldEffect.SetActive(true);
            //turn on the shield
            shield = true; //dont put bool in front!
            print("Shield is true");
            StartCoroutine("ShieldRoutine");

            
        }
        if (other.gameObject.tag == "GiftPet")
        {
            //change audio clip to gift clip
            audioSourcePlayer.clip = collectGift;
            //play the audio clip
            audioSourcePlayer.Play(0);
            //destroy the gift box
            Destroy(other.gameObject);

            //spawn pet
            Instantiate(Pet, transform.position, transform.rotation);
        }
        if (other.gameObject.tag == "Enemy" && shield == true) //Take not g is in small caps, as compared to above!
        {
            //get the script component in the other object
            other.GetComponent<EnemyHP>().MinusHp(other.GetComponent<EnemyHP>().health); // Get the EnemyHP script, then call the script's function Minushp with the argument damage
            //call the script with my "damage"
            //Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Enemy" ||other.gameObject.tag == "Boss") 
        {
            //Call the delaydeath function 1 seconds later
            Invoke("DelayDeath",1); 
            //change audio clip to playerDied
            audioSourcePlayer.clip = playerDied;
            //play the audio clip
            audioSourcePlayer.Play(0);
            //play Die animation
            knightAnimator.SetTrigger("Die");
            
        }

        if (other.gameObject.tag == "Key")
        {
            block.SetTrigger("Open");
            Destroy(other.gameObject);
            print("collected key");
        }
    }

     IEnumerator ShieldRoutine()
    {
        yield return new WaitForSeconds(shieldDuration);
        shieldEffect.SetActive(false);
        shield = false;
        print("Shield is off");
    }
}
