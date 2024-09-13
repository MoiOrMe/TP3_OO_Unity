using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;  // Vie maximale
    public int currentHealth;    // Vie actuelle
    public int attackPower = 10; // Force d'attaque

    public HealthBar healthBar;  // Référence à la barre de vie

    private bool canTakeDamage = true; // Pour vérifier si le personnage peut prendre des dégâts
    private float damageCooldown = 1f; // Temps de cooldown entre deux appels de TakeDamage (1 seconde)

    void Start()
    {
        currentHealth = maxHealth; // Initialiser la vie à la valeur maximale
        healthBar.SetMaxHealth(maxHealth); // Initialiser la barre de vie
    }

    // Fonction appelée lorsque le personnage subit des dégâts
    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            // Applique les dégâts
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            // Vérifie si le personnage est mort
            if (currentHealth <= 0)
            {
                Die();
            }

            // Empêche de prendre des dégâts immédiatement après
            canTakeDamage = false;
            StartCoroutine(DamageCooldown());
        }
    }

    // Coroutine pour gérer le cooldown des dégâts
    IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldown); // Attend 1 seconde
        canTakeDamage = true; // Permet à nouveau de prendre des dégâts
    }

    void Die()
    {

        Debug.Log(gameObject.name + " est mort !");
        Destroy(gameObject);
    }
}
