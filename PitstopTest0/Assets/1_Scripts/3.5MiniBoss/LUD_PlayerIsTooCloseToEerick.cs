using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_PlayerIsTooCloseToEerick : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                FindObjectOfType<EerickBehaviourTestLUD>().haveToChangeItsSpot = true;
            }
        }
    }
}
