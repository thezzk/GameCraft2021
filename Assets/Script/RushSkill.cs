using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RushSkill : ISkill
{
    GameObject myself;
    float skillTime = 0.5f;
    private float speed = 8f;
    public bool begin(GameObject[] objs)
    {
        myself = objs[0];
        return true;
    }

    public void end()
    {
        
    }

    public IEnumerator running()
    {
        float startTime = Time.time;
        Vector3 rushDir = myself.transform.TransformDirection(Vector3.forward * speed * Time.deltaTime);
        while(true)
        {
            if(Time.time - startTime > skillTime)
            {
                end();
                yield break;
            }

            myself.GetComponent<NavMeshAgent>().Move(rushDir);
            yield return null;
        }
    }

}
