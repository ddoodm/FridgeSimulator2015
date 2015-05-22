﻿using UnityEngine;
using System.Collections;

public class GoToMainGame : MonoBehaviour {


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Application.LoadLevel(1);
        }
    }
}
