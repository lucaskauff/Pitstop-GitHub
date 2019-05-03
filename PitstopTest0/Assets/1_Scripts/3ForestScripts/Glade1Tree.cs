using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class Glade1Tree : MonoBehaviour
    {
        [SerializeField] ScannableObjectBehaviour impulseApple = default;
        [SerializeField] Transform appleImpactOnGround = default;
        [SerializeField] float fallSpeed = 2;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                SpawnAnApple();
            }
        }

        public void SpawnAnApple()
        {
            impulseApple.targetPos = appleImpactOnGround.position;
            impulseApple.projectileSpeed = fallSpeed;
            impulseApple.isScannable = false;
            impulseApple.isFired = true;
        }
    }
}