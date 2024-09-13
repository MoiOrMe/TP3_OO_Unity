using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    public LineRenderer lineRenderer; // Le LineRenderer qui affichera la courbe
    public int segmentCount = 50;     // Nombre de segments pour la précision de la courbe

    // Points de contrôle pour une courbe quadratique
    public Transform point0;
    public Transform point1;
    public Transform point2;

    // Points de contrôle pour une courbe cubique (facultatif)
    public Transform point3;

    public bool isCubic = false; // Activer/désactiver la courbe cubique

    void Start()
    {
        lineRenderer.positionCount = segmentCount + 1;
    }

    void Update()
    {
        if (isCubic)
        {
            DrawCubicBezierCurve();
        }
        else
        {
            DrawQuadraticBezierCurve();
        }
    }

    // Méthode pour dessiner une courbe de Bézier quadratique
    void DrawQuadraticBezierCurve()
    {
        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            Vector3 position = CalculateQuadraticBezierPoint(t, point0.position, point1.position, point2.position);
            lineRenderer.SetPosition(i, position); // Mettre à jour la position du LineRenderer
        }
    }

    // Méthode pour dessiner une courbe de Bézier cubique
    void DrawCubicBezierCurve()
    {
        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            Vector3 position = CalculateCubicBezierPoint(t, point0.position, point1.position, point2.position, point3.position);
            lineRenderer.SetPosition(i, position); // Mettre à jour la position du LineRenderer
        }
    }

    // Calculer un point sur la courbe quadratique à l'instant t
    Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return (u * u) * p0 + 2 * u * t * p1 + (t * t) * p2;
    }

    // Calculer un point sur la courbe cubique à l'instant t
    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        return (u * u * u) * p0 + 3 * (u * u) * t * p1 + 3 * u * (t * t) * p2 + (t * t * t) * p3;
    }
}
