using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class DeerickBehaviour : MonoBehaviour
    {
        [Header("My components")]
        //use this in case of animation arrival
        //[SerializeField] Animator myAnim = default;
        [SerializeField] Collider2D myColl = default;

        [Header("Serializable")]
        [SerializeField] float moveSpeed = default;
        [SerializeField] Transform[] movePoints = default;

        static int moveIndex = 0;

        private void Start()
        {
            transform.position = movePoints[moveIndex].position;
        }

        private void Update()
        {
            if (transform.position == movePoints[moveIndex].position)
            {
                if (moveIndex == movePoints.Length-1)
                {
                    gameObject.SetActive(false);
                    return;
                }

                myColl.enabled = true;
            }
            else
            {
                Flee();
            }
        }

        public void Flee()
        {
            myColl.enabled = false;
            transform.position = Vector2.MoveTowards(transform.position, movePoints[moveIndex].position, moveSpeed * Time.deltaTime);            
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                moveIndex += 1;
            }
        }
    }
}