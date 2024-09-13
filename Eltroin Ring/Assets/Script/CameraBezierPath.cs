using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBezierPath : MonoBehaviour
{
    public Transform point0; // Premier point de la courbe
    public Transform point1; // Deuxième point (point de contrôle)
    public Transform point2; // Troisième point (fin de la courbe)

    // Points supplémentaires pour une courbe cubique (facultatif)
    public Transform point3;

    public bool isCubic = false; // Activer ou non la courbe cubique

    public float duration = 10f; // Temps que la caméra met à parcourir la courbe
    private float t = 0; // Paramètre t entre 0 et 1 pour la courbe

    private Transform cameraTransform; // Référence à la caméra qui va suivre la courbe

    void Start()
    {
        cameraTransform = Camera.main.transform; // La caméra principale
        t = 0; // Initialisation du paramètre t
    }

    void Update()
    {
        // Mettre à jour le paramètre t en fonction du temps et de la durée souhaitée pour parcourir la courbe
        t += Time.deltaTime / duration;

        // Empêche t de dépasser 1 (la fin de la courbe)
        if (t > 1)
        {
            t = 1;
        }

        // Déplacer la caméra le long de la courbe
        if (isCubic)
        {
            MoveAlongCubicBezier();
        }
        else
        {
            MoveAlongQuadraticBezier();
        }
    }

    // Déplacer la caméra sur une courbe de Bézier quadratique
    void MoveAlongQuadraticBezier()
    {
        Vector3 newPosition = CalculateQuadraticBezierPoint(t, point0.position, point1.position, point2.position);
        cameraTransform.position = newPosition;
        LookAhead(newPosition);
    }

    // Déplacer la caméra sur une courbe de Bézier cubique
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
        float lookAheadT = t + 0.01f; // Petit décalage pour regarder légèrement en avant

        // Si nous sommes à la fin de la courbe, garder la dernière position
        if (lookAheadT > 1)
        {
            nextPosition = currentPosition;
        }
        else
        {
            // Calculer la position légèrement en avant sur la courbe
            if (isCubic)
            {
                nextPosition = CalculateCubicBezierPoint(lookAheadT, point0.position, point1.position, point2.position, point3.position);
            }
            else
            {
                nextPosition = CalculateQuadraticBezierPoint(lookAheadT, point0.position, point1.position, point2.position);
            }
        }

        // Tourner la caméra pour regarder vers la prochaine position sur la courbe
        cameraTransform.LookAt(nextPosition);
    }
}
