using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class PillarCheckForRocks : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "ObjectRock")
            {
                Debug.Log("gggggg");
                Destroy(collision.gameObject);
            }
        }
    }
}