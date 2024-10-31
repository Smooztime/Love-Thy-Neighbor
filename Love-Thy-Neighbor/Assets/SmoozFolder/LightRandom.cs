using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [SerializeField] Light targetLight;
    [SerializeField] float speed = 3f;
    [SerializeField] float intensityMultiplier = 2f;

    private void Animate()
    {
        float LightIntensity = Mathf.PerlinNoise(speed * Time.time, intensityMultiplier);

        targetLight.intensity = LightIntensity;
    }

    private void Update()
    {
        if (!targetLight) return;

        Animate();
    }
}
