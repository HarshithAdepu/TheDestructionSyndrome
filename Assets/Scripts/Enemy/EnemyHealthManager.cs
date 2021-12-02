using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField] Text healthText;
    [SerializeField] readonly float DEFAULT_HEALTH = 100f;

    float maxHealth, currentHealth;

    void OnEnable()
    {
        currentHealth = maxHealth;
        healthText.text = "Health: " + currentHealth;
    }
    void Start()
    {
        maxHealth = DEFAULT_HEALTH;
        currentHealth = maxHealth;
        healthText.text = "Health: " + currentHealth;
    }

    public void Damage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth = currentHealth - damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            EnemyDeath();
        }
        healthText.text = "Health: " + currentHealth;
    }


    void EnemyDeath()
    {
        Debug.Log("Enemy Died!");
        EnemySpawner.enemySpawnerInstance.EnemyDied(this.gameObject);
        gameObject.SetActive(false);
    }
}
