using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;  // Vie maximale
    public int currentHealth;    // Vie actuelle
    public int attackPower = 10; // Force d'attaque

    public HealthBar healthBar;  // R�f�rence � la barre de vie

    private bool canTakeDamage = true; // Pour v�rifier si le personnage peut prendre des d�g�ts
    private float damageCooldown = 1f; // Temps de cooldown entre deux appels de TakeDamage (1 seconde)

    void Start()
    {
        currentHealth = maxHealth; // Initialiser la vie � la valeur maximale
        healthBar.SetMaxHealth(maxHealth); // Initialiser la barre de vie
    }

    // Fonction appel�e lorsque le personnage subit des d�g�ts
    public void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            // Applique les d�g�ts
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            // V�rifie si le personnage est mort
            if (currentHealth <= 0)
            {
                Die();
            }

            // Emp�che de prendre des d�g�ts imm�diatement apr�s
            canTakeDamage = false;
            StartCoroutine(DamageCooldown());
        }
    }

    // Coroutine pour g�rer le cooldown des d�g�ts
    IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(damageCooldown); // Attend 1 seconde
        canTakeDamage = true; // Permet � nouveau de prendre des d�g�ts
    }

    void Die()
    {

        Debug.Log(gameObject.name + " est mort !");
        Destroy(gameObject);
    }
}
