using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;
    public float dayLengthInMinutes = 10f;
    public Gradient lightColorGradient;
    public AnimationCurve lightIntensityCurve;
    public float timeMultiplier = 1f;

    private float currentTime;
    private float timeSpeed;
    private bool isTimeAccelerated = false;

    void Start()
    {
        timeSpeed = 24f / (dayLengthInMinutes * 60f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isTimeAccelerated = !isTimeAccelerated;
            timeMultiplier = isTimeAccelerated ? 50f : 1f;
        }

        currentTime += Time.deltaTime * timeSpeed * timeMultiplier;
        if (currentTime >= 24f)
        {
            currentTime = 0f;
        }

        float sunAngle = (currentTime / 24f) * 360f - 90f;
        directionalLight.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

        directionalLight.intensity = lightIntensityCurve.Evaluate(currentTime / 24f);

        directionalLight.color = lightColorGradient.Evaluate(currentTime / 24f);
    }
}
