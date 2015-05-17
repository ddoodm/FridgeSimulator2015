using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour
{
    public AnimationCurve fadeFunction;

    private float startTime;

    void Start()
    {
        startTime = Time.time;

        for (int i = 0; i < renderer.materials.Length; i++)
        {
            Color color = renderer.materials[i].color;
            color.a = 0.0f;

            renderer.materials[i].color = color;
        }
    }

    void Update()
    {
        float dt = Time.time - startTime;
        float fade = fadeFunction.Evaluate(dt);

        for (int i = 0; i < renderer.materials.Length; i++ )
        {
            Color color = renderer.materials[i].color;
            color.a = fade;

            renderer.materials[i].color = color;
        }
    }
}
