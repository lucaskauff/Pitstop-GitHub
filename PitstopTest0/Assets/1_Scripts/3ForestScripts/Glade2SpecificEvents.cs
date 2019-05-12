using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class Glade2SpecificEvents : MonoBehaviour
    {
        [SerializeField] Transform targetForHammerHeadInsideG2 = default;
        [SerializeField] GameObject targetForHammerHeadOutsideG2 = default;
        [SerializeField] float hammerheadSlowSpeed = 1f;
        [SerializeField] float slowDownLength = 5f;

        float hammerheadOriginalRushSpeed;

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
                hammerheadOriginalRushSpeed = hHGlade2Beh.rushSpeed;
                //it is buggy
                //StartCoroutine(SlowingTheHammerheadDown());
            }
        }

        public void HammerheadIsOutOfGlade()
        {
            hHGlade2Beh.target = targetForHammerHeadOutsideG2;
            hHGlade2Beh.isFleeing = true;
        }

        IEnumerator SlowingTheHammerheadDown()
        {
            hHGlade2Beh.rushSpeed = hammerheadSlowSpeed;
            yield return new WaitForSeconds(slowDownLength);
            hHGlade2Beh.rushSpeed = hammerheadOriginalRushSpeed;
        }
    }
}