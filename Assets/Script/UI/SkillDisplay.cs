using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillDisplay : MonoBehaviour
{
    [SerializeField] PlayerControl player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI skillTxt = GetComponent<TextMeshProUGUI>();
        skillTxt.text = "Skill Level: " + player.skillLevel.ToString() + "/7";

    }
}
