using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager playerHealthManagerInstance;
    readonly int DEFAULT_HEALTH = 100;
    readonly int HEALTH_INCREASE_PER_UPGRADE = 25;
    readonly int DEFAULT_ARMOR = 100;
    readonly int ARMOR_INCREASE_PER_UPGRADE = 25;
    [SerializeField] Image healthImage, armorImage;
    [SerializeField] int armorDamageReductionPercent;
    [SerializeField] float invincibilityAfterAttacDuration;
    bool isInvincible;

    int maxHealth, currentHealth, maxArmor, currentArmor;


    void Start()
    {

        maxHealth = DEFAULT_HEALTH;
        maxArmor = DEFAULT_ARMOR;

        currentHealth = maxHealth;
        currentArmor = maxArmor;

        UpdateStats();
    }

    public void Damage(int damage)
    {
        if (isInvincible)
            return;
        isInvincible = true;
        Invoke("ResetInvincibility", invincibilityAfterAttacDuration);
        damage = damage * (100 - armorDamageReductionPercent) / 100;
        if (currentArmor > damage)
            currentArmor -= damage;
        else
        {
            damage -= currentArmor;
            damage = (100 * damage) / (100 - armorDamageReductionPercent);
            currentArmor = 0;
            currentHealth -= damage;
        }
        if (currentHealth < 0)
            currentHealth = 0;

        if (currentHealth <= 0)
            PlayerDeath();

        UpdateStats();
    }

    public bool Heal(int health)
    {
        if (currentHealth == maxArmor)
            return false;

        currentHealth = currentHealth + health;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        UpdateStats();
        return true;
    }

    public bool ArmorUp(int armor)
    {
        if (currentArmor == maxArmor)
            return false;
        currentArmor = currentArmor + armor;
        if (currentArmor > maxArmor)
            currentArmor = maxArmor;
        UpdateStats();
        return true;
    }

    public void HealthUpgrade()
    {
        maxHealth = maxHealth + HEALTH_INCREASE_PER_UPGRADE;
        currentHealth = maxHealth;
        UpdateStats();
    }
    void PlayerDeath()
    {
        Debug.Log("Player Died!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateStats()
    {
        healthImage.fillAmount = (float)currentHealth / maxHealth;
        armorImage.fillAmount = (float)currentArmor / maxArmor;
    }

    void ResetInvincibility()
    {
        isInvincible = false;
    }
    void UpgradeHealth()
    {
        maxHealth = maxHealth + HEALTH_INCREASE_PER_UPGRADE;
        currentHealth = maxHealth;
        UpdateStats();
    }
    void UpgradeArmor()
    {
        maxArmor = maxArmor + ARMOR_INCREASE_PER_UPGRADE;
        currentArmor = maxArmor;
        UpdateStats();
    }

    public Vector2Int GetHealth()
    {
        return new Vector2Int(currentHealth, maxHealth);
    }
    public Vector2Int GetArmor()
    {
        return new Vector2Int(currentArmor, maxArmor);
    }
}
