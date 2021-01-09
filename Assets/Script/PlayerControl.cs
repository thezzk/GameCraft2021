using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] AudioClip itemSound;
    [SerializeField] AudioClip healthSound;
    [SerializeField] AudioClip gunSound;
    [SerializeField] AudioClip hurtSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip waveSound;
    [SerializeField] AudioClip rushSound;


    [SerializeField] public Healthbar healthBar;
    [SerializeField] string horizontalAxisName;
    [SerializeField] string verticalAxisName;
    [SerializeField] KeyCode waveBtnKeyCode;
    [SerializeField] KeyCode fireBtnKeyCode;
    [SerializeField] KeyCode rushBtnKeyCode;
    [SerializeField] KeyCode laserBtnKeyCode;
    [SerializeField] KeyCode upgradeBtnKeyCode;
    public float waveColdTime;
    public float fireColdTime;
    public float rushColdTime;
    public float laserColdTime;

    [HideInInspector] public float waveColdDown = 0;
    [HideInInspector] public float fireColdDown = 0;
    [HideInInspector] public float rushColdDown = 0;
    [HideInInspector] public float laserColdDown = 0;


    [SerializeField] GameObject[] firePoints;
    [SerializeField] GameObject coinBullet;
    [SerializeField] GameObject waveEffect;

    [SerializeField] GameObject coinPref;
    [SerializeField] GameObject laserPref;

    [SerializeField] GameObject GameManager;
    [SerializeField] GameObject coinGenPoints;

    public int coinNum;
    [HideInInspector] public float resetTime = 0f;

    [HideInInspector] public int skillLevel = 1;

    Vector3 movement;
    NavMeshAgent agent;
    AudioSource audioSource;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        GameManager = GameObject.FindGameObjectWithTag("Manager");
        audioSource = GetComponent<AudioSource>();
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

        float horizontalInput = Input.GetAxis(horizontalAxisName);
        float verticalInput = Input.GetAxis(verticalAxisName);

        movement.Set(horizontalInput, 0f, verticalInput);
        agent.Move(movement * Time.deltaTime * agent.speed);
        agent.SetDestination(transform.position + movement);
        rotateThePlayer(movement);

        if (Input.GetKeyDown(waveBtnKeyCode) && waveColdDown < Mathf.Epsilon)
        {
            Wave(1f);
            playSound("waveSound");
            waveColdDown = waveColdTime;
        }
        if (Input.GetKeyDown(fireBtnKeyCode) && fireColdDown < Mathf.Epsilon)
        {
            if (Fire())
            {
                playSound("gunSound");
                fireColdDown = fireColdTime;
            }
        }
        if (Input.GetKeyDown(rushBtnKeyCode) && rushColdDown < Mathf.Epsilon)
        {
            Rush();
            playSound("rushSound");
            rushColdDown = rushColdTime;
        }
        if (Input.GetKeyDown(laserBtnKeyCode) && laserColdDown < Mathf.Epsilon)
        {
            Laser();
            laserColdDown = laserColdTime;

        }
        if (Input.GetKeyDown(upgradeBtnKeyCode))
        {
            Upgrade();
        }



        if (healthBar.health < 1f)
        {
            playSound("deathSound");
            int coinsLost = Mathf.FloorToInt(coinNum * 0.5f);
            for (int i = 0; i < coinsLost; i++)
            {
                int genPointIndex = Random.Range(0, coinGenPoints.transform.childCount);
                Vector3 genPointTranPos = coinGenPoints.transform.GetChild(genPointIndex).position;
                genPointTranPos += new Vector3(Random.Range(0.05f, 0.05f), 0f, Random.Range(0.05f, 0.05f));
                var coin = Instantiate(coinPref, genPointTranPos, Quaternion.identity);
                coin.transform.GetChild(0).GetComponent<Coin>().onGainedCoin += GameManager.GetComponent<GameMgr>().DecCoinCnt;
                //coinCnt++;

                //var coin = Instantiate(coinPref, transform.position + new Vector3(Random.Range(0, 3f), 0f, Random.Range(0, 3f)), Quaternion.identity);
                //coin.transform.GetChild(0).GetComponent<Coin>().onGainedCoin += GameManager.GetComponent<GameMgr>().DecCoinCnt;
            }

            healthBar.GainHealth(100f);
            coinNum -= coinsLost;
            GetComponent<NavMeshAgent>().Warp(new Vector3(104.12f, -104.4f, 82.32f));
            resetTime = 3f;
            Wave(resetTime + 2f);
        }
        UpdateColdDown();

    }

    private void rotateThePlayer(Vector3 movingDir)
    {
        if (movingDir.magnitude > Mathf.Epsilon)
            transform.rotation = Quaternion.LookRotation(movingDir);
    }

    public void playSound(string sound)
    {
        if (sound == "itemSound")
        {
            audioSource.PlayOneShot(itemSound);
        }
        else if (sound == "healthSound")
        {
            audioSource.PlayOneShot(healthSound);
        }
        else if (sound == "gunSound")
        {
            audioSource.PlayOneShot(gunSound);
        }
        else if (sound == "hurtSound")
        {
            audioSource.PlayOneShot(hurtSound);
        }
        else if (sound == "deathSound")
        {
            audioSource.PlayOneShot(deathSound);
        }
        else if (sound == "waveSound")
        {
            audioSource.PlayOneShot(waveSound);
        }
        else if (sound == "rushSound")
        {
            audioSource.PlayOneShot(rushSound);
        }
    }

    void UpdateColdDown()
    {
        rushColdDown -= Time.deltaTime;
        rushColdDown = Mathf.Max(0, rushColdDown);
        waveColdDown -= Time.deltaTime;
        waveColdDown = Mathf.Max(0, waveColdDown);
        fireColdDown -= Time.deltaTime;
        fireColdDown = Mathf.Max(0, fireColdDown);
        laserColdDown -= Time.deltaTime;
        laserColdDown = Mathf.Max(0, laserColdDown);
    }

    void Upgrade()
    {
        if (coinNum >= 15)
        {
            coinNum -= 15;
            skillLevel++;
        }
    }

    void Laser()
    {
        GameObject[] objArray = { gameObject, firePoints[0], laserPref };
        var skill = new LaserSkill();
        skill.begin(objArray);
        StartCoroutine(skill.running());
    }

    void Rush()
    {
        GameObject[] objArray = { gameObject };
        var skill = new RushSkill();
        skill.begin(objArray);
        StartCoroutine(skill.running());

    }
    void Wave(float skillTime)
    {
        GameObject[] objArray = { gameObject, waveEffect };
        var skill = new WaveSkill();
        skill.skillTime = skillTime;
        skill.begin(objArray);
        StartCoroutine(skill.running());

    }

    bool Fire()
    {
        GameObject[] objArray = { gameObject, coinBullet };
        GameObject[] finalArray = objArray.Concat(firePoints).ToArray();
        var skill = new LaunchCoinSkill();
        bool retval = skill.begin(finalArray);
        skill.end();
        return retval;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(this.transform.position, WaveSkill.range);
    }

    public void gainCoin()
    {
        playSound("itemSound");
        coinNum += 1;
    }
    public void gainGoldCoin()
    {
        playSound("itemSound");
        coinNum += 10;
    }
    public void gainHealthPack()
    {
        playSound("healthSound");
        healthBar.SetHealth(healthBar.health + 30);
        if (healthBar.health > 100f)
        {
            healthBar.SetHealth(100f);
        }
    }

}
