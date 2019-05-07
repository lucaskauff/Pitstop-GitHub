using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class CrystalAltarRecuperation : MonoBehaviour
    {
        [SerializeField] Animator crystalUIAppear = default;
        [SerializeField] GameObject scanRange = default;
        [SerializeField] Animator crystalToGetAnim = default;
        //[SerializeField] GameObject whatElementShouldDisappear = default;

        [HideInInspector] public bool triggerOnceCheck = false;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Zayn" && !triggerOnceCheck)
            {
                crystalUIAppear.SetTrigger("Appear");

                scanRange.SetActive(true);

                crystalToGetAnim.SetTrigger("PlayerGet");

                //for blue beam over altar
                //whatElementShouldDisappear.SetActive(false);

                triggerOnceCheck = true;
            }
        }
    }
}