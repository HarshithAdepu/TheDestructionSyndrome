using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] Text healthText;
    [SerializeField] readonly int DEFAULT_HEALTH = 100;
    [SerializeField] readonly int HEALTH_INCREASE_PER_UPGRADE = 25;

    int maxHealth;
    int currentHealth;

    void Start()
    {
        maxHealth = DEFAULT_HEALTH;
        currentHealth = maxHealth;
        healthText.text = "Health: " + currentHealth;
    }

    public void Damage(int damage, InputAction.CallbackContext context)
    {
        if (currentHealth <= 0) return;

        currentHealth = currentHealth - damage;
        if (currentHealth <= 0)
            PlayerDeath();
        healthText.text = "Health: " + currentHealth;
    }

    public void Heal(int health, InputAction.CallbackContext context)
    {
        currentHealth = currentHealth + health;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        healthText.text = "Health: " + currentHealth;
    }

    public void HealthUpgrade()
    {
        maxHealth = maxHealth + HEALTH_INCREASE_PER_UPGRADE;
        currentHealth = maxHealth;
        healthText.text = "Health: " + currentHealth;
    }
    void PlayerDeath()
    {
        Debug.Log("Player Died!");
    }
}
