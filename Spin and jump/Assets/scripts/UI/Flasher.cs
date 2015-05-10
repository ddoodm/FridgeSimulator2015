using UnityEngine;
using System.Collections;

public class Flasher : MonoBehaviour
{
    public AnimationCurve flashForm;

    private float startTime;
    private bool flashing = false;

    public void flash()
    {
        startTime = Time.time;
        flashing = true;
    }

    void Update()
    {
        if (!flashing)
            return;

        float dt = Time.time - startTime;

        this.guiTexture.color = new Color(1.0f, 1.0f, 1.0f, flashForm.Evaluate(dt));

        if (dt > flashForm.length)
            flashing = false;
    }
}
