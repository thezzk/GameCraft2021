using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    bool begin(GameObject[] objs);
    IEnumerator running();
    void end();
}
