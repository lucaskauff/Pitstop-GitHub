using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class DeerickBehaviour : MonoBehaviour
    {
        [Header("My components")]
        [SerializeField] Animator myAnim = default;
        [SerializeField] Collider2D myColl = default;

        [Header("Serializable")]
        [SerializeField] float moveSpeed = default;
        [SerializeField] Transform[] movePoints = default;

        static int moveIndex = 0;

        public Vector2 moveInput;
        public Vector2 lastMove;
        public bool isMoving = false;

        private void Start()
        {
            //position on Start
            transform.position = movePoints[moveIndex].position;

            //orientation of the sprite on Start
            lastMove = new Vector2(-1, 0);
            myAnim.SetFloat("LastMoveX", lastMove.x);
            myAnim.SetFloat("LastMoveY", lastMove.y);
        }

        private void Update()
        {
            Animations();

            if (transform.position == movePoints[moveIndex].position)
            {
                isMoving = false;

                if (moveIndex == movePoints.Length-1)
                {
                    //should disappear in the woods
                    //gameObject.SetActive(false);
                    myAnim.SetTrigger("Disappear");
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
            isMoving = true;
            myColl.enabled = false;
            transform.position = Vector2.MoveTowards(transform.position, movePoints[moveIndex].position, moveSpeed * Time.deltaTime);
            moveInput = movePoints[moveIndex].position - transform.position;
            lastMove = moveInput;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "ObjectApple")
            {
                moveIndex += 1;
            }
        }

        public void Animations()
        {
            myAnim.SetBool("IsMoving", isMoving);
            myAnim.SetFloat("LastMoveX", lastMove.x);
            myAnim.SetFloat("LastMoveY", lastMove.y);
            myAnim.SetFloat("MoveX", moveInput.x);
            myAnim.SetFloat("MoveY", moveInput.y);
        }
    }
}