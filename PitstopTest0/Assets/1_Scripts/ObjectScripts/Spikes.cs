using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField] int damageDealing = 1;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageDealing);
            }
            else if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageDealing);
            }
        }
    }
}