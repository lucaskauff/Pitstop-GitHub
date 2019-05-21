using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class HHRelai : MonoBehaviour
    {
        [SerializeField] GameObject nextPoint = default;

        GorillaBehaviour gorillaBehaviour;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Hammerhead(Clone)")
            {
                gorillaBehaviour = collision.gameObject.GetComponent<GorillaBehaviour>();
                gorillaBehaviour.target = nextPoint;
                gorillaBehaviour.targetPos = nextPoint.transform.position;
                gorillaBehaviour.isFleeing = true;
            }
        }
    }
}