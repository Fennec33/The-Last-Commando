using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private PlayerController player;

    private void Update()
    {
        healthText.text = "Health: " + player.Health.ToString();
    }
}

