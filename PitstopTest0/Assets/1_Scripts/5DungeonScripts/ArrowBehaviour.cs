using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ArrowBehaviour : MonoBehaviour
    {
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
    }
}