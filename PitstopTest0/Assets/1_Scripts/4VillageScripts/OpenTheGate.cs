using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Pitstop
{
    public class OpenTheGate : MonoBehaviour
    {
        public bool testBridgeReparation = false;

        [Header("Bridge parts")]
        [SerializeField] GameObject brokenBridge = default;
        [SerializeField] GameObject workingBridge = default;
        [SerializeField] Animator repairingBridge = default;

        [Header("Virtual cameras")]
        [SerializeField] GameObject vCamPlayer = default;
        [SerializeField] GameObject vCamBridge = default;

        private void Update()
        {
            if (testBridgeReparation)
            {
                BridgeReparation();
                testBridgeReparation = false;
            }
        }

        public void BridgeReparation()
        {
            vCamBridge.SetActive(true);
            vCamPlayer.SetActive(false);

            repairingBridge.SetTrigger("RepairBridge");

            workingBridge.SetActive(true);
            brokenBridge.SetActive(false);
        }

        public void BridgeIsRepaired()
        {
            vCamPlayer.SetActive(true);
            vCamBridge.SetActive(false);
        }
    }
}