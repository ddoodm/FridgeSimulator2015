using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class tutorialPrompt : MonoBehaviour {

    public string tutorialmessage;
    public GUIText text;
    public GUIText textShadow;

    public Image[] showList, hideList;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.text = tutorialmessage;
            textShadow.text = tutorialmessage;

            foreach (Image i in showList)
                i.gameObject.SetActive(true);
            foreach (Image i in hideList)
                i.gameObject.SetActive(false);
        }
    }
}
