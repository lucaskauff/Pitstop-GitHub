using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ArrowDoor : MonoBehaviour
    {
        [SerializeField] Animator myAnim = default;
        [SerializeField] Collider2D myTrigger = default;
        [SerializeField] GameObject associatedSymbol = default;
        [SerializeField] bool stayForever = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Arrow")
            {
                myAnim.SetTrigger("OpenTheDoor");
                associatedSymbol.SetActive(true);
                Destroy(collision.gameObject);

                if (!stayForever)
                {
                    myTrigger.enabled = false;
                    stayForever = true;
                }
            }
        }
    }
}