using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class PillarCheckForRocks : MonoBehaviour
    {     
        /*
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "ObjectRock")
            {
                if (!collision.gameObject.GetComponent<RockBehaviour>().isOnRepulse)
                {
                    Destroy(collision.gameObject);
                }
            }
        }
        */

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "ObjectRock")
            {
                if (!collision.gameObject.GetComponent<RockBehaviour>().isOnRepulse)
                {
                    Destroy(collision.gameObject);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "ObjectRock")
            {        
                if (!collision.gameObject.GetComponent<RockBehaviour>().isOnRepulse)
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}