using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Glade1Cinematic : MonoBehaviour
{
    public bool startCinematic = false;
    public GameObject virtualCameraPlayer;
    public Transform centerOfTheGlade;
    public HHSpawnerFirstGlade spawner;

    private void Update()
    {
        if (startCinematic == true)
        {
            virtualCameraPlayer.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Zayn")
        {
            startCinematic = true;
        }
    }
}