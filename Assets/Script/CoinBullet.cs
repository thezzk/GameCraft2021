using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoinBullet : MonoBehaviour
{
    [SerializeField] float speed = 50f;
    private float lifeTime = 10f;

    [SerializeField] int damage = 10;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerControl hitPlayer = other.GetComponent<PlayerControl>();
        if (hitPlayer != null)
        {
            hitPlayer.healthBar.TakeDamage(damage);
            Rigidbody _rb = hitPlayer.GetComponent<Rigidbody>();
            // NavMeshAgent _agent = hitPlayer.GetComponent<NavMeshAgent>();
            // _rb.isKinematic = false;
            // _agent.enabled = false;
            _rb.velocity = -transform.forward * 10;

            //hitPlayer.coinNum = (int)(hitPlayer.coinNum * 0.5f);
        }
        Debug.Log(other.name);
        Destroy(gameObject);
    }
}
