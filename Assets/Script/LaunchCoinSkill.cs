﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchCoinSkill : ISkill
{
    public bool begin(GameObject[] objs)
    {
        PlayerControl player = objs[0].GetComponent<PlayerControl>();
        //if(player.coinNum <= 0) return false;
        // player.coinNum -= 1;
        GameObject coinBulletPref = objs[1];
        //0
        //

        List<GameObject> shootPoints = new List<GameObject>();
        if (player.skillLevel == 1)
        {
            shootPoints.Add(objs[2 + 0]);
        }
        else if (player.skillLevel == 2)
        {
            shootPoints.Add(objs[2 + 0]);
            shootPoints.Add(objs[2 + 1]);
            shootPoints.Add(objs[2 + 14]);
        }
        else if (player.skillLevel == 3)
        {
            shootPoints.Add(objs[2 + 0]);
            shootPoints.Add(objs[2 + 1]);
            shootPoints.Add(objs[2 + 2]);
            shootPoints.Add(objs[2 + 13]);
            shootPoints.Add(objs[2 + 14]);
        }
        else if (player.skillLevel == 4)
        {
            shootPoints.Add(objs[2 + 0]);
            shootPoints.Add(objs[2 + 1]);
            shootPoints.Add(objs[2 + 2]);
            shootPoints.Add(objs[2 + 3]);
            shootPoints.Add(objs[2 + 12]);
            shootPoints.Add(objs[2 + 13]);
            shootPoints.Add(objs[2 + 14]);
        }
        else if (player.skillLevel == 5)
        {
            shootPoints.Add(objs[2 + 0]);
            shootPoints.Add(objs[2 + 1]);
            shootPoints.Add(objs[2 + 2]);
            shootPoints.Add(objs[2 + 3]);
            shootPoints.Add(objs[2 + 4]);
            shootPoints.Add(objs[2 + 11]);
            shootPoints.Add(objs[2 + 12]);
            shootPoints.Add(objs[2 + 13]);
            shootPoints.Add(objs[2 + 14]);
        }
        else if (player.skillLevel == 6)
        {
            shootPoints.Add(objs[2 + 0]);
            shootPoints.Add(objs[2 + 1]);
            shootPoints.Add(objs[2 + 2]);
            shootPoints.Add(objs[2 + 3]);
            shootPoints.Add(objs[2 + 4]);
            shootPoints.Add(objs[2 + 5]);
            shootPoints.Add(objs[2 + 10]);
            shootPoints.Add(objs[2 + 11]);
            shootPoints.Add(objs[2 + 12]);
            shootPoints.Add(objs[2 + 13]);
            shootPoints.Add(objs[2 + 14]);
        }
        else if (player.skillLevel == 6)
        {
            shootPoints.Add(objs[2 + 0]);
            shootPoints.Add(objs[2 + 1]);
            shootPoints.Add(objs[2 + 2]);
            shootPoints.Add(objs[2 + 3]);
            shootPoints.Add(objs[2 + 4]);
            shootPoints.Add(objs[2 + 5]);
            shootPoints.Add(objs[2 + 6]);
            shootPoints.Add(objs[2 + 9]);
            shootPoints.Add(objs[2 + 10]);
            shootPoints.Add(objs[2 + 11]);
            shootPoints.Add(objs[2 + 12]);
            shootPoints.Add(objs[2 + 13]);
            shootPoints.Add(objs[2 + 14]);
        }
        else if (player.skillLevel == 7)
        {
            shootPoints.Add(objs[2 + 0]);
            shootPoints.Add(objs[2 + 1]);
            shootPoints.Add(objs[2 + 2]);
            shootPoints.Add(objs[2 + 3]);
            shootPoints.Add(objs[2 + 4]);
            shootPoints.Add(objs[2 + 5]);
            shootPoints.Add(objs[2 + 6]);
            shootPoints.Add(objs[2 + 7]);
            shootPoints.Add(objs[2 + 8]);
            shootPoints.Add(objs[2 + 9]);
            shootPoints.Add(objs[2 + 10]);
            shootPoints.Add(objs[2 + 11]);
            shootPoints.Add(objs[2 + 12]);
            shootPoints.Add(objs[2 + 13]);
            shootPoints.Add(objs[2 + 14]);
        }

        for (int i = 0; i < shootPoints.Count; i++)
        {
            GameObject firePoint = shootPoints[i];
            var bullet = GameObject.Instantiate(coinBulletPref, firePoint.transform.position, firePoint.transform.rotation);
            (bullet.GetComponent<CoinBullet>()).setEmitter(player);
        }
        return true;
    }

    public void end()
    {

    }

    public IEnumerator running()
    {
        throw new System.NotImplementedException();
    }
}
