using System.Collections;
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
        GameObject firePoint = objs[2];
        GameObject.Instantiate(coinBulletPref, firePoint.transform.position, firePoint.transform.rotation);
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
