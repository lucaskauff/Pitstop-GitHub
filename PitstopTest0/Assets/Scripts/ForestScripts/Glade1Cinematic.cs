using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Glade1Cinematic : MonoBehaviour
{
    public GameObject virtualCameraPlayer;

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Zayn")
        {
            virtualCameraPlayer.SetActive(false);
        }
    }
}