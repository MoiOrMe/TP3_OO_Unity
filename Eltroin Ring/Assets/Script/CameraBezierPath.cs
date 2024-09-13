using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBezierPath : MonoBehaviour
{
    public Transform point0; // Premier point de la courbe
    public Transform point1; // Deuxi�me point (point de contr�le)
    public Transform point2; // Troisi�me point (fin de la courbe)

    // Points suppl�mentaires pour une courbe cubique (facultatif)
    public Transform point3;

    public bool isCubic = false; // Activer ou non la courbe cubique

    public float duration = 10f; // Temps que la cam�ra met � parcourir la courbe
    private float t = 0; // Param�tre t entre 0 et 1 pour la courbe

    private Transform cameraTransform; // R�f�rence � la cam�ra qui va suivre la courbe

    void Start()
    {
        cameraTransform = Camera.main.transform; // La cam�ra principale
        t = 0; // Initialisation du param�tre t
    }

    void Update()
    {
        // Mettre � jour le param�tre t en fonction du temps et de la dur�e souhait�e pour parcourir la courbe
        t += Time.deltaTime / duration;

        // Emp�che t de d�passer 1 (la fin de la courbe)
        if (t > 1)
        {
            t = 1;
        }

        // D�placer la cam�ra le long de la courbe
        if (isCubic)
        {
            MoveAlongCubicBezier();
        }
        else
        {
            MoveAlongQuadraticBezier();
        }
    }

    // D�placer la cam�ra sur une courbe de B�zier quadratique
    void MoveAlongQuadraticBezier()
    {
        Vector3 newPosition = CalculateQuadraticBezierPoint(t, point0.position, point1.position, point2.position);
        cameraTransform.position = newPosition;
        LookAhead(newPosition);
    }

    // D�placer la cam�ra sur une courbe de B�zier cubique
    void MoveAlongCubicBezier()
    {
        Vector3 newPosition = CalculateCubicBezierPoint(t, point0.position, point1.position, point2.position, point3.position);
        cameraTransform.position = newPosition;
        LookAhead(newPosition);
    }

    // Calculer un point sur la courbe quadratique
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return (u * u) * p0 + 2 * u * t * p1 + (t * t) * p2;
    }

    // Calculer un point sur la courbe cubique
    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        return (u * u * u) * p0 + 3 * (u * u) * t * p1 + 3 * u * (t * t) * p2 + (t * t * t) * p3;
    }

    // Regarder en avant en suivant la trajectoire de la courbe
    void LookAhead(Vector3 currentPosition)
    {
        Vector3 nextPosition;
        float lookAheadT = t + 0.01f; // Petit d�calage pour regarder l�g�rement en avant

        // Si nous sommes � la fin de la courbe, garder la derni�re position
        if (lookAheadT > 1)
        {
            nextPosition = currentPosition;
        }
        else
        {
            // Calculer la position l�g�rement en avant sur la courbe
            if (isCubic)
            {
                nextPosition = CalculateCubicBezierPoint(lookAheadT, point0.position, point1.position, point2.position, point3.position);
            }
            else
            {
                nextPosition = CalculateQuadraticBezierPoint(lookAheadT, point0.position, point1.position, point2.position);
            }
        }

        // Tourner la cam�ra pour regarder vers la prochaine position sur la courbe
        cameraTransform.LookAt(nextPosition);
    }
}
