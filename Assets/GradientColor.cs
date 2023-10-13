using UnityEngine;

public class GradientBackground : MonoBehaviour
{
    public Color[] colors; // Add multiple colors for the background
    public float duration = 10f; // Duration of the color transition
    public float changeInterval = 5f; // Interval between color changes

    private Camera mainCamera;
    private float startTime;
    private int colorIndex;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
            startTime = Time.time;
            colorIndex = 0;
        }
    }

    void Update()
    {
        float t = (Time.time - startTime) / duration;
        mainCamera.backgroundColor = Color.Lerp(colors[colorIndex], colors[(colorIndex + 1) % colors.Length], t);

        if (t >= 1)
        {
            if (Time.time - startTime >= changeInterval)
            {
                colorIndex = (colorIndex + 1) % colors.Length;
                startTime = Time.time;
            }
        }
    }
}
