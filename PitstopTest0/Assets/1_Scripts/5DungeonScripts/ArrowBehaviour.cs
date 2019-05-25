using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ArrowBehaviour : MonoBehaviour
    {
        [SerializeField] bool hurtPlayerOnCollision = false;

        public Transform target = default;
        [SerializeField] float projSpeed = 2;

        public bool isFired = false;

        private void Update()
        {
            if (isFired)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, projSpeed * Time.deltaTime);
            }
        }   

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "ArrowDestroyer")
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player" && hurtPlayerOnCollision)
            {
                collision.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(1);
            }
        }
    }
}