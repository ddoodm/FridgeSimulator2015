using UnityEngine;
using System.Collections;

public class tutorialPrompt : MonoBehaviour {

    public string tutorialmessage;
    public GUIText text;
    public GUIText textShadow;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.text = tutorialmessage;
            textShadow.text = tutorialmessage;
        }
    }
}
