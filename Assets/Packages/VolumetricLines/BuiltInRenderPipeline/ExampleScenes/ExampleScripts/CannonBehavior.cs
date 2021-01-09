﻿using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour
{

    public Transform m_cannonRot;
    public Transform m_muzzle;
    public GameObject m_shotPrefab;
    public Texture2D m_guiTexture;

    private bool canShoot;

    private int bulletsLeft;


    // Use this for initialization
    void Start()
    {
        canShoot = false;
        bulletsLeft = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void startShooting()
    {
        bulletsLeft = 3;
        StartCoroutine(shootCoroutine());
    }

    public void enableLeftTurrets()
    {
        GetComponent<Animator>().SetInteger("state", 3);
    }

    IEnumerator shootCoroutine()
    {
        while (bulletsLeft > 0)
        {
            bulletsLeft--;
            GameObject go = GameObject.Instantiate(m_shotPrefab, m_muzzle.position, m_muzzle.rotation) as GameObject;
            GameObject.Destroy(go, 3f);
            yield return new WaitForSeconds(1f);
        }

        if (bulletsLeft <= 0)
        {
            GetComponent<Animator>().SetInteger("state", 2);
        }
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0f, 0f, m_guiTexture.width / 2, m_guiTexture.height / 2), m_guiTexture);
    }
}