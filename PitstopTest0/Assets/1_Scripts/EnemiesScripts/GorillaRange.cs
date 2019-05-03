using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class GorillaRange : MonoBehaviour
    {
        public GorillaBehaviour gorillaBeh;

        void OnTriggerStay2D(Collider2D collider)
        {
            if (!gorillaBeh.isArrived && !gorillaBeh.isFleeing && gorillaBeh.target == null && (collider.gameObject.name == "Zayn" || collider.gameObject.name == "Native"))
            {
                gorillaBeh.target = collider.gameObject;
            }
        }
    }
}