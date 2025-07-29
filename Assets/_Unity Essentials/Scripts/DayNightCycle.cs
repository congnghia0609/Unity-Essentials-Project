using UnityEngine;

[ExecuteAlways] // Makes it work in Edit mode too, remove if not needed
public class DayNightCycle : MonoBehaviour
{
    [Tooltip("How many real-time seconds it takes for a full day cycle (360 degrees).")]
    [Range(1f, 86400f)] // 1 second to 24 hours
    public float dayLengthInSeconds = 60f;

    private void Update()
    {
        if (dayLengthInSeconds <= 0f)
            return;

        float degreesPerSecond = 360f / dayLengthInSeconds;
        transform.Rotate(Vector3.right, degreesPerSecond * Time.deltaTime, Space.Self);
    }
}
