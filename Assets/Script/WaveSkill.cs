using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveSkill : ISkill
{
    GameObject myself;
    float skillTime = 1f;
    private float speed = 5f;
    public const float range = 4f;
    GameObject visualEffect;

    public bool begin(GameObject[] objs)
    {
        myself = objs[0];
        var visualEffectPref = objs[1];
        visualEffect = GameObject.Instantiate(visualEffectPref, myself.transform);
        visualEffect.transform.localScale = new Vector3(range, visualEffect.transform.localScale.y, range);  
        return true;
    }

    public void end()
    {
        GameObject.Destroy(visualEffect);
    }

    public IEnumerator running()
    {
        PlayerControl[] otherplayer = Transform.FindObjectsOfType<PlayerControl>();
        float startTime = Time.time;


        while(true)
        {
            if (Time.time - startTime > skillTime)
            {
                end();
                yield break;
            }

            foreach(var player in otherplayer)
            {
                if(player.gameObject != myself)
                {
                    Vector3 dirToOtherPlayer = player.transform.position  - myself.transform.position;
                    if(dirToOtherPlayer.magnitude > range) continue;
                    var moveVec = dirToOtherPlayer.normalized * (Mathf.Sqrt(range * range) - dirToOtherPlayer.magnitude);
                    Debug.Log(moveVec.magnitude);
                    player.GetComponent<NavMeshAgent>().Move(moveVec * speed * Time.deltaTime);
                }
            }
            yield return null;
        }
    }
}
