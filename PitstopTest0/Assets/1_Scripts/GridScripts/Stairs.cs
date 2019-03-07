using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Pitstop
{
    public class Stairs : MonoBehaviour
    {
        Collider2D myCollider;
        [SerializeField]
        CinemachineImpulseSource myImpulseSource;
        [SerializeField]
        float stairsIsometricRatio = 0.8f;

        private void Start()
        {
            myCollider = GetComponent<Collider2D>();
        }

        //Sets up a new isometric ratio for the player + generates light impulse
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Zayn" && collision.gameObject.GetComponent<PlayerControllerIso>().isMoving)
            {
                collision.gameObject.GetComponent<PlayerControllerIso>().isometricRatio = stairsIsometricRatio;
                myImpulseSource.GenerateImpulse();
            }
        }

        //Resets the player's regular isometric ratio
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Zayn")
            {
                collision.gameObject.GetComponent<PlayerControllerIso>().isometricRatio = 2;
            }
        }
    }
}