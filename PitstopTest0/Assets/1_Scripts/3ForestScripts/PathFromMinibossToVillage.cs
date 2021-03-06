﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class PathFromMinibossToVillage : MonoBehaviour
    {
        [Header("My component")]
        [SerializeField] Collider2D myCollider = default;

        [Header("Serializable")]
        [SerializeField] EnemyHealthManager minibossHealth = default;

        bool gateIsOpen = false;

        private void Update()
        {            
            if (minibossHealth.enemyCurrentHealth <= 0 && !gateIsOpen)
            {
                BossIsDefeated();
            }
        }

        private void BossIsDefeated()
        {
            //dialogue box + native should move towards the village and then open the gate
            myCollider.enabled = false;
            gateIsOpen = true;
        }
    }
}