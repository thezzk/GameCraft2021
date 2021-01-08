using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] public Healthbar healthBar;
    [SerializeField] public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Heath: " + Mathf.RoundToInt(healthBar.health).ToString();
    }
}
