using UnityEngine;
using System.Collections;

public class ShadowText : MonoBehaviour
{
    public GUIText[] shadows;

    public string text
    {
        set
        {
            this.guiText.text = value;

            for(int i=0; i<shadows.Length;i++)
                shadows[i].text = value;
        }
    }
}
