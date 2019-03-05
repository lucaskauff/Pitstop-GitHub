using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Stairs : MonoBehaviour
{
    Collider2D myCollider;
    public float stairsIsometricRatio = 0.8f;

    private void Start()
    {
        myCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Zayn" && collision.gameObject.GetComponent<PlayerControllerIso>().isMoving)
        {
            collision.gameObject.GetComponent<PlayerControllerIso>().isometricRatio = stairsIsometricRatio;
            collision.gameObject.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Zayn")
        {
            collision.gameObject.GetComponent<PlayerControllerIso>().isometricRatio = 2;
        }
    }
}