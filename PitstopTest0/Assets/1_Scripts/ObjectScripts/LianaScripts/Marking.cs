using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class Marking : MonoBehaviour
    {
        public HookPointBehaviour hookPoint;

        void Update()
        {
            if (hookPoint.markSign == true)
            {
                this.GetComponent<SpriteRenderer>().enabled = true;
            }

            else
            {
                this.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}