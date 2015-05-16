using UnityEngine;
using System.Collections;

public class CamShaker : MonoBehaviour
{
    private float intensity, decay;

    public void shake()
    {
        intensity = 0.2f;
        decay = 0.01f;
    }

    void Update()
    {
        if (intensity <= 0.0f)
            return;

        transform.position += Random.insideUnitSphere * intensity;

        intensity -= decay;
    }
}
