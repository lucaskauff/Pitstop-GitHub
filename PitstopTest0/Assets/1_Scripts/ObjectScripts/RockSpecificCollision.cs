using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace Pitstop
{
    public class RockSpecificCollision : MonoBehaviour
    {
        [SerializeField] Rigidbody2D myRb = default;
        [SerializeField] float repulseDelay = 1f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "ObjectApple")
            {
                StartCoroutine(WaitAfterBeingRepulsed());
            }
        }

        IEnumerator WaitAfterBeingRepulsed()
        {
            myRb.bodyType = RigidbodyType2D.Dynamic;            
            yield return new WaitForSeconds(repulseDelay);
            myRb.velocity = Vector2.zero;
            myRb.bodyType = RigidbodyType2D.Kinematic;
            StopCoroutine(WaitAfterBeingRepulsed());
        }
    }
}