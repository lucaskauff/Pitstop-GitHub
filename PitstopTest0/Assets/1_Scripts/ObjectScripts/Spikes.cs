using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField] int damageDealing;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageDealing);
            }
        }
    }
}