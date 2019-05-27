using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class HHRelai : MonoBehaviour
    {
        [SerializeField] bool isFinalRelay = false;
        [SerializeField] GameObject nextPoint = default;

        GorillaBehaviour gorillaBehaviour;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Hammerhead(Clone)")
            {
                if (isFinalRelay)
                {
                    Destroy(collision.gameObject);
                }

                gorillaBehaviour = collision.gameObject.GetComponent<GorillaBehaviour>();

                if (!isFinalRelay)
                {
                    gorillaBehaviour.target = nextPoint;
                    gorillaBehaviour.targetPos = nextPoint.transform.position;
                }

                gorillaBehaviour.isFleeing = true;
            }
        }
    }
}