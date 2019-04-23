using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

namespace Pitstop
{
    public class PlayerControllerIso : MonoBehaviour
    {
        //GameManager
        SceneLoader sceneLoader;
        InputManager inputManager;

        //My components
        Rigidbody2D myRb;
        Collider2D myCollider;
        Animator myAnim;

        //Public
        public bool canMove = true;
        public bool isMoving = false;
        public bool isBeingRepulsed = false;
        public float isometricRatio = 2;

        //Serializable
        [SerializeField]
        Transform sceneStartingPoint = null;
        [SerializeField]
        float moveSpeed = 3;
        [SerializeField]
        float dashSpeed = 5;
        [SerializeField]
        float dashLength = 0.5f;
        [SerializeField]
        float dashCooldown = 1;

        //Private
        Vector2 moveInput;
        Vector2 lastMove;
        float initialMoveSpeed = 0;
        float dashRate = 0;

        void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;
            inputManager = GameManager.Instance.inputManager;

            myRb = GetComponent<Rigidbody2D>();
            myCollider = GetComponent<Collider2D>();
            myAnim = GetComponent<Animator>();

            Spawn();
        }

        private void Spawn()
        {
            //Orientations of the player on start of scene
            switch (sceneLoader.activeScene)
            {
                case "1_TEMPLE":
                    lastMove = new Vector2(1, 0);
                    break;

                case "2_FOREST":
                    lastMove = new Vector2(1, 1);
                    break;

                case "3_VILLAGE":
                    lastMove = new Vector2(0, -1);
                    break;
            }

            myAnim.SetFloat("LastMoveX", lastMove.x);
            myAnim.SetFloat("LastMoveY", lastMove.y);
            transform.position = sceneStartingPoint.position;
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
            else if (!isBeingRepulsed)
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "ObjectApple")
            {
                Debug.Log("Apple touched !");
                isBeingRepulsed = true;
            }
        }

        private void Dash()
        {
            dashRate = Time.time + dashCooldown;
            initialMoveSpeed = moveSpeed;
            moveSpeed = dashSpeed;
            isBeingRepulsed = true;
            StartCoroutine(ComeOnAndDash());
        }

        IEnumerator ComeOnAndDash()
        {
            yield return new WaitForSeconds(dashLength);
            moveSpeed = initialMoveSpeed;
            isBeingRepulsed = false;
        }
    }
}