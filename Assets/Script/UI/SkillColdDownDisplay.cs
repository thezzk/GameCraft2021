using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillColdDownDisplay : MonoBehaviour
{
    [SerializeField] PlayerControl player;
    [SerializeField] Image laserSkillColdDown;
    [SerializeField] Image rushSkillColdDown;
    [SerializeField] Image waveSkillColdDown;

    private void Update() 
    {
        laserSkillColdDown.fillAmount = 1 - player.laserColdDown / player.laserColdTime;
        rushSkillColdDown.fillAmount = 1 - player.rushColdDown / player.rushColdTime;
        waveSkillColdDown.fillAmount = 1 - player.waveColdDown / player.waveColdTime;    
    }
}
