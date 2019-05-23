using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class FeelsMoment : MonoBehaviour
    {
        [SerializeField] float secondsToWait = 5;
        [SerializeField] GameObject vCamHorizon = default;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                StartCoroutine(WaitForPlayerToStay());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                StopAllCoroutines();
                vCamHorizon.SetActive(false);
            }
        }

        IEnumerator WaitForPlayerToStay()
        {
            yield return new WaitForSeconds(secondsToWait);
            vCamHorizon.SetActive(true);
        }
    }
}