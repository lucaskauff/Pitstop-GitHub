using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class CrystalRecuperation : MonoBehaviour
    {
        [SerializeField]
        UIManager uIManager;
        [SerializeField]
        GameObject[] whatElementsShouldAppear;
        [SerializeField]
        GameObject whatElementShouldDisappear;

        bool triggerOnceCheck = false;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "Zayn" && !triggerOnceCheck)
            {
                whatElementShouldDisappear.SetActive(false);

                foreach (var element in whatElementsShouldAppear)
                {
                    uIManager.MakeUIElementAppear(element);
                }

                triggerOnceCheck = true;
            }
        }
    }
}