using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGlade : MonoBehaviour
{
    public GameObject virtualCameraPlayer;
    public GameObject virtualCameraGlade;

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Zayn")
        {
            virtualCameraPlayer.SetActive(true);
            virtualCameraGlade.SetActive(false);
        }
    }
}