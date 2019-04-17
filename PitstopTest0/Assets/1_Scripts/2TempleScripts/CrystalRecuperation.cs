﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class CrystalRecuperation : MonoBehaviour
    {
        [SerializeField]
        Animator whatElementShouldAppear = default;
        [SerializeField]
        Animator crystalToGetAnim = default;
        [SerializeField]
        GameObject whatElementShouldDisappear = default;

        bool triggerOnceCheck = false;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Zayn" && !triggerOnceCheck)
            {
                whatElementShouldAppear.SetTrigger("Appear");

                crystalToGetAnim.SetTrigger("PlayerGet");

                //whatElementShouldDisappear.SetActive(false);

                triggerOnceCheck = true;
            }
        }
    }
}