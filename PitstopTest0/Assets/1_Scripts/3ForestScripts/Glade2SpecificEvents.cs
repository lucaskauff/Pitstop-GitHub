using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class Glade2SpecificEvents : MonoBehaviour
    {
        [SerializeField] Transform targetForHammerHeadInsideG2 = default;
        [SerializeField] GameObject targetForHammerHeadOutsideG2 = default;

        GorillaBehaviour hHGlade2Beh = null;
        bool hHStill = false;

        private void Update()
        {
            if (hHGlade2Beh != null)
            {
                if (hHGlade2Beh.gameObject.transform.position == targetForHammerHeadInsideG2.position && !hHStill)
                {
                    hHGlade2Beh.canMove = false;
                    hHStill = true;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Hammerhead(Clone)")
            {
                hHGlade2Beh = collision.gameObject.GetComponent<GorillaBehaviour>();
            }
            else if (collision.gameObject.tag == "Player")
            {
                hHGlade2Beh.isFleeing = false;
                hHGlade2Beh.canMove = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == hHGlade2Beh.gameObject)
            {
                hHGlade2Beh.target = targetForHammerHeadOutsideG2;
                hHGlade2Beh.isFleeing = true;
            }
        }
    }
}