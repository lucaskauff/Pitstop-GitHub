using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class PathFromMinibossToVillage : MonoBehaviour
    {
        [SerializeField] Collider2D myCollider = default;
        [SerializeField] EnemyHealthManager minibossHealth = default;

        bool gateIsOpen = false;

        private void Update()
        {            
            if (minibossHealth.enemyCurrentHealth <= 0 && !gateIsOpen)
            {
                //dialogue box + native should move towards the village and then open the gate
                myCollider.enabled = false;
                gateIsOpen = true;
            }
        }
    }
}