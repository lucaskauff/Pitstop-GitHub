using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class IMP_Apple : MonoBehaviour
    {
        [SerializeField]
        ScannableObjectBehaviour scannableObjectBehaviour;
        public float appleProjectionSpeed;
        [SerializeField]
        GameObject explosionRange;
        [SerializeField]
        int timer;

        [SerializeField]
        GameObject Player;

       public Vector2 playerPos;

        private void Update()
        {
            if (scannableObjectBehaviour.isArrived)
            {
                Explode();
            }
        }

        public void Explode()
        {
            explosionRange.SetActive(true);
            Destroy(gameObject, 0.1f);
        }

        void Bounce()
        {
            if (scannableObjectBehaviour.isFired == true)
            {
                playerPos = Player.transform.position;
            }
        }
    }
}