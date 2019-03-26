using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

namespace Pitstop
{
    public class PlayerControllerIso : MonoBehaviour
    {
        InputManager inputManager;

        //My components
        Rigidbody2D myRb;
        Collider2D myCollider;
        Animator myAnim;

        //Public
        public bool canMove = true;
        public bool isBeingRepulsed = false;
        public float isometricRatio = 2;

        //Serializable
        [SerializeField]
        Transform sceneStartingPoint = null;
        [SerializeField]
        float moveSpeed = 3;
        [SerializeField]
        float dashSpeed = 30;
        [SerializeField]
        float dashTime = 1;
        [SerializeField]
        float dashLength = 0.5f;

        //Private
        Vector2 moveInput;
        public bool isMoving = false;
        Vector2 lastMove = new Vector2(1, 0);
        float dashRate = 0;

        void Start()
        {
            inputManager = GameManager.Instance.inputManager;

            myRb = GetComponent<Rigidbody2D>();
            myCollider = GetComponent<Collider2D>();
            myAnim = GetComponent<Animator>();

            Spawn();

            canMove = true;
        }

        void Update()
        {
            isMoving = false;

            if (!canMove)
            {
                myRb.velocity = Vector2.zero;
                myAnim.SetBool("IsMoving", isMoving);
                return;
            }

            moveInput = new Vector2(inputManager.horizontalInput, inputManager.verticalInput / isometricRatio).normalized;

            if (moveInput != Vector2.zero)
            {
                myRb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
                isMoving = true;
                lastMove = moveInput;
            }
            else
            {
                myRb.velocity = Vector2.zero;
            }

            if (inputManager.dashKey && Time.time > dashRate)
            {
                Dash();
            }

            if (isBeingRepulsed)
            {
                StartCoroutine(ComeOnAndDash());
            }
            else
            {
                StopCoroutine(ComeOnAndDash());
            }

            //Infos to animator
            myAnim.SetBool("IsMoving", isMoving);
            myAnim.SetFloat("LastMoveX", lastMove.x);
            myAnim.SetFloat("LastMoveY", lastMove.y);
            myAnim.SetFloat("MoveX", moveInput.x);
            myAnim.SetFloat("MoveY", moveInput.y);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "ObjectApple")
            {
                Debug.Log("Apple touched !");

            }
        }

        private void Dash()
        {
            isBeingRepulsed = true;
            dashRate = Time.time + dashTime;
            myRb.velocity = lastMove * dashSpeed;
        }

        private void Spawn()
        {
            transform.position = sceneStartingPoint.position;
        }

        IEnumerator ComeOnAndDash()
        {
            yield return new WaitForSeconds(dashLength);
            isBeingRepulsed = false;
        }
    }
}