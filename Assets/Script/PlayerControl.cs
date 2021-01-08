using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] public Healthbar healthBar;
    [SerializeField] string horizontalAxisName;
    [SerializeField] string verticalAxisName;
    [SerializeField] KeyCode waveBtnKeyCode;
    [SerializeField] KeyCode fireBtnKeyCode;
    [SerializeField] KeyCode rushBtnKeyCode;
    public float waveColdTime;
    public float fireColdTime;
    public float rushColdTime;

    [HideInInspector] public float waveColdDown = 0;
    [HideInInspector] public float fireColdDown = 0;
    [HideInInspector] public float rushColdDown = 0;



    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject coinBullet;
    [SerializeField] GameObject waveEffect;


    public int coinNum;
    [HideInInspector] public float resetTime = 0f;

    Vector3 movement;
    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<GameMgr>().gameRunning) return;

        if (resetTime > Mathf.Epsilon)
        {
            if (GetComponent<Animation>().clip == null)
            {
                GetComponent<Animation>().Play("PlayerBlink");
            }
            resetTime -= Time.deltaTime;
            return;
        }

        if (GetComponent<Animation>().clip != null)
        {
            GetComponent<Animation>().clip = null;
        }
        float horizontalInput = -Input.GetAxis(verticalAxisName);
        float verticalInput = Input.GetAxis(horizontalAxisName);

        movement.Set(horizontalInput, 0f, verticalInput);

        agent.Move(movement * Time.deltaTime * agent.speed);
        agent.SetDestination(transform.position + movement);

        if (Input.GetKeyDown(waveBtnKeyCode) && waveColdDown < Mathf.Epsilon)
        {
            Wave();
            waveColdDown = waveColdTime;
        }
        if (Input.GetKeyDown(fireBtnKeyCode) && fireColdDown < Mathf.Epsilon)
        {
            if (Fire())
                fireColdDown = fireColdTime;
        }
        if (Input.GetKeyDown(rushBtnKeyCode) && rushColdDown < Mathf.Epsilon)
        {
            Rush();
            rushColdDown = rushColdTime;
        }


        if (healthBar.health < 1f)
        {
            healthBar.SetHealth(100f);
            coinNum = (int)(coinNum * 0.5f);
            GetComponent<NavMeshAgent>().Warp(new Vector3(24f, gameObject.transform.position.y, 9.3f));
            resetTime = 3f;
        }


        // if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
        // {
        //     transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        // }
        UpdateColdDown();

    }

    void UpdateColdDown()
    {
        rushColdDown -= Time.deltaTime;
        rushColdDown = Mathf.Max(0, rushColdDown);
        waveColdDown -= Time.deltaTime;
        waveColdDown = Mathf.Max(0, waveColdDown);
        fireColdDown -= Time.deltaTime;
        fireColdDown = Mathf.Max(0, fireColdDown);
    }

    void Rush()
    {
        GameObject[] objArray = { gameObject };
        var skill = new RushSkill();
        skill.begin(objArray);
        StartCoroutine(skill.running());

    }
    void Wave()
    {
        GameObject[] objArray = { gameObject, waveEffect };
        var skill = new WaveSkill();
        skill.begin(objArray);
        StartCoroutine(skill.running());

    }

    bool Fire()
    {
        GameObject[] objArray = { gameObject, coinBullet, firePoint };
        var skill = new LaunchCoinSkill();
        bool retval = skill.begin(objArray);
        skill.end();
        return retval;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(this.transform.position, WaveSkill.range);
    }

    public void gainCoin()
    {
        coinNum += 1;
    }

}
