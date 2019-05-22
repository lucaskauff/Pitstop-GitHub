using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ArrowDeviator : MonoBehaviour
    {
        [SerializeField] Collider2D myCol = default;
        [SerializeField] Transform newTarget = default;
        [SerializeField] HookPointBehaviour[] hookpointsNeeded = default;

        private void Start()
        {
            myCol.enabled = false;
        }

        private void Update()
        {
            CheckHookpoints();
        }

        void CheckHookpoints()
        {
            foreach (var hookpoint in hookpointsNeeded)
            {
                if (!hookpoint.markSign)
                {
                    myCol.enabled = false;
                    return;
                }

                myCol.enabled = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Arrow")
            {
                collision.gameObject.GetComponent<ArrowBehaviour>().target = newTarget;
            }
        }
    }
}