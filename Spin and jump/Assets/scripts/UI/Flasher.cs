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

        Color oldC = this.guiTexture.color;
        oldC.a = flashForm.Evaluate(dt);
        this.guiTexture.color = oldC;

        if (dt > flashForm.length)
            flashing = false;
    }
}
