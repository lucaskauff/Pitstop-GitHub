using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class CrystalRecuperation : MonoBehaviour
    {
        Collider2D myCollider;

        [SerializeField]
        UIManager uIManager;
        [SerializeField]
        GameObject[] whatElementsShouldAppear;
        [SerializeField]
        GameObject whatElementShouldDisappear;

        private void Start()
        {
            myCollider = GetComponent<CompositeCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Zayn")
            {
                whatElementShouldDisappear.SetActive(false);

                foreach (var element in whatElementsShouldAppear)
                {
                    uIManager.MakeUIElementAppear(element);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Zayn")
            {
                myCollider.isTrigger = false;
            }
        }
    }
}