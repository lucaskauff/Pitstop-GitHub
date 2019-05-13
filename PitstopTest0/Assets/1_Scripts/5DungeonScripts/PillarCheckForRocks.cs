using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class PillarCheckForRocks : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "ObjectRock")
            {
                Destroy(collision.gameObject);
                Destroy(collision.gameObject.transform.parent.gameObject);
            }
        }
    }
}