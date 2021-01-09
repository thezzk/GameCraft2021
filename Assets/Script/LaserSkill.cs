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

    private LineRenderer myLaserEffect;
    public bool begin(GameObject[] objs)
    {
        myself = objs[0];
        firePos = objs[1];
        laserPref = objs[2];
        laserRay = new Ray(firePos.transform.position, firePos.transform.forward);
        myLaserEffect = (GameObject.Instantiate
            (laserPref, firePos.transform.position, Quaternion.identity)).GetComponent<LineRenderer>();
        return true;
    }

    public void end()
    {
        GameObject.Destroy(myLaserEffect);
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
            myLaserEffect.transform.position = firePos.transform.position;
            myLaserEffect.transform.rotation = 
                Quaternion.LookRotation(firePos.transform.forward, firePos.transform.up);
            var hits = Physics.RaycastAll(laserRay, 1000f);
            RaycastHit nearestShieldHit = default(RaycastHit);
            nearestShieldHit.distance = float.MaxValue;
            foreach(var hit in hits)
            {   
                if(hit.collider.transform.tag == "Shield" && hit.distance < nearestShieldHit.distance)
                {
                    //Debug.Log("found shield");
                    nearestShieldHit = hit;
                }
            }
            if(nearestShieldHit.distance == float.MaxValue)
            {
                myLaserEffect.SetPosition(1, new Vector3(0, 0, 100f));
            }
            else
            {
                hits = Physics.RaycastAll(laserRay, nearestShieldHit.distance - 10f);
                myLaserEffect.SetPosition(1, new Vector3(0, 0, nearestShieldHit.distance));
            }
            foreach (var hit in hits)
            {
                var player = hit.transform.GetComponent<PlayerControl>();
                if (player != null)
                {
                    player.healthBar.health -= skillDamage * Time.deltaTime;
                }
            }
            yield return null;
        }

    }
}
