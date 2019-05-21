using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class CrystalRecuperation : MonoBehaviour
    {
        [SerializeField] Animator crystalUIAppear = default;
        [SerializeField] GameObject scanRange = default;
        [SerializeField] Animator crystalToGetAnim = default;
        //[SerializeField] GameObject whatElementShouldDisappear = default;

        bool triggerOnceCheck = false;

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