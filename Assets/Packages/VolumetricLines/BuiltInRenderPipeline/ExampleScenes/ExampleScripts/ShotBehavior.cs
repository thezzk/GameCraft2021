using UnityEngine;
using System.Collections;

public class ShotBehavior : MonoBehaviour
{

    [SerializeField] int damage = 50;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 20f;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shield")
        {
            Destroy(this.gameObject);
            return;
        }

        PlayerControl hitPlayer = other.GetComponent<PlayerControl>();
        if (hitPlayer != null)
        {
            hitPlayer.playSound("hurtSound");
            hitPlayer.healthBar.TakeDamage(damage);
            Rigidbody _rb = hitPlayer.GetComponent<Rigidbody>();
            // NavMeshAgent _agent = hitPlayer.GetComponent<NavMeshAgent>();
            // _rb.isKinematic = false;
            // _agent.enabled = false;
            _rb.velocity = -transform.forward * 10;

            //hitPlayer.coinNum = (int)(hitPlayer.coinNum * 0.5f);
            Destroy(gameObject);
        }
        //Debug.Log(other.name);
        //Destroy(gameObject);
        //Destroy desructible walls
        DestructibleWall hitDestructibleWall = other.GetComponent<DestructibleWall>();
        if (hitDestructibleWall != null)
        {
            hitDestructibleWall.decreaseHealth();
        }

    }
}
