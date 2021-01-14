using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text healthText;

    private void Start()
    {
        PlayerController.healthUpdate += UpdateHealth;
    }

    public void UpdateHealth(int health)
    {
        healthText.text = health.ToString();
    }
}
