using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class Glade1Tree : MonoBehaviour
    {
        [SerializeField] GameObject impulseApple = default;
        [SerializeField] Transform appleImpactOnGround = default;
        [SerializeField] float fallSpeed = 2;

        private void OnColliderEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                SpawnAnApple();
            }
        }

        public void SpawnAnApple()
        {
            impulseApple.GetComponent<ScannableObjectBehaviour>().targetPos = appleImpactOnGround.position;
            impulseApple.GetComponent<ScannableObjectBehaviour>().projectileSpeed = fallSpeed;
            impulseApple.GetComponent<ScannableObjectBehaviour>().isScannable = false;
            impulseApple.GetComponent<ScannableObjectBehaviour>().isFired = true;
        }
    }
}