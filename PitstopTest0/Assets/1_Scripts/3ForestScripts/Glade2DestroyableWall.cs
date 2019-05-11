using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class Glade2DestroyableWall : MonoBehaviour
    {
        [SerializeField] Animator myAnim = default;
        [SerializeField] Collider2D myColl = default;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                myAnim.SetTrigger("IsDestroyed");
                myColl.enabled = false;
            }
        }
    }
}