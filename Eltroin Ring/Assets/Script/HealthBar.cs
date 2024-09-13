using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    // Initialiser la barre de vie � la valeur maximale
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // Mettre � jour la barre de vie avec la valeur actuelle
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}

