using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSkill : ISkill
{
    const float skillTime = 2.5f;
    const float skillDamage = 100f;

    private GameObject myself;
    private GameObject firePos;
    private GameObject laserPref;
    private Ray laserRay;

    private GameObject myLazerEffect;
    public bool begin(GameObject[] objs)
    {
        myself = objs[0];
        firePos = objs[1];
        laserPref = objs[2];
        laserRay = new Ray(firePos.transform.position, firePos.transform.forward);
        myLazerEffect = GameObject.Instantiate(laserPref, firePos.transform.position, Quaternion.identity);
        return true;
    }

    public void end()
    {
        GameObject.Destroy(myLazerEffect);
    }

    public IEnumerator running()
    {
        float startTime = Time.time;
        while (true)
        {
            if (Time.time - startTime > skillTime)
            {
                end();
                yield break;
            }

            laserRay = new Ray(firePos.transform.position, firePos.transform.forward);
            myLazerEffect.transform.position = firePos.transform.position;
            myLazerEffect.transform.rotation = 
                Quaternion.LookRotation(firePos.transform.forward, firePos.transform.up);
            var hits = Physics.RaycastAll(laserRay, 1000f);
            foreach(var hit in hits)
            {
                var player = hit.transform.GetComponent<PlayerControl>();
                if(player != null)
                {
                    player.healthBar.health -= skillDamage * Time.deltaTime;
                }
            }
            yield return null;
        }

    }
}
