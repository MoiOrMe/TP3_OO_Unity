using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform target; // Référence au personnage
    public Vector3 offset;   // Pour ajuster la hauteur de la barre de vie au-dessus de la tête

    void Update()
    {
        // La barre de vie suit le personnage avec un léger décalage (offset)
        transform.position = target.position + offset;
        transform.LookAt(Camera.main.transform); // Faire en sorte que la barre de vie regarde toujours la caméra
    }
}
