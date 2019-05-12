using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class Glade2DestroyableWall : MonoBehaviour
    {
        [HideInInspector] public bool isDestroyed = false;

        [SerializeField] Animator myAnim = default;
        [SerializeField] Glade2SpecificEvents glade2SpecificEvents = default;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                myAnim.SetTrigger("IsDestroyed");
            }
        }

        public void WallReallyIsDestroyed()
        {
            glade2SpecificEvents.HammerheadIsOutOfGlade();
            isDestroyed = true;
        }
    }
}