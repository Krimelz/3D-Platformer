using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text healthText;
    public Text manaText;

    private void Start()
    {
        PlayerController.healthUpdate += UpdateHealth;
        PlayerController.manaUpdate += UpdateMana;
    }

    private void UpdateHealth(int health)
    {
        healthText.text = health.ToString();
    }

    private void UpdateMana(int mana)
    {
        manaText.text = mana.ToString();
    }
}
