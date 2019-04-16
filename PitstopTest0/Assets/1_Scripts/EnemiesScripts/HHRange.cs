using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class HHRange : MonoBehaviour
    {
        public HHBehaviour hammerheadBeh;

        void OnTriggerStay2D(Collider2D collider)
        {
            Debug.Log(collider.gameObject);

            //condition not perfect !!!
            if (!hammerheadBeh.isArrived && hammerheadBeh.target == null && (collider.gameObject.name == "Zayn" || collider.gameObject.name == "Native"))
            {
                hammerheadBeh.target = collider.gameObject;
            }
        }
    }
}