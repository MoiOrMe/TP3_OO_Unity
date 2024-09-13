using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform target; // R�f�rence au personnage
    public Vector3 offset;   // Pour ajuster la hauteur de la barre de vie au-dessus de la t�te

    void Update()
    {
        // La barre de vie suit le personnage avec un l�ger d�calage (offset)
        transform.position = target.position + offset;
        transform.LookAt(Camera.main.transform); // Faire en sorte que la barre de vie regarde toujours la cam�ra
    }
}
