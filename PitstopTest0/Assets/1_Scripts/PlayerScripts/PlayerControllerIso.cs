using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
    public class PlayerControllerIso : MonoBehaviour
    {
        //GameManager
        SceneLoader sceneLoader;
        InputManager inputManager;

        [Header("My components")]
        [SerializeField] Rigidbody2D myRb = default;
        [SerializeField] Animator myAnim = default;
        [SerializeField] Image dashCdFb = default;

        //Public
        public bool canMove = true;
        [HideInInspector] public bool playerCanMove = true;
        public bool isMoving = false;
        public bool isBeingRepulsed = false;
        public float moveSpeed = 3;
        public float isometricRatio = 2;

        //Serializable
        public static int savingPointIndex = 0;
        [SerializeField] Transform[] sceneStartingPoint;
        [SerializeField] float dashSpeed = 5;
        [SerializeField] float dashLength = 0.5f;
        [SerializeField] float dashCooldown = 1;
        [SerializeField] float repulseTime = 0.5f;
        [SerializeField] float repulseTimeDash = 0.5f;

        //Private
        [HideInInspector] public Vector2 moveInput;
        [HideInInspector] public Vector2 lastMove;
        float initialMoveSpeed = 0;
        float dashRate = 0;

        /*
        [Header("Sounds")]
        [SerializeField] AudioSource footStepSound = default;
        */

        void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;
            inputManager = GameManager.Instance.inputManager;

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

                case "2_MINIBOSS":
                    lastMove = new Vector2(1, 0);
                    break;

                case "3_VILLAGE":
                    lastMove = new Vector2(0, 1);
                    break;

                case "4_DUNGEON":
                    lastMove = new Vector2(1, 1);
                    break;
            }

            myAnim.SetFloat("LastMoveX", lastMove.x);
            myAnim.SetFloat("LastMoveY", lastMove.y);

            transform.position = sceneStartingPoint[savingPointIndex].position;
            
            canMove = true;
        }

        void Update()
        {
            isMoving = false;

            //DEBUG WITH LIANA !
            Debug.DrawLine(transform.position, new Vector2(transform.position.x + moveInput.x, transform.position.y + moveInput.y), Color.blue);

            if (Time.time < dashRate)
            {
                dashCdFb.fillAmount = (dashRate - Time.time) / dashCooldown;
            }
            else if (inputManager.dashKey)
            {
                Dash();
            }
            else if (!isBeingRepulsed)
            {
                StopCoroutine(ComeOnAndDash());
                StopCoroutine(RepulsionOnDash());
            }

            if (!canMove)
            {
                myRb.velocity = Vector2.zero;
                myAnim.SetBool("IsMoving", isMoving);
                return;
            }

            if (playerCanMove)
            {
                moveInput = new Vector2(inputManager.horizontalInput, inputManager.verticalInput / isometricRatio).normalized;
            }

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

            //Infos to animator
            myAnim.SetBool("IsMoving", isMoving);
            myAnim.SetFloat("LastMoveX", lastMove.x);
            myAnim.SetFloat("LastMoveY", lastMove.y);
            myAnim.SetFloat("MoveX", moveInput.x);
            myAnim.SetFloat("MoveY", moveInput.y);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "ObjectApple" && !isBeingRepulsed)
            {                
                if (collision.gameObject.GetComponentInParent<IMP_Apple>().hasExploded)
                {
                    Debug.Log("Apple touched !");
                    StartCoroutine(ComeOnAndFly());
                }
            }
        }

        private void Dash()
        {
            dashRate = Time.time + dashCooldown;
            initialMoveSpeed = moveSpeed;
            moveSpeed = dashSpeed;
            isBeingRepulsed = true;
            StartCoroutine(ComeOnAndDash());
            StartCoroutine(RepulsionOnDash());
        }

        public void IncrementSavingPoint(int associatedIndex)
        {
            savingPointIndex = associatedIndex;
        }

        public void ResetSavingPoint()
        {
            savingPointIndex = 0;
        }

        IEnumerator ComeOnAndDash()
        {
            yield return new WaitForSeconds(dashLength);
            moveSpeed = initialMoveSpeed;
        }

        IEnumerator RepulsionOnDash()
        {
            yield return new WaitForSeconds(repulseTimeDash);
            isBeingRepulsed = false;
        }

        IEnumerator ComeOnAndFly()
        {
            isBeingRepulsed = true;
            yield return new WaitForSeconds(repulseTime);
            myRb.velocity = Vector2.zero;
            isBeingRepulsed = false;
            StopCoroutine(ComeOnAndFly());
        }
    }
}