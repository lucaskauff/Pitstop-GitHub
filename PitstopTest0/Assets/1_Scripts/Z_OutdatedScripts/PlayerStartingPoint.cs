using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class PlayerStartingPoint : MonoBehaviour
    {
        private PlayerControllerIso thePlayer;

        void Start()
        {
            thePlayer = FindObjectOfType<PlayerControllerIso>();
            thePlayer.transform.position = transform.position;
        }
    }
}